using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    public AIPath aIPath;
    public Transform spawnPoint;
    public Animator animator;
    public GameObject exclaimation;
    public GameObject heartExclaimation;
    public float moveSpeed = 5f;
    public bool useAiPathTest; // FOR DEMO, TO REMOVE
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Transform flameLocation;
    public bool flameInRange { get; set; }
    public bool playerInRange { get; set; }
    public bool stare = false;

    // Start is called before the first frame update
    void Start()
    {
        // move to spawn point
        transform.position = spawnPoint.position;
        // initialise variables
        flameInRange = false;
        playerInRange = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
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
        if (useAiPathTest)
        {
            Debug.DrawLine(transform.position, aIPath.GetNextStep(transform.position), Color.white);
            ChaseTarget(aIPath.GetNextStep(transform.position));
        }
        else
        {
            ChaseTarget(Player.instance.transform.position);
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
        // move to spawn point
        ChaseTarget(spawnPoint.position);

    }
}
