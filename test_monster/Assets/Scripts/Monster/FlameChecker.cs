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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Debug.Log(favColor+"Flame");
        if (collision.gameObject.tag == favColor+"Flame")
        {
            // invoke event?
            Debug.Log("Flame in range");
        }
    }
}
