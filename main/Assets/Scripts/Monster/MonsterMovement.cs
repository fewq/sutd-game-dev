using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MonsterMovement : MonoBehaviour
{
    public List<Vector3> movementList = new List<Vector3>();
    private bool toMove = false;
    private GameObject monster;
    public Tilemap ground;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        //monster = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        //if (toMove == true)
        //{
        if (shouldWeMoveAlongPath() == true)
        {
            moveAlongPath();
        }

    }
    
    public void SetMovement(List<Vector3> path)
    {
        movementList = path;
        toMove = true;
        //StartCoroutine(Move());
    }
    //IEnumerator Move()
    //{
    //    WaitForSeconds wait = new WaitForSeconds(2f);
    //    while (movementList.Count != 0)
    //    {
    //        if (toMove)
    //        {
    //            if (movementList.Count == 0)
    //            {
    //                toMove = false;
    //            }
    //            else
    //            {
    //                transform.position = movementList[0];
    //                movementList.RemoveAt(0);
    //                Debug.Log("Remaining elements left in movementlist: "+ movementList.Count);
    //                //Debug.Log();
                
    //            }
    //        }
    //        yield return new WaitForSeconds(2f);

    //    }

    //    yield return null;
    //}
    bool shouldWeMoveAlongPath()
    {
        if (movementList.Count > 0)
        {
            return true;
        }
        else
        {
            toMove = false;
            return false;
        }
    }
    void moveAlongPath()
    {
        if(Vector3.Distance(transform.position, movementList[counter]) > 0.5f){
            Vector3 dir = movementList[counter] - transform.position;
            transform.Translate(dir * 5 * Time.deltaTime);
        }
        else
        {
            if (counter < movementList.Count - 1)
            {
                counter++;
            }
        }
    }

}
