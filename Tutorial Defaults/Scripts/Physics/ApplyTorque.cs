using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTorque : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool constant = true;
    public bool randomTorque = false;
    public float torque = 0;
    public float minTorque = 1;
    public float maxTorque = 2;


    void AddTorque(float multplier = 1)
    {
        rb.AddTorque(torque * multplier);
    }

    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        if (randomTorque) { torque = Random.Range(minTorque, maxTorque); }

        if (!constant)
        {
            AddTorque();
            Destroy(this);
        }
    }

    void Update()
    {
        AddTorque(Time.deltaTime);
    }
}
