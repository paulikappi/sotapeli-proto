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
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;
    
    public BattleController battleController;
    public ObjectPooler pooler;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleController = FindObjectOfType<BattleController>();
        pooler = FindObjectOfType<ObjectPooler>();

        Invoke("PoolObjects", 1);
    }

    public void PoolObjects()
    {
        pooledObjects = new List<GameObject>();
        if (itemsToPool != null)
        {            
            Debug.Log("Pool");

            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    if (item.objectToPool != null)
                    {
                        GameObject obj = (GameObject)Instantiate(item.objectToPool);

                        obj.SetActive(false);
                        pooledObjects.Add(obj);
                    }                    
                }
            }            
        }        
    }



    /*
    public void PoolSubFormations()
    {
        if (pooler != null)
        {            
            foreach (ObjectPoolItem g in formation.formationPrefabs)
            {
                if (g.objectToPool.GetComponent<WarObject>() != null)
                {
                    WarObject war = g.objectToPool.GetComponent<WarObject>();
                    war.commandingFormations.Add(this);
                }

                pooler.AddPoolObject(g);
            }
            if (subFormations.Count > 0)
            {
                foreach (FormationData subFormation in subFormations)
                {
                    subFormation.PoolSubFormations();
                    subFormation.superiorFormationList.Add(this);
                }
            }
            PoolObjects();
        }
        else
        {
            Debug.LogError(this.name + " missing pooler");
        }
    }*/

    public void AddPoolObjectList(List<ObjectPoolItem> list)
    {
        foreach (ObjectPoolItem item in list)
        {
            itemsToPool.Add(item);
        }
    }

    public void AddPoolObject(ObjectPoolItem item)
    {
        itemsToPool.Add(item);
    }

    public GameObject GetPooledObjectByName (string name)
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
