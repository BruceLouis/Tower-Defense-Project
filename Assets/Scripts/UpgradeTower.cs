using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour {

    private MouseClickRayCaster mouseClickRayCaster;
    private Button button;

	// Use this for initialization
	void Start () {
        mouseClickRayCaster = FindObjectOfType<MouseClickRayCaster>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickedUpgrade);

    }
	
    void ClickedUpgrade()
    {
        Tower selectedTower = mouseClickRayCaster.GetSelectedTower();
        Debug.Log(selectedTower);
        selectedTower.GetComponent<Tower>().TowerUpgrade();
    }
}
