using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject[] bullets;        // The bullet object that will fire
    public GameObject[] muzzles;        // Where the bullets will spawn from
    public GameObject muzzleFlash;
    public int shots = 1;               // The number of bullets to fire
    public float fireRate = 5;          // How many bullets can fire per second
    private float fireDelay = 0;        // How long until we can fire again

    public float maxHeat = 5;
    public float heatPerShot = 1;
    public float cooldownRate = 3;
    public float overheatingCooldownRate = 1;
    public float heat = 0;
    public bool overheating = false;
    public AudioClip overheatSound;
    private AudioSource audioSource;


    public void Fire()
    {
        fireDelay = 1 / fireRate;
        
        if (!overheating)
        {
            heat += heatPerShot;

            foreach (GameObject muzzle in muzzles.Take(shots))
            {
                Instantiate(muzzleFlash, muzzle.transform);

                foreach (GameObject bullet in bullets)
                {
                    GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
                    b.layer = LayerMask.NameToLayer("PlayerBullet");
                }
            }
        }
        else if (audioSource != null && overheatSound != null)
        {
            audioSource.PlayOneShot(overheatSound);
        }
    }


    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (heat > 0)
        { 
            heat -= (overheating ? overheatingCooldownRate : cooldownRate) * Time.deltaTime;

            if (heat >= maxHeat) { overheating = true; }
        }
        else if (heat < 0) { heat = 0; }
        else { overheating = false; }

        if (fireDelay > 0) { fireDelay -= Time.deltaTime; }
        else if (fireDelay < 0) { fireDelay = 0; }
        else if (
            Input.GetAxis("Fire1") > 0 ||
            Input.touches.Any(t => t.position.x > Screen.width / 2))
        {
            Fire();
        }
    }
}
