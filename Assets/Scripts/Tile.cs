﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject friendlyTile, enemyTile;
    private bool spotTaken = false, isFriendly = true;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!spotTaken && !EventSystem.current.IsPointerOverGameObject()) //event system to make sure tower don't get placed when in menu
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
                Debug.Log("place tower");
            }
            else
            {
                Debug.Log("cant place tower");
            }
        }
        Debug.Log(gameObject.name);
    }

    public void SetPathingTile()
    {
        enemyTile.SetActive(true);
        friendlyTile.SetActive(false);
        spotTaken = true;
    }

    public bool IsFriendly
    {
        get { return isFriendly; }
        set { isFriendly = value; }
    }

    public bool IsSpotTaken
    {
        get { return spotTaken; }
        set { spotTaken = value; }
    }
}
