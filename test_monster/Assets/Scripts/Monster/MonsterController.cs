using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Determines monster movement logic */
public class MonsterController : GenericSingletonClass<MonsterController>
{
    public Transform m_SpawnPoint;
    public Animator animator;
    public GameObject exclaimation;
    // public GameObject heartExclaimation;
    public float lookRadius = 3f;
    public float moveSpeed = 5f;
    private Rigidbody2D rigidbody;
    public bool finishBurning = false; // FOR POC, REMOVE LATER
    private Vector2 direction;

    public delegate void Detect();
    public static event Detect playerDetected;
    public static event Detect flameDetected; 
    // Start is called before the first frame update
    void Start() {}

    void Update() {}

    public void PlayerDetected()
    {

    }



    public void ChaseTarget(Transform target)
    {
        // prevents interpolation between left/right/up/down movement
        direction = (target.position - transform.position);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(lookRadius, lookRadius, lookRadius));
    }
}
