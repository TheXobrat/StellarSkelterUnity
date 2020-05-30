using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force = 1;     // The amount force that movement exerts on the rigidbody


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = new Vector2();
            int totalTouchs = 0;
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2) // && touch.position.y < Screen.height / 4)
                {
                    touchPosition += touch.position;
                    totalTouchs++;
                }
            }
            touchPosition /= totalTouchs;

            if (totalTouchs > 0)
            {
                horizontal = Mathf.Clamp(Mathf.Lerp(-1.5f, 1.5f, (touchPosition.x / Screen.width) * 2), -1, 1);
                vertical = Mathf.Lerp(-1, 1, (touchPosition.y / Screen.height) * 4);
            }
        }

        if (vertical < 0) { vertical = 0; }

        rb.AddForce(new Vector2(horizontal, vertical) * force * Time.deltaTime);
        rb.AddTorque((-transform.rotation.z - rb.velocity.x*0.05f) * 50 * Time.deltaTime);
    }
}
