using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 30f;
    [SerializeField] float swipeDuration = 0.2f;
    [SerializeField] float pushSpeed = 0.75f;
    bool inRun = false;
    bool finished = false;

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("Weight")]
    [SerializeField] float fatness = 0f;
    [SerializeField] float maxFatness = 120;
    [SerializeField] float fatnessMultiply = 4f;
    [SerializeField] float breakingTime = 0.5f;

    [Header("Components")]
    [SerializeField] Text scoreText;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] SwipeDetector swipeDetector;

    Rigidbody rb;
    Wall pushedWall;
    float targetX = 0.5f;
    Coroutine swipeCoroutine;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(PushCor());

    }

    private void FixedUpdate()
    {
        
        if (inRun)
        {
            Vector3 delta;
            if (pushedWall == null)
            {
                delta = new Vector3(0, 0, movementSpeed);
            }
            else
            {
                delta = new Vector3(0, 0, pushSpeed);
            }
            delta *= Time.deltaTime;
            rb.position = rb.position + delta;
            transform.LookAt(transform.position + new Vector3(targetX - transform.position.x, 0, 1f));
            
        }
    }

    private void Update()
    {
        FatnessModify(0);
        if (swipeCoroutine == null && inRun)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Swipe(-1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Swipe(1);
            }
        }
    }

    public void SetRun(bool state)
    {
        animator.SetBool("run", state);
        inRun = state;
    }

    public void Finish()
    {
        SetRun(false);
        animator.SetBool("finish", true);
    }

    public void PickUpFood(int score)
    {
        FatnessModify(score);
    }

    public void FatnessModify(float fatnesMod)
    {
        fatness = fatness + fatnesMod;
        scoreText.text = fatness.ToString();
        meshRenderer.SetBlendShapeWeight(0, Mathf.Clamp(fatness * fatnessMultiply,0,maxFatness));
        transform.GetChild(0).localScale = new Vector3(1,1,1) * (1f + 0.25f * fatness * fatnessMultiply / maxFatness);
        if(fatness <= 0)
        {
            SetRun(false);
            LevelController.levelController.GameOver();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Food":
                PickUpFood(collision.gameObject.GetComponent<Food>().PickedUp());
                break;
            case "Wall":
                if (swipeCoroutine != null && collision.gameObject.transform.position.z - transform.position.z < 0.5f)
                {
                    Swipe((int)Mathf.Sign(transform.position.x - collision.gameObject.transform.position.x));
                }
                else
                {
                    pushedWall = collision.gameObject.GetComponent<Wall>();
                }
                break;
            case "Obstacle":
                Swipe((int)Mathf.Sign(transform.position.x - collision.gameObject.transform.position.x));
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (pushedWall != null && pushedWall.gameObject == collision.gameObject)
        {
            pushedWall = null;
        }
    }
        
    public void Swipe(int swipe)
    {
        float targetXSwipe = targetX + swipe;
        if (targetXSwipe < -2 || targetXSwipe > 2)
            return;
        targetX = targetXSwipe;
        if(swipeCoroutine != null)
            StopCoroutine(swipeCoroutine);
        swipeCoroutine = StartCoroutine(SwipeCor(targetX, swipeDuration));
    }

    IEnumerator SwipeCor(float targetPosition, float duration)
    {
        float time = 0;
        float startPosition = transform.position.x;

        while (time < duration)
        {
            transform.position += new Vector3(Mathf.Lerp(startPosition, targetPosition, time / duration) - transform.position.x, 0, 0);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position += new Vector3(targetPosition - transform.position.x, 0, 0);
        swipeCoroutine = null;
    }

    IEnumerator PushCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(breakingTime);
            if (pushedWall != null && inRun)
            {
                pushedWall.Break();
                FatnessModify(-1);
            }
        }
    }

}
