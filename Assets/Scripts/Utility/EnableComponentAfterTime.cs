using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableComponentAfterTime : MonoBehaviour
{
    public Behaviour component;
    public float time = 1;
    private float startTime;


    void Start()
    {
    }

    void Update()
    {
        if (time == startTime) { component.enabled = false; }

        time -= Time.deltaTime;
        if (time <= 0)
        { 
            component.enabled = true;
            time = startTime;
            this.enabled = false;
        }
    }

    private void Awake()
    {
        startTime = time;
    }

    private void OnEnable()
    {
        time = startTime;
    }
}
