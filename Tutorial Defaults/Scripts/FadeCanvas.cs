using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    private CanvasGroup canvas;
    public float fadeSpeed = 0.1f;


    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvas != null) 
        {
            canvas.alpha += fadeSpeed * Time.deltaTime;

            if (canvas.alpha <= 0 || canvas.alpha >= 1) { enabled = false; }
        }
        else { Destroy(this); }
    }
}
