using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = GameManager.Instance.gridScale;

    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        
        if (countdown <= 0f)
        {
            Debug.Log("EKUSUPLOSION!");
            //Change GameManager to singleton
            //FindObjectOfType<GameManager>().Explode(transform.position);
            GameManager.Instance.Explode(transform.position);
            
            Destroy(gameObject);
        }
    }
}
