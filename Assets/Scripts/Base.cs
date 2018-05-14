using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    [SerializeField] float hitPoints;
    [SerializeField] Slider hpBar;

    private float maxHP;
    private float sliderValue;
    private bool settingMaxHP;

    // Use this for initialization
    void Start()
    {
        settingMaxHP = true;
        SetHP(hitPoints);
    }

    void OnTriggerEnter(Collider collider)
    {
        Enemy enemy = collider.gameObject.GetComponentInParent<Enemy>();
        if (enemy)
        {
            enemy.EnemyBlowsUp(true);
            GotHit(enemy.Damage);
        }
    }

    public void GotHit(float damage)
    {
        hitPoints -= damage;
        SetHP(hitPoints);
    }
    
    public void SetHP(float currentHP)
    {
        if (settingMaxHP)
        {
            maxHP = currentHP;
            settingMaxHP = false;
        }
        sliderValue = currentHP / maxHP;
        hpBar.value = sliderValue;
    }
}

