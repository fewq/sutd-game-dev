using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem {
        public GameObject prefab;

        public Transform transform;

    }

    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    private List<GameObject> pooledObjects;

    private List<Transform> pooledObjectsTransform;

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        pooledObjectsTransform = new List<Transform>();

        foreach (ObjectPoolItem item in itemsToPool)
        {   
            GameObject goblin = (GameObject)Instantiate(item.prefab);
            goblin.SetActive(false);
            pooledObjects.Add(goblin);
            pooledObjectsTransform.Add(item.transform);

        }
    }
    
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
        if (!pooledObjects[i].activeInHierarchy)
        {
        return pooledObjects[i];
        }
        }
        return null;
    }
    
    public int PooledLength()
    {
        return pooledObjects.Count;
    }

    public List<Transform> GetTransformObject()
    {
        return pooledObjectsTransform;
    }
}
