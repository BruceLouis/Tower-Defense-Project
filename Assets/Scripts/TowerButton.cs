using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour {

    [SerializeField] Text costText;
    [SerializeField] Tower tower;

    // Use this for initialization
    void Start () {
        costText.text = "Cost: " + tower.Cost.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
