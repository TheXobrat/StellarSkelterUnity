using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthColor : MonoBehaviour
{
    public Destructible destructible;
    public Color[] colors;
    private float maxHealth;
    private Renderer ren;


    void Start()
    {
        maxHealth = destructible.health;
        ren = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (ren != null && ren.material.HasProperty("_EmissionColor"))
        {
            ren.material.SetColor("_EmissionColor", Color.Lerp(colors[1], colors[0], destructible.health / maxHealth));
        }
        else { Destroy(this); }
    }
}
