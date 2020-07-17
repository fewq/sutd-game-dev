using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    private MonsterController monsterController;
    private bool playerInRange;
    void Start()
    {
        monsterController = GetComponentInParent<MonsterController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            monsterController.playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            monsterController.playerInRange = false;
        }
    }
}
