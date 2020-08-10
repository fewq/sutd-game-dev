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
    // public float moveSpeed = 5f;
    private float tileMovement;
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    private Transform flameLocation;
    private Transform playerLocation;
    public bool flameInRange { get; set; }
    //public bool playerInRange { get; set; }
    public bool playerInRange = false;
    [HideInInspector]
    public bool stare = false;

    // Variables for chasing enemy
    public Transform target;
    public float nextWaypointDistance = 3f;
    private float repeatInterval = 0.5f;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedPlayer = false;
    private bool returnToSpawn = false;
    private bool atSpawn = true;
    private Seeker seeker;
    public BoxCollider2D playerCheck;
    public BoxCollider2D flameCheck;
    public AStarAI astarAI;
    // Variables for checking for player
    public float lookRange = 5f;
    private Vector2 sizeOfRaycast;
    public int radius = 5;
    private BoxCollider2D monsterCollider;

    private bool isAlerted = false;
    private bool hasChased = false;
    private bool isDistracted = false;
    // Start is called before the first frame update
    void Start()
    {
        // move to spawn point
        transform.position = spawnPoint.position;
        // initialise variables
        flameInRange = false;
        playerInRange = false;
        rigidBody = GetComponent<Rigidbody2D>();
        monsterCollider = GetComponent<BoxCollider2D>();
        tileMovement = GameManager.Instance.gridScale.x / 10;
        astarAI = GetComponent<AStarAI>();
    }

    IEnumerator ChaseFlame(Transform flame)
    {
        if (atSpawn)
        {
            Debug.Log("ChaseFlame");
            heartExclaimation.SetActive(true);
            flameLocation = flame;
            flameInRange = true;
            atSpawn = false;
            if (isDistracted == false)
            {
                GameManager.Instance.PlaySFX("goblindistracted");
                isDistracted = true;
            }
            yield return new WaitForSeconds(1);
            //returnToSpawn = false;
            Debug.Log("Chase Flame");
            exclaimation.SetActive(false);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
            animator.SetFloat("Speed", Mathf.Max(1, direction.sqrMagnitude));
            playerInRange = true;
            target = flame;
            if (!astarAI.MoveToTarget(flame.position, "flame"))
            {
                ReturnToSpawnPoint();
            }
        }

    }

    IEnumerator ChasePlayer(Transform player)
    {

        if (player == null)
        {
            ReturnToSpawnPoint();
        }
        else
        {
            if (isAlerted == false)
            {
                GameManager.Instance.PlaySFX("goblinalerted");
                isAlerted = true;
            }
            yield return new WaitForSeconds(0.1f);
            if (flameInRange)
            {
                Debug.Log("flameInRange");
            }
            else
            {
                if (GameManager.Instance.LookForPlayer(gameObject.transform) == true)
                {

                    returnToSpawn = false;
                    Debug.Log("Chase Player");
                    animator.SetFloat("Horizontal", direction.x);
                    animator.SetFloat("Vertical", direction.y);
                    animator.SetFloat("Speed", Mathf.Max(1, direction.sqrMagnitude));
                    if (hasChased == false)
                    {
                        GameManager.Instance.PlaySFX("goblinchase");
                        hasChased = true;
                    }
                    playerInRange = true;
                    playerLocation = player;
                    target = player;
      
                    if (!astarAI.MoveToTarget(player.position, "player"))
                    {
                        ReturnToSpawnPoint();
                        
                    }


                }

            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // GameManager.Instance.PlaySFX("playerscream");
            // GameManager.Instance.PlaySFX("goblinlaugh");
            // Destroy(collision.gameObject);
            ReturnToSpawnPoint();
            //ChasePlayer(collision.gameObject.transform);
            Debug.Log("kill Player");
            collision.gameObject.GetComponent<Player>().playerDeath("goblin");
        }
        else
        {
            Debug.Log("Debugging Collision");
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().playerDeath("goblin");
        }

    }


    public void Idle()
    {
        Debug.Log("Start to idle");
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        //stare = true;
        animator.SetFloat("Speed", 0);
        playerInRange = false;
        isAlerted = false;
        isDistracted = false;
        hasChased = false;
        returnToSpawn = true;
    }

    public void ReturnToSpawnPoint()
    {
        Debug.Log("Returning to spawn point");
        playerInRange = false;
        isAlerted = false;
        isDistracted = false;
        hasChased = false;
        returnToSpawn = true;
        exclaimation.SetActive(false);
        // move to spawn point
        astarAI.MoveToTarget(spawnPoint.position, "spawnpoint");
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", Mathf.Max(1, direction.sqrMagnitude));

        //ChaseTarget(spawnPoint.position);
    }

    public void PlayerInRange(Transform player)
    {
        if (playerInRange)
        {

        }
        else
        {
            exclaimation.SetActive(true);
            StartCoroutine(ChasePlayer(player));
        }

    }
    public void FlameInRange(Transform flame)
    {
        StartCoroutine(ChaseFlame(flame));

    }

}
