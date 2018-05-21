using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] ParticleSystem enemyExplosion, hitExplosion, goalExplosion;
    [SerializeField] float hitPoints, damage, scoreValue, cashValue;
    [SerializeField] AudioClip hitSound, deathSound, goalSound;

    private Vector3 soundPlayedAtCamera;
    private bool gotShot;

    void Start()
    {
        soundPlayedAtCamera = Camera.main.transform.position + new Vector3(0f, 0f, -10f);
    }

    void Update()
    {
        IsEnemyDead();
    }

    void IsEnemyDead()
    {
        if (hitPoints <= 0f)
        {
            EnemyBlowsUp(false);
            FindObjectOfType<Score>().AddScore(scoreValue);
            FindObjectOfType<Money>().AddMoney(cashValue);
        }
    }

    public void GotHit(float damageDealt, bool isSFXOn)
    {
        if (isSFXOn)
        {
            AudioSource.PlayClipAtPoint(hitSound, soundPlayedAtCamera);
        }
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        hitPoints -= damageDealt;
    }

    public void EnemyBlowsUp(bool didHeGetToGoalLine)
    {
        if (didHeGetToGoalLine)
        {
            AudioSource.PlayClipAtPoint(goalSound, soundPlayedAtCamera);
            Instantiate(goalExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            AudioSource.PlayClipAtPoint(deathSound, soundPlayedAtCamera);
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public IEnumerator DealtSplashDamage(float rateOfFire, float damageDealt)
    {
        gotShot = true;
        AudioSource.PlayClipAtPoint(hitSound, soundPlayedAtCamera);
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        hitPoints -= damageDealt;
        yield return new WaitForSecondsRealtime(rateOfFire);
        gotShot = false;
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public bool GetGotShot()
    {
        return gotShot;
    }
}
