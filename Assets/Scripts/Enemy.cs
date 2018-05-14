using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] ParticleSystem enemyExplosion, hitExplosion, goalExplosion;
    [SerializeField] float hitPoints, damage, scoreValue, cashValue;
    [SerializeField] AudioClip hitSound, deathSound;

    private Vector3 soundPlayedAtCamera;

    void Start()
    {
        soundPlayedAtCamera = Camera.main.transform.position;
    }

    public void GotHit(float damageDealt)
    {
        AudioSource.PlayClipAtPoint(hitSound, soundPlayedAtCamera);
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        hitPoints -= damageDealt;
    }

    public void IsEnemyDead()
    {
        if (hitPoints <= 0f)
        {
            EnemyBlowsUp(false);
            FindObjectOfType<Score>().AddScore(scoreValue);
            FindObjectOfType<Money>().AddMoney(cashValue);
        }
    }

    public void EnemyBlowsUp(bool didHeGetToGoalLine)
    {
        if (didHeGetToGoalLine)
        {
            Instantiate(goalExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            AudioSource.PlayClipAtPoint(deathSound, soundPlayedAtCamera);
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
