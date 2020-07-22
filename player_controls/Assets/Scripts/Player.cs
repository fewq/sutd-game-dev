using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    public LayerMask layer;


    // private int xVal;

    // private int yVal;
    private bool playerMoving;

    private EdgeCollider2D walls;

    // length of the tile to move, set to 0.1 from unity
    public float tileMovement;

    // Vector of last direction the player was facing, defaults to zero or (0,0,0)
    // useful for placing objects
    public Vector3 lastDirection;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        lastDirection = Vector3.zero;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {


        // xVal = (int) (movement.x);
        // yVal = (int) (movement.y);
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        // get inputs from user

        if (movement.x != 0)
        {
            Debug.Log("X movement");
            movement.y = 0.0f;
            // yVal = 0;
            if (movement.x > 0)
            {
                movement.x = tileMovement;
                lastDirection = Vector2.right; // set the last direction the player was facing
            }
            else
            {
                movement.x = -tileMovement;
                lastDirection = Vector2.left; // set the last direction the player was facing
            }
            // movement.x = 0.3f;
        }

        if (movement.y != 0)
        {
            Debug.Log("Y movement");
            movement.x = 0.0f;
            // xVal = 0;
            if (movement.y > 0)
            {
                movement.y = tileMovement;
                lastDirection = Vector2.up; // set the last direction the player was facing
            }
            else
            {
                movement.y = -tileMovement;
                lastDirection = Vector2.down; // set the last direction the player was facing
            }

        }

        if (movement.x != 0 || movement.y != 0)
        {
            movementController(movement.x, movement.y);

        }

        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        // this is used to update the player animation
        // this is passed into the animator parameters, which is then used to update the blend tree

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    public void movementController(float x, float y)
    {
        // base.checkMove<T>(x,y);
        RaycastHit2D hit;
        if (playerMovement(x, y, out hit))
        {
            // base.Move(xVal, yVal, out hit);
            // Debug.Log
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", Mathf.Max(1, movement.sqrMagnitude));
            Debug.Log("dX: " + movement.x + " dY: " + movement.y + " dV: " + movement.sqrMagnitude);


        }
        else
        {
            Debug.Log("Cant move");
        }

    }


}
