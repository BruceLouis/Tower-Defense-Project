using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject[] friendlyTiles, enemyTiles;
    private bool spotTaken = false, isFriendly = true;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!spotTaken && !EventSystem.current.IsPointerOverGameObject()) //event system to make sure tower don't get placed when in menu
            {
                FindObjectOfType<TowerFactory>().AddTower(this);
            }
            else
            {
                Debug.Log("cant place tower");
            }
        }
    }

    public void SetPathingTile()
    {
        foreach (GameObject friendlyTile in friendlyTiles)
        {
            friendlyTile.SetActive(false);
        }
        foreach (GameObject enemyTile in enemyTiles)
        {
            enemyTile.SetActive(true);
        }
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
