using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public GameObject[] foodItems;

    public float spawnDelay;


    void Start()
    {
        StartCoroutine(SpawnFood());
    }

    private IEnumerator SpawnFood()
    {
        while (true)
        {
            Bounds bounds = this.gridArea.bounds;


            float x = Random.Range(bounds.min.x + 1, bounds.max.x - 1);
            float y = Random.Range(bounds.min.y + 1, bounds.max.y - 1);

            Vector3 spawnPosition = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);



            GameObject fooditems = foodItems[Random.Range(0, foodItems.Length)];

            // Instantiate the food at the randomized position
            Instantiate(fooditems, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnDelay);

        }
    }

}
