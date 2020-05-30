using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public GameObject remains;


    void Start()
    {
        
    }

    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (remains != null && transform.localScale.x > 0.05f) { Instantiate(remains, transform.position, transform.rotation); }

        Destroy(gameObject);
    }
}
