using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public MonsterController monsterController;
    private bool playerInRange;
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Look for Player");
            monsterController.PlayerInRange(GameManager.Instance.ReturnPlayerPosition());
            //if (GameManager.Instance.LookForPlayer(gameObject.transform) == true)
            //{
            //    Debug.Log(monsterController.flameInRange);
            //    if (!monsterController.flameInRange)
            //    {
            //        Debug.Log("PLAYER FOUND HUE HUE");
            //        //monsterController.playerInRange = true;
            //        monsterController.PlayerInRange(GameManager.Instance.ReturnPlayerPosition());
            //    }

            //    //monsterController.ChaseTarget(collider.transform.position);
            //}
            //ChasePlayer(collision.gameObject.transform);
            //Debug.Log("Chase Player");
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
