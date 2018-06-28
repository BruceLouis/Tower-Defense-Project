using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float speed;
    PathFinder pathFinder;
    List<WayPoint> path;
    Vector3 targetPosition, direction;
    private float distance;
    private int index;

    // Use this for initialization
    void Start() 
    {
        pathFinder = FindObjectOfType<PathFinder>();
        path = pathFinder.GetPath();
        index = 0;
    }

    void Update()
    {
        SmoothEnemyPathing();
    }

    void SmoothEnemyPathing()
    {
        //instead of a while loop, we use the update called every frame to iterate through the waypoint
        IdentifyNewTargetPosition();

        Vector3 direction = targetPosition - transform.position;
        RotateTowardsNewTargetPosition(direction);

        distance = Vector3.Distance(transform.position, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        IterateIndex();
    }

    void IdentifyNewTargetPosition()
    {
        if (index < path.Count) //prevents out of index error
        {
            targetPosition = path[index].transform.position;
        }
    }

    void RotateTowardsNewTargetPosition(Vector3 direction)
    {
        if (direction != Vector3.zero) //prevents jerky rotation on straight lines
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void IterateIndex()
    {
        if (distance <= 0f && index < path.Count)
        {
            index++;
        }
    }
}
