using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float animationSpeed = 0.25f;
    public Vector3 offset;

    private PlayerMovement player;
    private float startSize;


    public void UpdateAnimation()
    {
        Vector3 position = Vector3.Lerp(transform.position, player.transform.position, animationSpeed);
        position.z = transform.position.z;
        position += offset;

        transform.position = position;
    }


    void Start()
    {

    }

    void Update()
    {
        if (player == null) { player = FindObjectsOfType(typeof(PlayerMovement)).FirstOrDefault() as PlayerMovement; }
        else { UpdateAnimation(); }
    }
}
