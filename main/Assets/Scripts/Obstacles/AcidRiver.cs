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
            // GameManager.Instance.PlaySFX("playerscream");
            // GameManager.Instance.PlaySFX("acidriverkill");
            //Game over sequence here and destroy player object
            // collision.gameObject.SetActive(false);
            // Destroy(collision.gameObject);
            // GameObject.Find("GameManager").GetComponent<GameManager>().RestartGame();

            // Coroutine for player death
            // need this check. Coroutine or not, several sources of the sound will be played in parallel and sound distorted
            if (playerDeathCoroutineCheck == false) StartCoroutine(PlayerDeath(collision));
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("DestroyEnemy");
            Destroy(collision.gameObject);
        }
        //Kills enemy as well
    }

    IEnumerator PlayerDeath(Collider2D collision)
    {
        Debug.Log("Acid river playing PlayerDeath coroutine");
        playerDeathCoroutineCheck = true;
        // play audio
        // stuff before restart
        GameManager.Instance.PlaySFX("playerscream");
        GameManager.Instance.PlaySFX("acidriverkill");
        // play the player death animation
        collision.gameObject.GetComponent<Animator>().SetBool("Death", true);
        // add delay before destroying player and restarting game
        yield return new WaitForSeconds(0.3f);
        // show the death canvas
        deathCanvas.GetComponent<Canvas>().enabled = true;
        yield return new WaitForSeconds(0.7f);
        // restart game
        collision.enabled = true;
        Destroy(collision.gameObject);
        playerDeathCoroutineCheck = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().RestartGame();
    }
}


