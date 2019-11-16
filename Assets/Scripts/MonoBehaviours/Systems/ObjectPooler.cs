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
    //[HideInInspector] 
    public List<GameObject> pooledObjects;
    //[HideInInspector] 
    public List<ObjectPoolItem> ItemsToPool;
    
    BattleController battleController;
    public static ObjectPooler SharedInstance;
    public int maxPoolItems;
    public int pooledItemsCount;

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
        if (ItemsToPool != null)
        {
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
                            pooledItemsCount++;
                            if (item.hideInHierachy)
                            {
                                obj.hideFlags = HideFlags.HideInHierarchy;
                            }
                            if (pooledObjects != null)
                            pooledObjects.Add(obj);
                        }
                    }
                }
            }
            //Debug.Log("Pooled items: " + ItemsToPool.Count);
            //ItemsToPool.Clear();
        }        
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
        /* TODO: Make work properly
         * Combine same objects together
         * One item per gameobject
        */
        List<ObjectPoolItem> tempList = new List<ObjectPoolItem>();
        tempList.Clear();

        foreach (ObjectPoolItem objItem in ItemsToPool)
        {
            if (tempList.Exists(x => x.objectToPool == objItem.objectToPool))
            {
                ObjectPoolItem tempObj = tempList.Find(x => x.objectToPool == objItem.objectToPool);
                if (tempObj != null)
                {                    
                    tempObj.AmountToPool += objItem.AmountToPool;
                }
                else
                {
                    Debug.LogError("ObjectPoolItem ERROR");
                }
            }
            else
            {
                tempList.Add(objItem);
            }               
            
        }

        //ItemsToPool.Clear();
        ItemsToPool = tempList;

        /*

        
        foreach (ObjectPoolItem oldItem in ItemsToPool)
        {
            if (oldItem != null && oldItem.objectToPool != null)
            {
                //check if the new list has already the item
                // is found add amount to pool to it
                if (newList.Exists(x => x.objectToPool.name == oldItem.objectToPool.name))
                {
                    newList.Find(x => x.objectToPool.name == oldItem.objectToPool.name).amountToPool += oldItem.amountToPool;
                    Debug.Log("found duplicate " + oldItem.objectToPool.name + " amount is now: " + oldItem.amountToPool);
                }
                else
                {
                    
                    newList.Add(oldItem);
                    Debug.Log("Added: " + oldItem.objectToPool.name);
                }
            }
        }

        ItemsToPool = newList;
        ItemsToPool.TrimExcess();
        Debug.Log("Optimized Pool List");
        */
        foreach (ObjectPoolItem item in ItemsToPool)
        {
            Debug.Log("Pooling " + item.AmountToPool + " " + item.objectToPool );
        }
        

        PoolObjects();
    }
    void ResizePoolItemList()
    {
        
    }
}
