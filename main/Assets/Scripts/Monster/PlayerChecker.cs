﻿using System.Collections;
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
            Debug.Log("Player in range");
            monsterController.target = collider.gameObject.transform;
            monsterController.playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player out of range");
            monsterController.playerInRange = false;
            monsterController.exclaimation.SetActive(false);
        }
    }
}
