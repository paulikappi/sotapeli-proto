using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool = 1;
    public bool canExpand = true;

    public List<GameObject> subordinates;
    public List<GameObject> commanders;
    public FactionData faction;
}

public class ObjectPooler : MonoBehaviour
{    
    public List<GameObject> pooledObjects = new List<GameObject>();
    public List<ObjectPoolItem> itemsToPool;
    
    public BattleController battleController;
    public static ObjectPooler SharedInstance;
    public GameManager gameManager;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        battleController = FindObjectOfType<BattleController>();
        

        Invoke("PoolObjects", 0.5f);
    }

    public void PoolObjects()
    {        
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
                        if (obj.GetComponent<WarObject>() == true)
                        {
                            WarObject objData = obj.GetComponent<WarObject>();
                            objData.Faction = item.faction;
                        }
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
        if (objectPooler != null)
        {            
            foreach (ObjectPoolItem g in formation.Subordinates)
            {
                if (g.objectToPool.GetComponent<WarObject>() != null)
                {
                    WarObject war = g.objectToPool.GetComponent<WarObject>();
                    war.commandingFormations.Add(this);
                }

                objectPooler.AddGameObject(g);
            }
            if (SubFormationTypes.Count > 0)
            {
                foreach (FormationData subFormation in SubFormationTypes)
                {
                    subFormation.PoolSubFormations();
                    subFormation.superiorFormationList.Add(this);
                }
            }
            PoolObjects();
        }
        else
        {
            Debug.LogError(this.name + " missing objectPooler");
        }
    }*/
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
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }            
        }
        return null;
    }
}
