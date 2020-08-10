using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChecker : MonoBehaviour
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
        flameTag = favColor+"Light";
        Debug.Log(flameTag);
        monsterController = GetComponentInParent<MonsterController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
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
