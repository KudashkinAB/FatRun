using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LevelController.levelController.player.gameObject)
        {
            LevelController.levelController.Finish();
        }
    }
}
