

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public BoxCollider2D gridArea;
    public GameObject[] foodItems; // Array to hold different types of food prefabs

    public float spawnDelay = 3f;
    public float lifeTime = 8f;


    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            
            // set food boundary to spawn
            Bounds bounds = gridArea.bounds;
            float x = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
            float y = Random.Range(bounds.min.y + 1, bounds.max.y - 1);

            Vector3 spawnPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

            GameObject foodItem = Instantiate(foodItems[Random.Range(0, foodItems.Length)], spawnPosition, Quaternion.identity);
            Destroy(foodItem, lifeTime); // Destroy the food item after 10 seconds

            yield return new WaitForSeconds(spawnDelay);
        }
    }



}

