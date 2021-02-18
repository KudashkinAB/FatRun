using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    float offset;
    private void Start()
    {
        offset = transform.position.z - target.transform.position.z;
    }

    private void Update()
    {
        transform.Translate(0, 0, target.transform.position.z + offset - transform.position.z, Space.World);
    }
}
