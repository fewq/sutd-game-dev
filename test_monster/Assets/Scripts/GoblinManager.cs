using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   

        List<Vector2> list = ObjectPooler.SharedInstance.GetTransformObject();

        for (int i = 0; i < ObjectPooler.SharedInstance.PooledLength(); i++) 
        {
            GameObject goblin = ObjectPooler.SharedInstance.GetPooledObject();
            goblin.transform.position = list[i];
            goblin.SetActive(true);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
