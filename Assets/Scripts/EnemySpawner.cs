using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Tooltip("size of the array represents number of waves")] [SerializeField] int[] numEnemies;
    [Tooltip("element in the array represents wave number")] [SerializeField] [Range(0.25f, 10f)] float[] spawnRate;
    [Tooltip("element in the array represents wave number")] [SerializeField] Enemy[] enemies;

    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnStartDelay, waveDelay, victoryDelay;

    private IEnumerator spawnEnemies;

	// Use this for initialization
	void Start ()
    {
        spawnEnemies = SpawnEnemies();
        StartCoroutine(SpawnStartDelay(spawnStartDelay));
    }


    // Update is called once per frame
    void Update ()
    {
        if (FindObjectOfType<Base>().GetGameOver())
        {
            StopCoroutine(spawnEnemies);
        }
    }

    IEnumerator SpawnStartDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(spawnEnemies);
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numEnemies.Length; i++)
        {
            while (numEnemies[i] > 0)
            {
                Enemy enemy = Instantiate(enemies[i], spawnPosition.position, Quaternion.identity) as Enemy;
                enemy.transform.parent = transform;
                numEnemies[i]--;
                yield return new WaitForSecondsRealtime(spawnRate[i]);
            }
            yield return new WaitForSecondsRealtime(waveDelay);
        }

        if (numEnemies[numEnemies.Length - 1] <= 0)
        {
            yield return new WaitForSecondsRealtime(victoryDelay);
            StartCoroutine(FindObjectOfType<GameDirector>().Victory());
        }
    }
}
