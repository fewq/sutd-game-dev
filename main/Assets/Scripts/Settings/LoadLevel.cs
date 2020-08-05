using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Load(string level)
    {
        SceneManager.LoadScene(level);    
    }
}
