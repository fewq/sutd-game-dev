using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Singleton
    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // get inputs from user
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // this is used to update the player animation
        // this is passed into the animator parameters, which is then used to update the blend tree
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        // Debug.Log("dX: " + movement.x + " dY: " + movement.y + " dV: " + movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // actual movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("enemy caught you!");
            // Time.timeScale=0f;
        }
    }
}
