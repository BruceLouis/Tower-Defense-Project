using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower[] towers;
    private GameObject towerParent;
    private Money money;
    private int towerChoice;

	// Use this for initialization
	void Start () {        
        towerParent = GameObject.Find("Towers");
        if (towerParent == null)
        {
            towerParent = new GameObject("Towers");
        }
        towerParent.transform.parent = GameObject.Find("World").transform;

        money = FindObjectOfType<Money>();
        towerChoice = 0;
    }

    public void AddTower(Tile tile)
    {
        if (money.GetMoney() < towers[towerChoice].Cost) { return; }

        Tower tower = Instantiate(towers[towerChoice], tile.transform.position, Quaternion.identity) as Tower;
        tower.transform.parent = towerParent.transform;
        money.PaidMoney(tower.Cost);
        tile.IsSpotTaken = true;
    }

    public int TowerChoice
    {
        get { return towerChoice; }
        set { towerChoice = value; }
    }
}
