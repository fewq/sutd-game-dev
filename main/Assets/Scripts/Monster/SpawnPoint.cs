using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public MonsterController monsterController;
    // Start is called before the first frame update
    void Start()
    {
        monsterController = GetComponentInChildren<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("OnTriggerEnter2D SpawnPoint");
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            monsterController.Idle();
        }
    }
}
