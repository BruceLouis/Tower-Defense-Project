using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    [SerializeField] Transform objectToPan;
    [SerializeField] Bullet bullets;
    [SerializeField] LineRenderer laserLine;
    [SerializeField] GameObject[] machineGunRifles;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0f, 100f)] float range;
    [SerializeField] float bulletSpeed, bulletPower, rateOfFire, cost, yOffset;

    private enum TurretType { projectile, machineGun, laser, splash};
    [SerializeField] TurretType turretType;
    
    private Transform targetEnemy;
    private bool isShooting, isMakingNoise;

	// Use this for initialization
	void Start ()
    {
        isShooting = false;
        isMakingNoise = false;
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
        if (turretType != TurretType.splash)
        {
            objectToPan.LookAt(targetEnemy);
        }
        if (!isShooting && distance < range)
        {
            if (turretType == TurretType.projectile)
            {
                StartCoroutine(FireBullets());
            }
            else if (turretType == TurretType.machineGun)
            {
                StartCoroutine(FireMachineGuns());
            }
            else if (turretType == TurretType.laser)
            {
                StartCoroutine(FireLasers());
            }
        }
    }

    IEnumerator FireBullets()
    {
        isShooting = true;
        Vector3 offset = new Vector3(0f, yOffset, 0f);

        Bullet bullet = Instantiate(bullets, objectToPan.transform.position + offset, objectToPan.transform.localRotation) as Bullet;
        bullet.Damage = bulletPower;
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed);

        yield return new WaitForSecondsRealtime(rateOfFire);
        isShooting = false;
    }

    IEnumerator FireMachineGuns()
    {
        //basically just call the got hit method on the target enemy 
        isShooting = true;

        MachineGunFlash(true);
        targetEnemy.GetComponent<Enemy>().GotHit(bulletPower, true);
        targetEnemy.GetComponent<Enemy>().GotHit(bulletPower, true);
        yield return new WaitForSecondsRealtime(0.1f);

        MachineGunFlash(false);
        yield return new WaitForSecondsRealtime(rateOfFire);

        isShooting = false;
    }

    IEnumerator FireLasers()
    {
        //cast a raycast on the target enemy, then render line and do damage to enemy
        isShooting = true;
        Vector3 offset = new Vector3(0f, yOffset, 0f);

        laserLine.enabled = true;
        laserLine.SetPosition(0, objectToPan.position + offset);
        laserLine.SetPosition(1, targetEnemy.transform.position);
        targetEnemy.GetComponent<Enemy>().GotHit(bulletPower, false);

        if (!isMakingNoise)
        {
            StartCoroutine(LaserSound());
        }

        yield return new WaitForSecondsRealtime(rateOfFire);
        laserLine.enabled = false;
        isShooting = false;
    }

    IEnumerator LaserSound()
    {
        isMakingNoise = true;
        AudioSource.PlayClipAtPoint(laserSound, Camera.main.transform.position + new Vector3(0f, 0f, -10f));
        yield return new WaitForSecondsRealtime(1f);
        isMakingNoise = false;
    }

    void MachineGunFlash(bool active)
    {
        foreach (GameObject machineGun in machineGunRifles)
        {
            machineGun.SetActive(active);
        }
    }

    public float Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public float GetRange()
    {
        return range;
    }

    public float GetRateOfFire()
    {
        return rateOfFire;
    }

    public float GetBulletPower()
    {
        return bulletPower;
    }
}
