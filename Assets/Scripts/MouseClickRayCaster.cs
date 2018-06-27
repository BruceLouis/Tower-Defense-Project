using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClickRayCaster : MonoBehaviour {

    private InfoPanel infoPanel;
    private Button upgradeButton, sellButton;
    private Text nameText;
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
        }
        itemMugShot.sprite = tower.GetTowerMugShot();
    }

    public Tower GetSelectedTower()
    {
        return selectedTower;
    }
}
