using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

    [SerializeField] Tower towerPrefab;
    private GameObject towerParent;
    private Money money;

	// Use this for initialization
	void Start () {
        towerParent = GameObject.Find("Towers");
        if (towerParent == null)
        {
            towerParent = new GameObject("Towers");
        }
        towerParent.transform.parent = GameObject.Find("World").transform;

        money = FindObjectOfType<Money>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddTower(Tile tile)
    {
        if (money.GetMoney() < towerPrefab.Cost) { return; }

        Tower tower = Instantiate(towerPrefab, tile.transform.position, Quaternion.identity) as Tower;
        tower.transform.parent = towerParent.transform;
        money.PaidMoney(tower.Cost);
        tile.IsSpotTaken = true;
    }
}
