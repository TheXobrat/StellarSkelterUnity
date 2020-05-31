using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnCurve : MonoBehaviour
{
    public float speed = 1;
    public AnimationCurve curveX;
    public AnimationCurve curveY;
    public float curveTime = 1;
    private float animationProgress = 0;
    public bool relative;


    void Start()
    {

    }

    void Update()
    {
        animationProgress += speed * Time.deltaTime;
        if (animationProgress > 1) { animationProgress = 0; }

        transform.Translate(new Vector2(curveX.Evaluate(animationProgress), curveY.Evaluate(animationProgress)) * Time.deltaTime, relative ? Space.Self : Space.World);
    }
}
