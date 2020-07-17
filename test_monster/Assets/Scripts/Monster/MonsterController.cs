using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    public Transform m_SpawnPoint;

    public GameObject gameobject;
    public Animator animator;
    public float lookRadius = 3f;
    public float moveSpeed = 5f;
    public MonsterType.Monsters monsterType;
    public Transform torch;
    Transform player;
    private Rigidbody2D rigidbody;
    private float playerDistance;
    private float torchDistance; // TODO: account for multiple torches
    private bool distracted = false;
    public bool finishBurning = false; // FOR POC, REMOVE LATER
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.player.transform;
        rigidbody = GetComponent<Rigidbody2D>();

        GameObject child = gameobject.transform.GetChild(0).gameObject;

        child.SetActive(false);

    }

    void Update()
    {
        // TODO
        // check transforms of all relevant torches
        // make torch send an event when torch finishes burning
        if (!finishBurning)
        {
            torchDistance = Vector2.Distance(torch.position, transform.position);
            if (torchDistance <= lookRadius)
            {
                WatchFlame();
            }
        }
        if (finishBurning)
        {
            moveSpeed = 5f; // reset movespeed
            distracted = false; // reset distracted
            ChaseTarget(m_SpawnPoint); // return to spawn point
        }
        if (!distracted)
        {
            playerDistance = Vector2.Distance(player.position, transform.position);

            if (playerDistance <= lookRadius)
            {
                ChaseTarget(player);
                GameObject child = gameobject.transform.GetChild(0).gameObject;
                child.SetActive(true);
            }
            
            else {
                GameObject child = gameobject.transform.GetChild(0).gameObject;
                child.SetActive(false);

            }
        }
    }

    void WatchFlame()
    {
        distracted = true;
        moveSpeed = 5f;
        ChaseTarget(torch);
    }

    void ChaseTarget(Transform target)
    {
        // direction = normalize(playerPos - enemyPos);
        // playerPos = playerPos + direction * velocity;
        // prevents diagonal movement

        direction = (target.position - transform.position);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
        {
            direction.y = 0f;
        } else {
            direction.x = 0f;
        }
        direction.Normalize();
        transform.Translate(direction*Time.deltaTime*moveSpeed);

        FacePlayer(direction);
        // rigidbody.MovePosition(rigidbody.position + direction * moveSpeed * Time.deltaTime);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    void FacePlayer(Vector2 direction)
    {
        // TODO: update monster animation
        // animator.SetFloat("Horizontal", movement.x);
        // animator.SetFloat("Vertical", movement.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Kill");
        }
    }
}
