using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "ScriptableObjects/Food", order = 1)]
public class FoodData : ScriptableObject
{
    public int foodScore = 1;
    public List<GameObject> foodPrefabs;
}
