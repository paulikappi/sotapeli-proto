using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool canExpand = true;
    public bool hideInHierachy = false;
    public int maxCount;

    public List<GameObject> subordinates;
    public List<GameObject> commanders;
    public FactionData faction;
}

public class ObjectPooler : MonoBehaviour
{
    public List<ObjectPoolItem> tempList;
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;
    
    public BattleController battleController;
    public static ObjectPooler SharedInstance;
    public GameManager gameManager;
    public int maxPoolItems;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {   
        gameManager = FindObjectOfType<GameManager>();
        battleController = FindObjectOfType<BattleController>();
    }

    public void PoolObjects()
    {
        if (itemsToPool != null)
        {
            foreach (ObjectPoolItem item in itemsToPool)
            {
                if (item != null)
                {
                    for (int i = 0; i < item.amountToPool; i++)
                    {
                        if (item.objectToPool != null)
                        {
                            GameObject obj = Instantiate(item.objectToPool);
                            obj.SetActive(false);
                            if (item.hideInHierachy)
                            {
                                obj.hideFlags = HideFlags.HideInHierarchy;
                            }                            
                            pooledObjects.Add(obj);
                        }
                    }
                }
            }
            Debug.Log("Pooled item types: " + itemsToPool.Count);
        }        
    }

    public void AddPoolItem(ObjectPoolItem item)
    {
        itemsToPool.Add(item);
    }

    public void AddPoolItemList(List<ObjectPoolItem> list)
    {
        foreach (ObjectPoolItem item in list)
        {
            itemsToPool.Add(item);
        }
    }
    public void AddGameObject(GameObject item)
    {
        ObjectPoolItem poolitem = new ObjectPoolItem();
        poolitem.amountToPool = 1;
        poolitem.canExpand = false;
        poolitem.objectToPool = item;

        itemsToPool.Add(poolitem);
    }

    public void SetGameObjectToPooledList(GameObject item)
    {
        pooledObjects.Add(item);
    }

    public GameObject ActivatePooledObjectByName (string name)
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
                    GameObject obj = Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }            
        }
        return null;
    }
    public void OptimizePoolList()
    {        
        List<ObjectPoolItem> tempList = new List<ObjectPoolItem>();
        
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item != null && item.objectToPool != null)
            {
                if (tempList.Exists(x => x.objectToPool.name == item.objectToPool.name))
                {
                    ObjectPoolItem tempItem = tempList.Find(x => x.objectToPool.name == item.objectToPool.name);
                    tempItem.amountToPool++;
                    Debug.Log("found duplicate " + tempItem.objectToPool.name);         
                }
                else
                {
                    tempList.Add(item);
                    Debug.Log("Added: " + item.objectToPool.name);
                }
            }
        }

        itemsToPool = tempList;

        itemsToPool.TrimExcess();

        Debug.Log("Optimized Pool List");
        
        PoolObjects();
    }
}
