using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] [Range (0.25f, 10f)] float spawnRate;
    [SerializeField] Enemy enemies;
    [SerializeField] Transform spawnPosition;
    [SerializeField] int numEnemies;
    [SerializeField] float spawnStartDelay;
    private bool isSpawning, canStartSpawning;    

	// Use this for initialization
	void Start () {
        isSpawning = false;
        canStartSpawning = false;
        StartCoroutine(SpawnStartDelay(spawnStartDelay));
	}


    // Update is called once per frame
    void Update () {
        if (!isSpawning && canStartSpawning)
        {
            StartCoroutine(SpawnEnemies(spawnRate));
        }
    }

    IEnumerator SpawnStartDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        canStartSpawning = true;
    }

    IEnumerator SpawnEnemies(float spawnRate)
    {
        while (numEnemies > 0)
        {
            isSpawning = true;
            Enemy enemy = Instantiate(enemies, spawnPosition.position, Quaternion.identity) as Enemy;
            enemy.transform.parent = transform;
            numEnemies--;
            yield return new WaitForSecondsRealtime(spawnRate);
            isSpawning = false;
        }
    }
}
