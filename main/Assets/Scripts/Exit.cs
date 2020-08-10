using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
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
            Debug.Log("ESCAPED!");
            var currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "Level1")
            {
                Debug.Log("Level2");
                SceneManager.LoadScene("Level2");
            }
            if (currentSceneName == "Level2")
            {
                Debug.Log("Level3");
                SceneManager.LoadScene("Level3");
            }
            if (currentSceneName == "Level3")
            {
                Debug.Log("Level4");
                SceneManager.LoadScene("Level4");
            }
            if (currentSceneName == "Level4")
            {
                Debug.Log("Level5");
                SceneManager.LoadScene("Level5");
            }
            if (currentSceneName == "Level5")
            {
                Debug.Log("GAME COMPLETEEEEEEEEEEEE");
                SceneManager.LoadScene("Cutscene_Outro");
            }

        }
    }
}
