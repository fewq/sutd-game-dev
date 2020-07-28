using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<GameManager> : MonoBehaviour where GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (GameManager)FindObjectOfType(typeof(GameManager));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                        // Make instance persistent.
                        //DontDestroyOnLoad(singletonObject);
                        //Debug.Log("New GameManager Made");
                    }
                }

                return m_Instance;
            }
        }
    }


    private void OnApplicationQuit()
    {

    }


    private void OnDestroy()
    {

    }

}
