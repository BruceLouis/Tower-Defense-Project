using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClickRayCaster : MonoBehaviour {

    /* uses raycast that casts a line on where the mouse clicks on the scene
     * then whoever comes in contact with the line from the raycast
     * is what will determine what actions are done. for example, if it hits a tower component, 
     * it activates panel and stores the selected tower in a variable
    */

    private InfoPanel infoPanel;
    private Button upgradeButton, sellButton;
    private Text nameText, upgradeCostText;
    private Image itemMugShot;
    private Tower selectedTower;

    // Use this for initialization
    void Start ()
    {
        infoPanel = FindObjectOfType<InfoPanel>();
        itemMugShot = infoPanel.transform.GetChild(0).GetComponent<Image>(); //child index 0 has image component
        nameText = infoPanel.transform.GetChild(1).GetComponent<Text>(); //child index 1 has name of item text component
        upgradeButton = infoPanel.transform.GetChild(2).GetComponent<Button>(); //child index 2 has name of item text component
        sellButton = infoPanel.transform.GetChild(3).GetComponent<Button>(); //child index 3 has name of item text component
        upgradeCostText = infoPanel.transform.GetChild(4).GetComponent<Text>(); //child index 4 has upgrade cost of item text component

        InfoPanelActivation(false);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            SelectedItem();
        }
	}

    void SelectedItem()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit))
        {
            if (hit.transform.GetComponent<Tower>())
            {
                selectedTower = hit.transform.GetComponent<Tower>();
                InfoPanelActivation(true);
                TowerSelection(selectedTower);
            }
            else if (EventSystem.current.IsPointerOverGameObject()) //if clicked on UI panel, get out of this method
            {
                return;
            }
            else
            {
                InfoPanelActivation(false);
            }
        }
    }

    void InfoPanelActivation(bool infoActive)
    {
        itemMugShot.gameObject.SetActive(infoActive);
        nameText.gameObject.SetActive(infoActive);
        upgradeButton.gameObject.SetActive(infoActive);
        sellButton.gameObject.SetActive(infoActive);
        upgradeCostText.gameObject.SetActive(infoActive);
    }

    void TowerSelection(Tower tower)
    {
        switch (tower.GetTurretType())
        {
            case Tower.TurretType.projectile:
                nameText.text = "Cannon Tower";
                break;
            case Tower.TurretType.machineGun:
                nameText.text = "Machine Gun Tower";
                break;
            case Tower.TurretType.laser:
                nameText.text = "Laser Tower";
                break;
            case Tower.TurretType.splash:
                nameText.text = "Splash Tower";
                break;
        }

        try
        {
            upgradeCostText.text = "Upgrade Cost: " + tower.GetUpgradeCost()[tower.GetUpgradeLevel()];
        }
        catch (System.IndexOutOfRangeException)
        {
            upgradeCostText.text = "Max Upgrade";
        }

        itemMugShot.sprite = tower.GetTowerMugShot();
    }

    public Tower GetSelectedTower()
    {
        return selectedTower;
    }
}
