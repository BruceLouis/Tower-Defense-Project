using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {

    [SerializeField] float hitPoints;
    [SerializeField] Slider hpBar;

    private GameDirector gameDirector;
    private float maxHP, sliderValue;
    private bool settingMaxHP, gameOver;

    // Use this for initialization
    void Start()
    {
        gameDirector = FindObjectOfType<GameDirector>();

        settingMaxHP = true;
        gameOver = false;
        SetHP(hitPoints);
    }

    void Update()
    {
        if (hitPoints <= 0f && !gameOver)
        {
            StartCoroutine(gameDirector.GameOverSon());
            gameOver = true;
        }
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
    
    public bool GetGameOver()
    {
        return gameOver;
    }
}

