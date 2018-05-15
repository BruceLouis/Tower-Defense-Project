using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] GameObject bullets;
    [SerializeField] float bulletPower, rateOfFire, cost, yOffset;
    [SerializeField] [Range(0f, 100f)] float range;
    
    private Transform targetEnemy;
    private bool isShooting;

	// Use this for initialization
	void Start ()
    {
        isShooting = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SetTargetEnemy();
        if (targetEnemy)
        {
            Fire();
        }
    }

    void SetTargetEnemy()
    {
        Enemy[] seenEnemies = FindObjectsOfType<Enemy>();
        if (seenEnemies.Length <= 0) { return; }

        Transform closestEnemy = seenEnemies[0].transform;
        foreach (Enemy closerEnemy in seenEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, closerEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    Transform GetClosest(Transform enemyNumeroA, Transform enemyNumeroB)
    {
        if (Vector3.Distance(enemyNumeroA.position, transform.position) < Vector3.Distance(enemyNumeroB.position, transform.position))
        {
            return enemyNumeroA;
        }
        else
        {
            return enemyNumeroB;
        }
    }

    void Fire()
    {
        float distance = Vector3.Distance(transform.position, targetEnemy.transform.position);
        objectToPan.LookAt(targetEnemy);
        if (!isShooting && distance < range)
        {
            StartCoroutine(FireBullets());
        }
    }

    IEnumerator FireBullets()
    {
        isShooting = true;
        Vector3 offset = new Vector3(0f, yOffset, 0f);
        GameObject bullet = Instantiate(bullets, objectToPan.transform.position + offset, objectToPan.transform.localRotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletPower);
        yield return new WaitForSecondsRealtime(rateOfFire);
        isShooting = false;
    }

    public float Cost
    {
        get { return cost; }
        set { cost = value; }
    }
}
