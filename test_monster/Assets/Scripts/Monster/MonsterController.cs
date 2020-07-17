using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private bool stare = false;

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
        if (flameInRange && !stare)
        {
            ChaseTarget(flameLocation);
        }

        else if (playerInRange)
        {
            ChasePlayer();
        }
        else if (Mathf.Abs((spawnPoint.position - transform.position).magnitude) >= 0.2f)
        {
            ReturnToSpawnPoint();
        }
        else 
        {
            animator.SetFloat("Speed", 0);
        }
    }

    public void ChaseFlame(Transform flame)
    {
        flameLocation = flame;
        flameInRange = true;
    }

    public void ChasePlayer()
    {
        ChaseTarget(Player.instance.transform);
    }

    public void ChaseTarget(Transform target)
    {
        direction = (target.position - transform.position);
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

    public void Stare()
    {
        stare = true;
        animator.SetFloat("Speed",0);
    }

    void ReturnToSpawnPoint()
    {
        // move to spawn point
        ChaseTarget(spawnPoint);

    }
}
