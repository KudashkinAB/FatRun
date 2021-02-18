using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    FoodData foodData;
    private void Start()
    {
        GameObject foodAppearance = Instantiate(foodData.foodPrefabs[Random.Range(0, foodData.foodPrefabs.Count)], transform) as GameObject;
        foodAppearance.transform.position = transform.position;
    }

    public int PickedUp()
    {
        Destroy(gameObject);
        return foodData.foodScore;
    }

    private void OnDrawGizmosSelected()
    {
        gameObject.name = "Food (" + foodData.foodScore + ")";
    }
}
