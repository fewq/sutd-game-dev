using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            GameManager.Instance.PlaySFX("acidriverkill");
            //Game over sequence here and destroy player object
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            
            GameObject.Find("GameManager").GetComponent<GameManager>().RestartGame();

            
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("DestroyEnemy");
            Destroy(collision.gameObject);
        }
        //Kills enemy as well
    }
}
