using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Tooltip("size of the array represents number of waves")] [SerializeField] int[] numEnemies;

    [SerializeField] [Range (0.25f, 10f)] float spawnRate;
    [SerializeField] Enemy enemies;
    [SerializeField] Transform spawnPosition;
    [SerializeField] float spawnStartDelay, waveDelay;    

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnStartDelay(spawnStartDelay));
    }


    // Update is called once per frame
    void Update () {
    }

    IEnumerator SpawnStartDelay(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(SpawnEnemies(spawnRate));
    }

    IEnumerator SpawnEnemies(float spawnRate)
    {
        for (int i = 0; i < numEnemies.Length; i++)
        {
            while (numEnemies[i] > 0)
            {
                Enemy enemy = Instantiate(enemies, spawnPosition.position, Quaternion.identity) as Enemy;
                enemy.transform.parent = transform;
                numEnemies[i]--;
                yield return new WaitForSecondsRealtime(spawnRate);
            }
            yield return new WaitForSecondsRealtime(waveDelay);
        }
    }
}
