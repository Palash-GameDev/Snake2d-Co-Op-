using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryItems : MonoBehaviour
{
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject,timeToDestroy);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GreenSnake"))
        {
            Destroy(this.gameObject);
        }
    }
}
