using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyForce : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public bool constant = true;
    public bool relative = true;
    public bool randomForce = false;
    public Vector2 force;
    public Vector2 minForce;
    public Vector2 maxForce;


    void AddForce(float multplier = 1)
    {
        if (relative) { rigidbody.AddRelativeForce(force * multplier); }
        else { rigidbody.AddForce(force * multplier); }
    }


    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        if (randomForce) { force = new Vector2(Random.Range(minForce.x, maxForce.x), Random.Range(minForce.y, maxForce.y)); }

        if (!constant)
        { 
            AddForce();
            Destroy(this);
        }
    }

    void Update()
    {
        AddForce(Time.deltaTime);
    }
}
