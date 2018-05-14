using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(WayPoint))]
public class CubeEditor : MonoBehaviour {
    
    WayPoint wayPoint;

    void Awake()
    {
        wayPoint = GetComponent<WayPoint>();    
    }

    // Update is called once per frame
    void Update ()
    {
        SnapToGrid();
        UpdateGrid();
    }
    
    void SnapToGrid()
    {
        transform.position = wayPoint.GetGridPosition() * wayPoint.GetGridSize();
    }

    void UpdateGrid()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        Tile tile = GetComponent<Tile>();
        Vector3Int gridPosition = wayPoint.GetGridPosition();
        string blockLabel = gridPosition.x + "," + gridPosition.z;
        textMesh.text = blockLabel;
        if (tile.IsFriendly)
        {
            gameObject.name = "Friendly Grid: " + blockLabel;
        }
        else
        {
            gameObject.name = "Enemy Grid: " + blockLabel;
        }
    }
}
