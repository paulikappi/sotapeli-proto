using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool canExpand = true;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                if (GameObject.Find("Green").transform != null)
                {
                    obj.transform.parent = GameObject.Find("Green").transform;
                }
                
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }        
    }

    public void SetPoolObject(GameObject objectToPool, int amountToPool, bool canExpand)
    {
        ObjectPoolItem objToAdd = new ObjectPoolItem();
        objToAdd.objectToPool = objectToPool;
        objToAdd.amountToPool = amountToPool;
        objToAdd.canExpand = canExpand;        
        itemsToPool.Add(objToAdd);
    }

    public GameObject GetPooledObject (string name)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].name == name)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.name == name)
            {
                if (item.canExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool);
                    obj.transform.parent = GameObject.Find("Green").transform;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }            
        }
        return null;
    }    
}
