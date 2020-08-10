using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarAI : MonoBehaviour
{
    //public Transform targetPosition;
    private Seeker seeker;
    public Path path;
    //private Rigidbody2D rb;
    private bool toSpawn = true;
    private float speed = 1f;
    private MonsterController monsterController;
    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;
    public bool reachedEndOfPath;
    // Start is called before the first frame update
    public void Start()
    {
        seeker = GetComponent<Seeker>();
        speed = GameManager.Instance.gridScale.x;
        monsterController = GetComponent<MonsterController>();
        //seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }


    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay we got a path back. Did it have error? " + p.error);
        Debug.Log(p.errorLog);

        if (!p.error)
        {
            path = p;

            currentWaypoint = 0;
        }
    }
    public bool MoveToTarget(Vector3 target, string loc)
    {
        if (loc == "spawnpoint")
        {
            toSpawn = true;
        }
        else
        {
            toSpawn = false;
        }
        if (target == null)
        {
            return false;
        }
        else
        {
            Debug.Log("MovetoTarget: " + target);

            seeker.StartPath(transform.position, target, OnPathComplete);
            return true;
        }

    }
    public void Update()
    {
        if (path == null)
        {
            return;
        }
        reachedEndOfPath = false;
        float distanceToWaypoint;
        while (true)
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    //Fix for monster running constantly but not a good fix because when near spawn point, it looks like monster is floating
                    //monsterController.Idle();
                    break;
                }
            }
            else
            {
                break;
            }
        }
        // Slow down smoothly upon approaching the end of the path
        // This value will smoothly go from 1 to 0 as the agent approaches the last waypoint in the path.
        float speedFactor;
        if (toSpawn)
        {
            speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
        }
        else
        {
            speedFactor = 8f;
        }

        //var speedFactor = 5f;
        // Direction to the next waypoint
        // Normalize it so that it has a length of 1 world unit
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        // Multiply the direction by our desired speed to get a velocity
        Vector3 velocity = dir * speed * speedFactor;
        //rb.AddForce(dir, fMode);
        transform.position += velocity * Time.deltaTime;
    }
    public void CancelLastPath()
    {
        seeker.CancelCurrentPathRequest();
    }
}
