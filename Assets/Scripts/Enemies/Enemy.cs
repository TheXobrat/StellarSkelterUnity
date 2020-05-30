using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool faceForward = true;

    private bool fire = false;
    public int shots = 1;           // The number of bullets to fire
    public GameObject[] bullets;    // The bullet object that will fire
    public GameObject[] muzzles;    // Where the bullets will spawn from
    public GameObject muzzleFlash;
    public float fireRate = 5;      // How many bullets can fire per second
    private float fireDelay = 0;    // How long until we can fire again


    public void Fire()
    {
        ResetFireDelay();

        foreach (GameObject muzzle in muzzles.Take(shots)) {
            foreach (GameObject bullet in bullets) {
                GameObject b = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation, transform.parent);
                b.layer = LayerMask.NameToLayer("EnemyBullet");
            }

            Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation, muzzle.transform);
        }
    }

    public void ResetFireDelay()
    { 
        fireDelay = 1 / fireRate;
    }


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        ResetFireDelay();
    }

    void Update()
    {
        if (faceForward) { rb.AddTorque(-transform.rotation.z * Time.deltaTime); }

        if (fire)
        {
            if (fireDelay > 0) { fireDelay -= Time.deltaTime; }
            else if (fireDelay < 0) { fireDelay = 0; }
            else { Fire(); }
        }
        else 
        { 
            foreach(GameObject muzzle in muzzles)
            {
                Renderer ren = muzzle.GetComponent<Renderer>();
                if (ren != null && ren.isVisible)
                {
                    fire = true;
                    break;
                }
            }
        }
    }
}
