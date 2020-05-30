using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffCamera : MonoBehaviour
{
    private Destructible destructible;


    void Start()
    {
        destructible = gameObject.GetComponent<Destructible>();
    }

    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        if (destructible != null) { destructible.Destroy(); }
        Destroy(this);
    }
}
