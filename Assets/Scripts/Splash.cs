using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    [SerializeField] ParticleSystem aoeCloud;

    private Tower tower;
    private ParticleSystem.MainModule aoeCloudProperties;
    private bool isShooting;
    private List<Enemy> enemies;

    // Use this for initialization
    void Start()
    {
        tower = GetComponentInParent<Tower>();
        aoeCloudProperties = aoeCloud.main;
        UpdateAreaSize();
        isShooting = false;
        enemies = new List<Enemy>();
    }

    void Update()
    {
        if (enemies.Count <= 0)
        {
            aoeCloud.gameObject.SetActive(false);
        }
        enemies.RemoveAll(enemy => enemy == null);
    }

    void OnTriggerEnter(Collider collider)
    {
        enemies.Add(collider.gameObject.GetComponentInParent<Enemy>());
    }

    void OnTriggerStay(Collider collider)
    {
        Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
        aoeCloud.gameObject.SetActive(true);
        if (enemy && !enemy.GetGotShot())
        {
            StartCoroutine(enemy.DealtSplashDamage(tower.GetRateOfFire(), tower.GetBulletPower()));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        enemies.Remove(collider.gameObject.GetComponentInParent<Enemy>());
    }

    public void UpdateAreaSize()
    {
        aoeCloudProperties.startSize = tower.GetRange();
        GetComponent<SphereCollider>().radius = tower.GetRange() * 0.1f;
    }
}