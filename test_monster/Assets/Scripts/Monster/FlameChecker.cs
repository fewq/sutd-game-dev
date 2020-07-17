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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(favColor+"Flame"))
        {
            // invoke event?
            Debug.Log("Flame in range");
        }
    }
}
