using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRiver : MonoBehaviour
{

    private bool playerDeathCoroutineCheck;
    public Canvas deathCanvas;
    // Start is called before the first frame update
    void Start()
    {
        playerDeathCoroutineCheck = false;
        deathCanvas = GameObject.Find("DeathCanvas").GetComponent<Canvas>();
        deathCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DestroyPlayer");
            collision.gameObject.GetComponent<Player>().playerDeath("acid");

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("DestroyEnemy");
            Destroy(collision.gameObject);
            GameManager.Instance.PlaySFX("goblindeath");
        }
    }


}


