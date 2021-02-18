using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WallData", menuName = "ScriptableObjects/Wall", order = 2)]
public class WallData : ScriptableObject
{
    public Material[] materials;
    public Material[] transparentMaterials;
    public GameObject uiPrefab;
    public float uiRange = 5f;
    public float tranparentRange = 2f;
}
