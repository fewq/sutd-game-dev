using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    public LayerMask layer;
    public AudioSource playerWalkAS;
    public AudioClip playerWalkSFX;

    public CustomInputManager customInputManager;

    // private int xVal;

    // private int yVal;
    private bool playerMoving;
    private bool isPlaying;

    private EdgeCollider2D walls;

    // length of the tile to move, set to 0.1 from unity
    private float tileMovement;

 
    protected override void Start()
    {
        tileMovement = GameManager.Instance.gridScale.x / 10;
        Debug.Log(tileMovement);
        base.Start();
        rb = gameObject.GetComponent<Rigidbody2D>();
        isPlaying = false;
        playerWalkAS.clip = playerWalkSFX;
        playerWalkAS.loop = true;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    private void FixedUpdate()
    {


        // xVal = (int) (movement.x);
        // yVal = (int) (movement.y);
    }
    void Update()
    {
        movement.x = customInputManager.GetAxisRaw("Horizontal");
        movement.y = customInputManager.GetAxisRaw("Vertical");
        // get inputs from user
        if (movement.x != 0)
        {
            Debug.Log("X movement");
            movement.y = 0.0f;
            // yVal = 0;
            if (movement.x > 0)
            {
                movement.x = tileMovement;
            }
            else
            {
                movement.x = -tileMovement;
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
            }
            else
            {
                movement.y = -tileMovement;
            }

        }
        if (movement.x != 0 || movement.y != 0)
        {
            movementController(movement.x, movement.y);
            if (isPlaying == false)
            {
                playerWalkAS.Play();
                isPlaying = true;
            }

        }

        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
            playerWalkAS.Stop();
            isPlaying = false;
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
        //animator.SetFloat("Horizontal", movement.x);
        //animator.SetFloat("Vertical", movement.y);
        //animator.SetFloat("Speed", Mathf.Max(1, movement.sqrMagnitude));
        if (playerMovement(x, y, out hit))
        {
            // base.Move(xVal, yVal, out hit);
            // Debug.Log
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", Mathf.Max(1, movement.sqrMagnitude));
            Debug.Log("dX: " + movement.x + " dY: " + movement.y + " dV: " + movement.sqrMagnitude);

        }
    }


}
