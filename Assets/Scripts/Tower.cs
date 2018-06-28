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
    [SerializeField] Material[] upgradeColors;
    [SerializeField] AudioClip laserSound;
    [SerializeField] [Range(0f, 100f)] float range;
    [SerializeField] float bulletSpeed, bulletPower, rateOfFire, cost, yOffset;
    [SerializeField] float[] upgradeCost;
    [SerializeField] Sprite towerMugShot;

    public enum TurretType { projectile, machineGun, laser, splash};
    public TurretType turretType;
    
    private Transform targetEnemy;
    private Money money;
    private bool isShooting, isMakingNoise;
    private int upgradeLevel = 0, maxUpgrades = 2;

	// Use this for initialization
	void Start ()
    {
        money = FindObjectOfType<Money>();
        isShooting = false;
        isMakingNoise = false;
        UpgradeTowerAppearance();
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
        if (turretType != TurretType.splash && distance < range)
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

    void UpgradeTowerAppearance()
    {
        switch (turretType)
        {
            case TurretType.projectile:
                transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the sphere renderer in the turret object
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the base renderer in the base object
                break;
            case TurretType.machineGun:
                transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the cube renderer in the turret object
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the base renderer in the base object
                break;
            case TurretType.laser:
                Transform stripesParent = transform.GetChild(1).GetChild(0);
                foreach (Transform stripes in stripesParent)
                {
                    stripes.GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the stripes of the cube renderer
                }
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the base renderer in the base object
                break;
            case TurretType.splash:
                transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = upgradeColors[upgradeLevel]; //obtain the base renderer in the base object
                break;
        }
    }

    void MachineGunFlash(bool active)
    {
        foreach (GameObject machineGun in machineGunRifles)
        {
            machineGun.SetActive(active);
        }
    }

    public void TowerUpgrade()
    {
        try
        {
            if (money.GetMoney() < upgradeCost[upgradeLevel]) { return; }
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.LogWarning("no more upgrade levels");
        }
        if (upgradeLevel < maxUpgrades)
        {
            money.PaidMoney(upgradeCost[upgradeLevel]);
            upgradeLevel++;
            bulletPower *= 1.5f;
            bulletSpeed *= 1.5f;
            range *= 1.125f;
            rateOfFire *= 0.75f;
            UpgradeTowerAppearance();
            if (turretType == TurretType.splash)
            {
                GetComponentInChildren<Splash>().UpdateAreaSize();
            }
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

    public float[] GetUpgradeCost()
    {
        return upgradeCost;
    }

    public int GetUpgradeLevel()
    {
        return upgradeLevel;
    }

    public TurretType GetTurretType()
    {
        return turretType;
    }

    public Sprite GetTowerMugShot()
    {
        return towerMugShot;
    }
}
