using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float countdown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //Collider2D objectCollider = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f), 90f);
        //Debug.Log(objectCollider);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
