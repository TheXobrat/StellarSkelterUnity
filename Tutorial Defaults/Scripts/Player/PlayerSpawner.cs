using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerMovement player;
    private PlayerMovement curPlayer;
    public GameObject splash;
    public int maxLives = 3;
    public int lives = 0;
    private ScoreCounter scoreCounter;
    private AudioClip ambientClip;
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    public void SpawnPlayer()
    {
        if (curPlayer == null && lives > 0 && !scoreCounter.IsAnimating())
        {
            if (lives == maxLives) // First spawn
            {
                scoreCounter.ClearScore();
                splash.SetActive(false);

                ChangeMusicTrack();

                ClearScreen();
            }

            lives--;

            curPlayer = Instantiate(player, gameObject.transform.position, new Quaternion());
            scoreCounter.curPlayer = curPlayer;
        }
    }

    public void ClearScreen()
    {
        foreach (DisappearOffCamera disappearOffCamera in FindObjectsOfType(typeof(DisappearOffCamera))) { Destroy(disappearOffCamera.gameObject); }
    }

    public void ChangeMusicTrack()
    {
        if (audioSource != null && musicTracks.Length > 0 && lives == maxLives)
        {
            audioSource.clip = musicTracks[Random.Range(0, musicTracks.Length)];
            audioSource.Play();
        }
    }

    public void PlayerDied()
    {
        curPlayer = null;

        if (lives == 0)
        { 
            ShowSplash(); 
            scoreCounter.DisplayScore();
        }
    }

    public void ShowSplash()
    { 
        lives = maxLives;

        foreach (CanvasGroup canvas in splash.GetComponentsInChildren<CanvasGroup>()) { canvas.alpha = 0; }
        foreach (Behaviour component in splash.GetComponentsInChildren<Behaviour>()) { component.enabled = true; }
        splash.SetActive(true);

        if (audioSource != null && audioSource.clip != ambientClip) { 
            audioSource.clip = ambientClip;
            audioSource.Play();
        }
    }


    void Start()
    {
        scoreCounter = FindObjectsOfType(typeof(ScoreCounter)).First() as ScoreCounter;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) { ambientClip = audioSource.clip; }

        ShowSplash();
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 || (Input.touches.Length > 0))
        {
            SpawnPlayer();
        }
    }
}
