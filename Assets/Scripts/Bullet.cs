using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] float damage;

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.GotHit(damage);
            enemy.IsEnemyDead();
        }
        Destroy(gameObject);
    }

}
