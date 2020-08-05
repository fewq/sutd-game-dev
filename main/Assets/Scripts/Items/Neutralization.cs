using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutralization : MonoBehaviour
{
    public float countdown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
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
