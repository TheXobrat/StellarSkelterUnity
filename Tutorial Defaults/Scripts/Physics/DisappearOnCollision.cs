using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
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
        if (remains != null) { Instantiate(remains, transform.position, transform.rotation); }

        Destroy(gameObject);
    }
}
