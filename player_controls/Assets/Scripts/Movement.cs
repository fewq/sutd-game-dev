using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb2d;
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
    // Start is called before the first frame update
    // public bool Move(float x, float y, out RaycastHit2D hit){
    //     Vector2 start_position = transform.position;

    //     Vector2 end_position = start_position + new Vector2(x, y);

    //     edgeCollider.enabled = false; //set current object's collider to be false so that it doesnt intefere with line


    //     hit = Physics2D.Linecast(start_position, end_position, layerMask);

    //     edgeCollider.enabled = true;

    //     if(hit.transform == null){
    //         StartCoroutine(calculateMovement(end_position));
    //         return true;
    //     }
    //     return false;
    // }

    IEnumerator calculateMovement(Vector3 destination){
        float sqrRemainingDistance = (transform.position - destination).sqrMagnitude;

        while(sqrRemainingDistance > float.Epsilon){
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, destination, 10f);
            rb2d.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - destination).sqrMagnitude;
            yield return null;
        }
    }

    // protected virtual void checkMove <T> (int x, int y) where T:Component{
    //     RaycastHit2D hit;

    //     bool move = Move(x, y, out hit);
    //     if(hit.transform == null){
    //         return;
    //     }

    //     T objectCollider = hit.transform.GetComponent<T>();

    //     if(!move && objectCollider != null){
    //         Debug.Log("Cant move!");
    //         //change to something else maybe
    //     }

    // }
}
