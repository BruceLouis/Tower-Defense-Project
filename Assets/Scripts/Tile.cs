using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour {

    [SerializeField] GameObject friendlyTile, enemyTile;
    private bool spotTaken = false, isFriendly = true;

    // Update is called once per frame
    void Update ()
    {

    }
    
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
