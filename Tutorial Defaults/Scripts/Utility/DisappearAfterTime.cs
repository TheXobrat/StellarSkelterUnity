using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearAfterTime : MonoBehaviour
{
    public float time = 1;


    void Start()
    {
        
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0) { Destroy(gameObject); }
    }
}
