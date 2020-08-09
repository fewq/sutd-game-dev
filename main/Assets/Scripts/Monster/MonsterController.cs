using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

/* Determines monster movement logic */
public class MonsterController : MonoBehaviour
{
    private Vector3 ogPosition;
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
        
        //transform.position = spawnPoint.position;
        //transform.localScale = GameManager.Instance.gridScale;
        // initialise variables
        flameInRange = false;
        playerInRange = false;
        rigidBody = GetComponent<Rigidbody2D>();
        monsterCollider = GetComponent<BoxCollider2D>();
        ogPosition = transform.position;
        //transform.localScale = new Vector3(GameManager.Instance.gridScale.x / 10, GameManager.Instance.gridScale.y / 10); 
        tileMovement = GameManager.Instance.gridScale.x / 10;
        //monsterMovement = GetComponent<MonsterMovement>();
        astarAI = GetComponent<AStarAI>();
    }

    IEnumerator ChaseFlame(Transform flame)
    {
        if (!returnToSpawn)
        {
            Debug.Log("ChaseFlame");
            heartExclaimation.SetActive(true);
            flameLocation = flame;
            flameInRange = true;
            if (isDistracted == false)
            {
                GameManager.Instance.PlaySFX("goblindistracted");
                isDistracted = true;
            }
            yield return new WaitForSeconds(2);
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
            yield return new WaitForSeconds(0.2f);
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
                    //if (path == null)
                    //{
                    //    Debug.Log("PATH IS NULL");
                    //    yield break;
                    //}
                    if (hasChased == false)
                    {
                        GameManager.Instance.PlaySFX("goblinchase");
                        hasChased = true;
                    }
                    playerInRange = true;
                    playerLocation = player;
                    target = player;
                    //List<Vector3> path = GameManager.Instance.PathFind(transform,player);
                    //Debug.Log("Path found is");
                    //Debug.Log(path);
                    //for (int i = 0; i < path.Count; i++)
                    //{
                    //    Debug.Log(path[i].x);
                    //    Debug.Log(path[i].y);
                    //}
                    //monsterMovement.SetMovement(path);
                    astarAI.CancelLastPath();
                    if (!astarAI.MoveToTarget(player.position, "player"))
                    {
                        ReturnToSpawnPoint();
                        //exclaimation.SetActive(false);
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
    public void Idle()
    {
        Debug.Log("Start to idle");
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        //stare = true;
        animator.SetFloat("Speed", 0);
    }

    public void ReturnToSpawnPoint()
    {
        Debug.Log("Returning to spawn point");
        playerInRange = false;
        isAlerted = false;
        isDistracted = false;
        hasChased = false;
        // move to spawn point
        astarAI.MoveToTarget(ogPosition, "spawnpoint");
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(transform.position, new Vector3(lookRange,lookRange,lookRange));
    //}
}
