using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;

    private int xVal;

    private int yVal;

    private RaycastHit2D hit;
    private BoxCollider2D walls;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        walls = GameObject.FindGameObjectWithTag("Wall").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // get inputs from user
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        xVal = (int) movement.x;
        yVal = (int) movement.y;

        if(xVal != 0){
            movement.y = 0.0f;
            yVal = 0;
            // checkMove<BoxCollider2D>(xVal, yVal);
        }

        if(yVal != 0){
            movement.x = 0.0f;
            xVal = 0;
        }

        if (xVal != 0 || yVal != 0){
            checkMove<BoxCollider2D>(xVal, yVal);
            
        }

        if(xVal == 0 && yVal == 0)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        // this is used to update the player animation
        // this is passed into the animator parameters, which is then used to update the blend tree
        
        
    }

    // void FixedUpdate()
    // {
    //     // actual movement
    //     rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    // }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected override void checkMove<T>(int x, int y)
    {
        base.checkMove<T>(x,y);

        if(Move(xVal, yVal, out hit)){
            // base.Move(xVal, yVal, out hit);
            Debug.Log("Moving");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            Debug.Log(movement.sqrMagnitude);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            Debug.Log("dX: " + movement.x + " dY: " + movement.y + " dV: " + movement.sqrMagnitude);
            
        }
    }
}
