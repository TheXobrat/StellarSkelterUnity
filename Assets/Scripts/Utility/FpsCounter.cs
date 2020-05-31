using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    private Text text;
    public int targetFramerate = 60;
    private int framerate;


    void Start()
    {
        Application.targetFrameRate = targetFramerate;
        
        text = gameObject.GetComponent<Text>();
        if (text == null || text.enabled == false) { Destroy(this); }
    }

    void Update()
    {
        framerate = (int)(1f / Time.unscaledDeltaTime);
        text.text = framerate.ToString();
    }
}
