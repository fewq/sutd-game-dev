using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   

        List<Transform> list = ObjectPooler.SharedInstance.GetTransformObject();

        for (int i = 0; i < ObjectPooler.SharedInstance.PooledLength(); i++) 
        {
            GameObject goblin = ObjectPooler.SharedInstance.GetPooledObject();
            goblin.transform.position = new Vector3(list[i].position.x, list[i].position.y, 0);
            goblin.SetActive(true);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
