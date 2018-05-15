using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMenu : MonoBehaviour {

    private bool active;
    public void ActivateMenu()
    {
        active = !active;
        gameObject.SetActive(active);
    }
}
