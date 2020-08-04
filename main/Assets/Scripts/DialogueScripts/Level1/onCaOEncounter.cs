using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCaOEncounter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Mi pan");
        if (other.CompareTag("Player")){
            Debug.Log("Mi pan");
            FindObjectOfType<Level1Cinematic>().onCaOFound();
        }
    }
}
