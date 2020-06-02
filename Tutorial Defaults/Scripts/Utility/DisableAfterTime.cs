using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float time = 10;
    private float initialTime;
    public Behaviour enableOther;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = initialTime;
            if (enableOther != null) { enableOther.enabled = true; }
            gameObject.SetActive(false);
        }
    }
}
