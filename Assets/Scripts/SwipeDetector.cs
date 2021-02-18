using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    [SerializeField]
    bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    checkSwipe();
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                checkSwipe();
            }
        }
    }

    void checkSwipe()
    {
        if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

    }

    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }
    
    void OnSwipeLeft()
    {
        player.Swipe(-1);
        Debug.Log("Swipe Left");
    }

    void OnSwipeRight()
    {
        player.Swipe(1);
        Debug.Log("Swipe Right");
    }
}
