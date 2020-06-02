using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public string powerupName;
    private GameObject powerup;
    public GameObject[] remains;
    public float duration = 10;

    public bool weapon;
    private GameObject player;
    private PlayerWeapon playerWeapon;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.attachedRigidbody != null)
        {
            player = collision.collider.attachedRigidbody.gameObject;
            if (player != null)
            {
                powerup = player.transform.Find(powerupName).gameObject;
                if (powerup != null)
                { 
                    powerup.SetActive(true);

                    DisableAfterTime disableAfterTime = powerup.GetComponent<DisableAfterTime>();
                    if(disableAfterTime != null) { disableAfterTime.time = duration; }

                    if (transform.childCount > 0)
                    {
                        foreach (Transform child in transform) { Destroy(child.gameObject); }
                    }

                    if (remains.Length > 0)
                    {
                        foreach (GameObject r in remains) { Instantiate(r, transform.position, transform.rotation); }
                    }

                    if (weapon)
                    {
                        playerWeapon = player.GetComponent<PlayerWeapon>();
                        if (playerWeapon != null) { playerWeapon.enabled = false; }
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
