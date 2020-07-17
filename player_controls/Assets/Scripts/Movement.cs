using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2d;

    public List<string> itemNames = new List<string>();
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    public LayerMask layerMask;

    private float inverseMoveTime;

    public float moveTime = 0.1f;
    protected virtual void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }
    public bool playerMovement(float x, float y, out RaycastHit2D hit){
        
        var boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false; //set current object's collider to be false so that it doesnt intefere with line
        

        hit = Physics2D.Linecast(transform.position, transform.position + new Vector3(x,y), layerMask);

        boxCollider.enabled = true;

        if(hit.transform == null || hit.transform.tag == "Item"){
            rb2d.MovePosition(transform.position + new Vector3(x,y));
            return true;
        }
        return false;
    }

}
