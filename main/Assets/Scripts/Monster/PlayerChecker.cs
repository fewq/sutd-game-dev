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
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Look for Player");
            monsterController.PlayerInRange(GameManager.Instance.ReturnPlayerPosition());

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
