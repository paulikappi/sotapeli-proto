using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int AmountToPool = new int();

    public bool canExpand = true;
    public bool hideInHierachy = false;
    public int maxCount;

    public List<GameObject> subordinates;
    public List<GameObject> commanders;
    public FactionData faction;
}

public class ObjectPooler : MonoBehaviour
{
    List<ObjectPoolItem> tempList;
    [HideInInspector] 
    public List<GameObject> pooledObjects;
    [HideInInspector] 
    public List<ObjectPoolItem> ItemsToPool;
    
    BattleController battleController;
    public static ObjectPooler SharedInstance;
    public int maxPoolItems;
    public int pooledItemsCount;
    public bool hideObjectsFromHierarchy;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {   
        battleController = BattleController.SharedInstance;
    }

    public void PoolObjects()
    {
        //Debug.Log("Pooling items: " + pooledItemsCount);
        foreach (ObjectPoolItem item in ItemsToPool)
        {
            if (item != null)
            {
                for (int i = 0; i < item.AmountToPool; i++)
                {
                    if (item.objectToPool != null)
                    {
                        GameObject obj = Instantiate(item.objectToPool);
                        obj.name = item.objectToPool.name;
                        obj.SetActive(false);
                        if (item.hideInHierachy || hideObjectsFromHierarchy)
                        {
                            obj.hideFlags = HideFlags.HideInHierarchy;
                        }
                        if (pooledObjects != null)
                            pooledObjects.Add(obj);
                    }
                }
            }
        }
        
        //Debug.Log(pooledItemsCount);
        //Debug.Log("Pooled items: " + pooledItemsCount);

    }

    public void AddPoolItem(ObjectPoolItem item)
    {
        if (ItemsToPool != null)
        ItemsToPool.Add(item);
    }

    public void AddPoolItemList(List<ObjectPoolItem> list)
    {
        foreach (ObjectPoolItem item in list)
        {
            ItemsToPool.Add(item);
        }
    }
    public void AddGameObject(GameObject item)
    {
        ObjectPoolItem poolitem = new ObjectPoolItem();
        poolitem.AmountToPool = 1;
        poolitem.canExpand = false;
        poolitem.objectToPool = item;
        if (ItemsToPool != null)
        {
            ItemsToPool.Add(poolitem);
        }        
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
        foreach (ObjectPoolItem item in ItemsToPool)
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
        //Debug.Log("Pooling unoptimized: " + ItemsToPool.Count);
        //Debug.Log("Optimizing pool list");

        List<ObjectPoolItem> tempList = new List<ObjectPoolItem>();
        tempList.Clear();

        foreach (ObjectPoolItem objItem in ItemsToPool)
        {
            //Debug.Log("Checking poolitem: " + objItem.objectToPool.name);

            if (tempList.Exists(x => x.objectToPool == objItem.objectToPool))
            {
                ObjectPoolItem tempObj = tempList.Find(x => x.objectToPool == objItem.objectToPool);
                if (tempObj != null)
                {
                    //Debug.Log("Found, Adding: " + tempObj.objectToPool.name);
                    tempObj.AmountToPool += objItem.AmountToPool;
                    pooledItemsCount += objItem.AmountToPool;
                }
            }
            else
            {
                //Debug.Log("!!Not found, Created: " + objItem.objectToPool.name);
                tempList.Add(objItem);
                pooledItemsCount += objItem.AmountToPool;
            }
        }

        ItemsToPool = tempList;
        //Debug.Log("Pooling optimized: " + ItemsToPool.Count);
        
        if (pooledItemsCount > maxPoolItems)
        {
            ResizePoolItemList();
        }
        else
        {
            PoolObjects();
        }        
    }

    void ResizePoolItemList()
    {
        //Debug.Log("OBJECT POOLER: Big amount of pooled items, reducing...: " + pooledItemsCount);
        float items = pooledItemsCount;
        float max = maxPoolItems;
        float poolFactor = items / max;

        //Debug.Log("Pool divide factor is: " + poolFactor.ToString());
        //Debug.Log("item count old: " + pooledItemsCount);
        pooledItemsCount = 0;
        foreach (ObjectPoolItem item in ItemsToPool)
        {            
            //Debug.Log("item count old: " + item.AmountToPool);
            item.AmountToPool = Convert.ToInt16(item.AmountToPool / poolFactor);
            if (item.AmountToPool < 1)
            {
                item.AmountToPool = 1;
            }
            //Debug.Log("item count new: " + Convert.ToInt16(item.AmountToPool / poolFactor));
            pooledItemsCount += item.AmountToPool;
        }
        //Debug.Log("OBJECT POOLER: New smaller item count is: " + pooledItemsCount);
        PoolObjects();
    }
}
