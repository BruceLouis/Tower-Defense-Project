using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float damage;

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.GotHit(damage, true);
        }
        Destroy(gameObject);
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

}
