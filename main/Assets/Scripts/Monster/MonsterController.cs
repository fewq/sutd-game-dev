using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    public Transform spawnPoint;
    public Animator animator;
    public GameObject exclaimation;
    public GameObject heartExclaimation;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Transform flameLocation;
    public bool flameInRange { get; set; }
    public bool playerInRange { get; set; }
    [HideInInspector]
    public bool stare = false;

    // Variables for chasing enemy
    public Transform target;
    public float nextWaypointDistance = 3f;
    private float repeatInterval = 0.5f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedPlayer = false;
    private Seeker seeker;
    
    // Start is called before the first frame update
    void Start()
    {
        // move to spawn point
        transform.position = spawnPoint.position;
        // initialise variables
        flameInRange = false;
        playerInRange = false;
        rigidbody = GetComponent<Rigidbody2D>();

        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, repeatInterval);
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) // don't calculate path again if in the middle of calculating
        {
            seeker.StartPath(rigidbody.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (flameInRange)
        {
            if (!distanceCloserThan(flameLocation.position, 1.5f))
            {
                Debug.Log("Chase Flame");
                ChaseTarget(flameLocation.position);
            }
            else if (!stare)
            {
                Stare();
            }
        }

        else if (playerInRange)
        {
            ChasePlayer();
        }
        else if (!distanceCloserThan(spawnPoint.position, 0.2f))
        {
            ReturnToSpawnPoint();
        }
        else
        {
            // Idle down
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", -1f);
        }
    }

    private bool distanceCloserThan(Vector3 target, float distance)
    {
        return (Mathf.Abs((target - transform.position).magnitude) <= distance);
    }

    public void ChaseFlame(Transform flame)
    {
        heartExclaimation.SetActive(true);
        flameLocation = flame;
        flameInRange = true;
    }

    public void ChasePlayer()
    {
        exclaimation.SetActive(true);
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedPlayer = true;
            return;
        }
        else
        {
            reachedPlayer = false;
        }

        ChaseTarget((Vector3)path.vectorPath[currentWaypoint]);

        // update next waypoint if the current one is reached
        float distance = Vector2.Distance(rigidbody.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public void ChaseTarget(Vector3 targetPos)
    {
        Debug.Log("Chasing");
        direction = (targetPos - transform.position);
        // prevents interpolation between left/right/up/down movement
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0f;
        }
        else
        {
            direction.x = 0f;
        }
        direction.Normalize();
        rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void Stare()
    {
        Debug.Log("Start to stare");
        stare = true;
        animator.SetFloat("Speed", 0);
    }

    void ReturnToSpawnPoint()
    {
        Debug.Log("Returning to spawn point");
        // move to spawn point
        ChaseTarget(spawnPoint.position);
    }
}
