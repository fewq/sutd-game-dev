using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameChecker : MonoBehaviour
{
    public enum favColors
    {
        Blue = 0,
        Yellow = 1,
        Red = 3,
        Orange = 4,
        Purple = 5
    }
    public favColors favColor;
    private MonsterController monsterController;
    private string flameTag;
    private bool chasing;

    void Start()
    {
        flameTag = favColor+"Flame";
        Debug.Log(flameTag);
        monsterController = GetComponentInParent<MonsterController>();
    }


    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    Debug.Log("FlameCheck");
    //    Debug.Log(collider.tag);
    //    Debug.Log(collider.name);
    //    if (collider.gameObject.CompareTag(flameTag))
    //    {
    //        Debug.Log("Flame in range");

    //        if (GameManager.Instance.LookForFlame(gameObject.transform) == true)
    //        {
    //            Debug.Log("FLAME FOUND HUE HUE");
    //            monsterController.flameInRange = true;
    //            monsterController.FlameInRange(collider.transform);
    //            monsterController.ChaseTarget(collider.transform.position);
    //        }
    //    }
    //}

    void OnTriggerStay2D(Collider2D other)
    {
        // make sure unlit torches that are in range get registered when they're lit
        //if (!chasing && other.gameObject.CompareTag("UnlitTorch"))
        //{
        //    other.gameObject.GetComponent<Rigidbody2D>().WakeUp();
        //}
        //else if (!chasing && other.gameObject.CompareTag(flameTag))
        //{
        //    //chasing = true;
        //    Debug.Log("Flame in range");
        //    monsterController.FlameInRange(other.gameObject.transform);
        //}
        if (other.gameObject.CompareTag(flameTag))
        {
            Debug.Log("Flame in range");

            if (GameManager.Instance.LookForFlame(gameObject.transform) == true)
            {
                Debug.Log("FLAME FOUND HUE HUE");
                monsterController.flameInRange = true;
                monsterController.FlameInRange(other.transform);
                
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(flameTag))
        {
            Debug.Log("Flame no longer in range");
            monsterController.flameInRange = false;
            monsterController.heartExclaimation.SetActive(false);
            monsterController.ReturnToSpawnPoint();
        }
    }
}
