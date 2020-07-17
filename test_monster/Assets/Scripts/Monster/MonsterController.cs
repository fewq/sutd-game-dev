using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    public Transform m_SpawnPoint;
    public Animator animator;
    public GameObject exclaimation;
    // public GameObject heartExclaimation;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    private Vector2 direction;
    private Transform flameLocation;
    public bool flameInRange { get; set; }
    public bool playerInRange { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        flameInRange = false;
        playerInRange = false;
    }

    void Update()
    {
        if (flameInRange)
        {
            ChaseTarget(flameLocation);
        }

        else if (playerInRange)
        {
            ChasePlayer();
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
        transform.Translate(direction * Time.deltaTime * moveSpeed);

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

    }
}
