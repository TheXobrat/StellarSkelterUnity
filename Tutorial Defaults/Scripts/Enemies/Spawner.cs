using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class Spawnable
    {
        public GameObject gameObject;
        public float spawnChance = 1;
        public int scoreRequired = 0;
    }

    public Spawnable[] spawnlist;
    public float spawnRate = 1;

    private float spawnDelay = 0;
    private ScoreCounter scoreCounter;

    private PlayerMovement player;


    public void Spawn()
    {
        spawnDelay = 1 / spawnRate;

        Spawnable[] possibleSpawns = spawnlist.Where(x => x.scoreRequired <= scoreCounter.score).ToArray();
        if (possibleSpawns.Length > 0)
        {
            float totalSpawnChance = 0;
            foreach (Spawnable spawnable in possibleSpawns) { totalSpawnChance += spawnable.spawnChance; }
            float randomPoint = Random.value * totalSpawnChance;

            Spawnable spawn = null;
            foreach (Spawnable spawnable in possibleSpawns)
            {
                if (randomPoint < spawnable.spawnChance) { spawn = spawnable; }
                else { randomPoint -= spawnable.spawnChance; }
            }

            if (spawn != null) { Instantiate(spawn.gameObject, transform.position + new Vector3(Random.Range(-3, 3), 0, 0), transform.rotation); }
        }
    }


    void Start()
    {
        scoreCounter = FindObjectsOfType(typeof(ScoreCounter)).First() as ScoreCounter;
        spawnDelay = 1 / spawnRate;
    }

    void Update()
    {
        if (player == null) { player = FindObjectsOfType(typeof(PlayerMovement)).FirstOrDefault() as PlayerMovement;  }
        else
        {
            spawnDelay -= Time.deltaTime;
            if (spawnDelay < 0) { spawnDelay = 0; }
            if (spawnDelay == 0) { Spawn(); }
        }
    }
}
