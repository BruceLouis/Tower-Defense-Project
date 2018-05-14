using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour {

    void OnTriggerExit(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}
