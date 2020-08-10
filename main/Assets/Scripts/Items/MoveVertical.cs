using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVertical : MonoBehaviour
{
    private bool floatup = true;
    
    private float y;

    void Start(){
        y = transform.position.y;
    }

    void Update(){
        if(floatup){
            StartCoroutine(FloatUp());
        }
        else if(!floatup){
            StartCoroutine(FloatDown());
        }
    }

    IEnumerator FloatUp(){
        transform.Translate(0, Time.deltaTime * (float)0.1 * 1.0f, 0);
        yield return new WaitForSeconds(1);
        floatup = false; 
    }

   IEnumerator FloatDown(){
        transform.Translate(0, Time.deltaTime * (float)0.1 * -1.0f, 0);

        yield return new WaitForSeconds(1);
        floatup = true; 
   }
}
