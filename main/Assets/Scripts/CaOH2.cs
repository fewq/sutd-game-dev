using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaOH2 : MonoBehaviour
{
    public float countdown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f)
        {
            Debug.Log("Neutralize!");
            //Change GameManager to singleton
            FindObjectOfType<GameManager>().Neutralize(transform.position);
            Destroy(gameObject);
        }

    }
}
