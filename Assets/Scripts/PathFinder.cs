using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    /* we use the breadth first search algorithm to determine how
     * the enemies pathfind their way to their target
     */

    Dictionary<Vector3Int, WayPoint> grid = new Dictionary<Vector3Int, WayPoint>();
    Queue<WayPoint> queue = new Queue<WayPoint>();
    List<WayPoint> path = new List<WayPoint>(); 

    Vector3Int[] directions = {
        Vector3Int.right,
        Vector3Int.left,
        new Vector3Int (0,0,1), //for some reason, unity doesnt have shorthand for forward or back with vector ints
        new Vector3Int (0,0,-1)            
    };

    [SerializeField] WayPoint startPoint, endPoint;
    private bool foundFinishLine = false;
    private WayPoint searchPoint;

    void Start()
    {
        CalculatePath();    
    }

    void BreadthFirstSearch()
    {
        queue.Enqueue(startPoint);

        while (queue.Count > 0 && !foundFinishLine)
        {
            FoundTheFinishLine(queue);
            searchPoint = queue.Dequeue();
            searchPoint.IsExplored = true;
            ExploreNeighbours();
        }        
    }

    void CreatePath()
    {
        path.Add(endPoint);
        WayPoint previous = endPoint.ExploredFrom;
        while (previous != startPoint)
        {
            path.Add(previous);
            previous.GetComponent<Tile>().IsFriendly = false;
            previous.GetComponent<Tile>().SetPathingTile();
            previous = previous.ExploredFrom;
        }

        path.Add(startPoint);
        path.Reverse();
    }

    void FoundTheFinishLine(Queue<WayPoint> wayPointsInQueue)
    {
        if (wayPointsInQueue.Contains(endPoint))
        {
            foundFinishLine = true;
            Debug.Log("stop search now");
        }
    }

    void LoadBlocks()
    {
        WayPoint[] wayPoints = FindObjectsOfType<WayPoint>();
        foreach(WayPoint wayPoint in wayPoints)
        {
            if ( grid.ContainsKey(wayPoint.GetGridPosition()) )
            {
                Debug.LogWarning("skipping overlapping block " + wayPoint);
            }
            else
            {
                grid.Add(wayPoint.GetGridPosition(), wayPoint);
            }
        }
    }
    
    void ExploreNeighbours()
    {
        if (foundFinishLine ) { return; }

        foreach (Vector3Int direction in directions)
        {
            Vector3Int neighbourCoordinates = searchPoint.GetGridPosition() + direction;
            if (grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    void QueueNewNeighbours(Vector3Int neighbourCoordinates)
    {
        WayPoint neighbour = grid[neighbourCoordinates];
        if (!neighbour.IsExplored && !queue.Contains(neighbour)) //prevent duplicates from being queued in and searched from
        {
            queue.Enqueue(neighbour);
            neighbour.ExploredFrom = searchPoint;
        }
    }

    void CalculatePath()
    {
        LoadBlocks();
        BreadthFirstSearch();
        CreatePath();
    }

    public List<WayPoint> GetPath()
    {
        return path;
    }
}
