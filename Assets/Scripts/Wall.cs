using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    
    [SerializeField]
    int wallStrength = 10;
    [SerializeField]
    int wallStrengthRandom = 0;
    [SerializeField]
    WallData wallData;
    [SerializeField]
    Vector3 UIOffset;

    int materialIndex = 0;
    Text floatingUIText;

    private void Start()
    {
        GameObject floatingUI = Instantiate(wallData.uiPrefab, LevelController.levelController.floatingCanvas.transform) as GameObject;
        floatingUI.transform.position = transform.position;
        floatingUIText = floatingUI.GetComponent<Text>();
        floatingUIText.text = wallStrength.ToString();
        materialIndex = Random.Range(0, wallData.materials.Length);
        GetComponent<MeshRenderer>().material = wallData.materials[materialIndex];
    }

    public void Break(int power = 1)
    {
        wallStrength -= power;
        floatingUIText.text = wallStrength.ToString();
        if (wallStrength <= 0)
        {
            Destroy(gameObject);
            Destroy(floatingUIText.gameObject);
        }
    }

    private void Update()
    {
        if (wallData.uiRange > Mathf.Abs(LevelController.levelController.player.transform.position.z - transform.position.z))
        {
            floatingUIText.gameObject.SetActive(true);
            floatingUIText.transform.position = transform.position + UIOffset;
        }
        else
        {
            floatingUIText.gameObject.SetActive(false);
        }
        if(wallData.tranparentRange > Mathf.Abs(LevelController.levelController.player.transform.position.z - transform.position.z))
        {
            GetComponent<MeshRenderer>().material = wallData.transparentMaterials[materialIndex];
        }
    }

    private void OnDrawGizmosSelected()
    {
        gameObject.name = "Wall (" + wallStrength + ")";
    }

}
