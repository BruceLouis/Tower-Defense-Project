using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {
        
    const int gridSize = 10;
    private Vector3Int gridPosition;
    private bool isExplored = false;

    public Vector3Int GetGridPosition()
    {
        gridPosition.x = Mathf.RoundToInt(transform.position.x / gridSize);
        gridPosition.y = Mathf.RoundToInt(transform.position.y / gridSize);
        gridPosition.z = Mathf.RoundToInt(transform.position.z / gridSize);

        return gridPosition;
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public bool IsExplored
    {
        get { return isExplored; }
        set { isExplored = value; }
    }
    
    public WayPoint ExploredFrom { get; set; }
}
