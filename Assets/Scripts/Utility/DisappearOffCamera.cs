using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOffCamera : MonoBehaviour
{
    public float time = 5;
    private float startTime;
    public bool isVisible = false;
    private Renderer[] renderers;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) { Destroy(this); }

        startTime = time;
    }

    void Update()
    {
        isVisible = false;
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                isVisible = true;
                break;
            }
        }

        if (!isVisible)
        {
            time -= Time.deltaTime;
            if (time <= 0) { Destroy(gameObject); }
        }
        else { time = startTime; }
    }
}
