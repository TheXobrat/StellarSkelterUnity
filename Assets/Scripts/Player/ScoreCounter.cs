using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public int score;

    private RectTransform rect;
    private Text text;

    private bool animate = false;
    public float animationSpeed = 1;
    public AnimationCurve animationCurve;
    private float animationProgress = 0;

    [NonSerialized]
    public PlayerMovement curPlayer;


    public bool IsAnimating()
    {
        return animate && animationProgress < 1;
    }

    public void IncreaseScore(int amount = 3)
    {
        if (curPlayer != null)
        {
            score+=amount;
            text.text = score.ToString();
        }
    }

    public void ClearScore()
    {
        score = 0;
        text.text = score.ToString();

        animate = false;
        UpdateAnimation(true);
    }

    public void DisplayScore()
    {
        animate = true;

        int highscore = PlayerPrefs.GetInt("highscore");
        if (score > highscore)
        { 
            text.text = "Old Highscore: " + highscore + "\nNew Highscore: " + score;
            PlayerPrefs.SetInt("highscore", score);
        }
        else
        {
            text.text = "Score: " + score + "\nHighscore: " + highscore;
        }
    }

    public void UpdateAnimation(bool reset = false)
    {
        if (reset) { animationProgress = 0; }
        else { animationProgress += animationSpeed * Time.deltaTime; }

        rect.anchorMax = new Vector2(rect.anchorMax.x, Mathf.Lerp(0.25f, 1, animationCurve.Evaluate(animationProgress)));
        rect.localScale = new Vector3(1, 1, 1) * Mathf.Lerp(1, 1.5f, animationCurve.Evaluate(animationProgress));
    }


    void Start()
    {
        rect = gameObject.GetComponent<RectTransform>();
        text = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (animate) { UpdateAnimation(); }   
    }
}
