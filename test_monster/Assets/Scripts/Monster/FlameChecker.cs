using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameChecker : MonoBehaviour
{
    public enum favColors {
        Blue = 0,
        Yellow = 1,
        Red = 3,
        Orange= 4
    }
    public favColors favColor;
    private MonsterController monsterController;
    private string flameTag;

    void Start()
    {
        flameTag = favColor+"Flame";
        monsterController = GetComponentInParent<MonsterController>();
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(flameTag))
        {
            Debug.Log("Flame in range");
            monsterController.ChaseFlame(collider.gameObject.transform);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(flameTag)) 
        {
            Debug.Log("Flame no longer in range");
            monsterController.flameInRange = false;
        }
    }
}
