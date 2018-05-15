using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour {

    [SerializeField] Text costText;
    [SerializeField] Tower tower;
    [SerializeField] Image image;
    [SerializeField] int towerChoice;

    // Use this for initialization
    void Start ()
    {
        costText.text = "Cost: " + tower.Cost.ToString();
    }

    void Update()
    {
        if (FindObjectOfType<TowerFactory>().TowerChoice == towerChoice)
        {
            image.color = Color.blue;
        }
        else
        {
            image.color = Color.clear;
        }
    }

    public void SelectTower()
    {
        FindObjectOfType<TowerFactory>().TowerChoice = towerChoice;
    }
}
