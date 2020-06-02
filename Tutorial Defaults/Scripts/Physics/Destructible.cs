using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private bool destroyed = false;
    public int health = 10;

    public bool scoresPoints = true;
    public float decayDelay = 3;
    public float decaySpeed = 0.3f;
    public GameObject[] remains;
    public GameObject debrisRemains;
    public LayerMask debrisLayer;
    private float hitDelay = 0.1f;
    private ScoreCounter scoreCounter;


    public void Destroy()
    {
        health = 0;
        destroyed = true;

        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        { 
            PlayerSpawner playerSpawner = FindObjectsOfType(typeof(PlayerSpawner)).FirstOrDefault() as PlayerSpawner;
            if (playerSpawner != null) { playerSpawner.PlayerDied(); }
        }

        if (remains.Length > 0)
        {
            foreach (GameObject r in remains) { Instantiate(r, transform.position, transform.rotation); }
        }

        foreach (var component in GetComponents<Component>())
        {
            if (!(component is Transform) && component != this) { Destroy(component); }
        }

        foreach (Transform child in GetComponentsInChildren<Transform>().Where(x => x != transform)) // Config debris
        {
            child.gameObject.layer = Helpers.layermask_to_layer(debrisLayer);

            Renderer renderer = child.gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (renderer.material.HasProperty("_Color"))
                {
                    renderer.material.SetColor("_Color", renderer.material.GetColor("_Color") + new Color(0.2f, 0.2f, 0.2f, 1));
                }

                Rigidbody2D rb = child.gameObject.GetComponent<Rigidbody2D>();
                if (rb == null) { rb = child.gameObject.AddComponent<Rigidbody2D>(); }
                if (rb != null)
                {
                    rb.mass = 0.1f;
                    rb.AddExplosionForce(Random.Range(0, 20), transform.position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f), 0), 20);

                    Collider c = child.gameObject.GetComponent<Collider>();
                    if (c != null) { c.enabled = true; }
                }

                Debris debris = child.gameObject.AddComponent<Debris>();
                debris.remains = debrisRemains;

                foreach (Transform subChild in child) { Destroy(subChild.gameObject); }
            }
            else
            {
                foreach (var component in child.GetComponents<Component>())
                {
                    if (!(component is Transform)) { Destroy(component); }
                }
            }
        }
    }

    public void UpdateDebris()
    {
        if (decayDelay > 0) { decayDelay -= Time.deltaTime; }
        else
        {
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.GetComponent<Renderer>() != null && Random.Range(0, 3) > 1)
                {
                    child.localScale -= new Vector3(decaySpeed * Time.deltaTime, decaySpeed * Time.deltaTime, decaySpeed * Time.deltaTime);
                    if (child.localScale.x <= 0)
                    {
                        if (child.gameObject.layer != LayerMask.NameToLayer("PlayerBullet")) { scoreCounter.IncreaseScore(); }
                        Destroy(child.gameObject);
                    }
                }
            }

            if (GetComponentsInChildren<Renderer>().Length == 0) { Destroy(gameObject); }
        }
    }


    void Start()
    {
        scoreCounter = FindObjectsOfType(typeof(ScoreCounter)).First() as ScoreCounter;
    }

    void Update()
    {
        if (destroyed) { UpdateDebris(); }
        else
        {
            if (health < 0) { health = 0; }
            if (health == 0) { Destroy(); }

            if (hitDelay < 0) { hitDelay = 0; }
            else if (hitDelay > 0) { hitDelay -= Time.deltaTime; }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitDelay == 0 && collision.transform.localScale.x > 0.05f)
        {
            health--;
            hitDelay = 0.1f;
        }
    }
}
