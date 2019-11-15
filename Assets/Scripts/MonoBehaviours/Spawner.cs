using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    /*
    [SerializeField] FactionData faction;
    [SerializeField] int instanceCount = 1;
    [SerializeField] int spawnRounds = 1;
    [SerializeField] bool spawnBurst;
    [SerializeField] float spawnInterval = 0.2f;
    */

    Tuple<GameObject, int> spawnTuple;
    public Queue<Tuple<GameObject, int>> spawnQueue; 

    WarObject soldierData;
    public int maxSpawnAmount;
    bool readyStatus;
    [SerializeField] GameObject nextSpawning;
    [SerializeField] int nextSpawnAmount;
    //int spawnedCount;
    //int spawnRoundCount;

    private void Start()
    {        
        if (spawnQueue != null && spawnQueue.Count > 0)
        {
            spawnTuple = spawnQueue.Peek();

            nextSpawning = spawnTuple.Item1;
            nextSpawnAmount = spawnTuple.Item2;
        }
        /*

        if (spawnBurst)
        {
            if (spawnRounds > 1)
            {
                InvokeRepeating("SpawnAllSameTime", 0.5f, spawnInterval);
            }
            else
            {
                Invoke("SpawnAllSameTime", 0.5f);
            }            
        }
        else
        {
            InvokeRepeating("SpawnNext", 0.5f, spawnInterval);
        }
        */                
    }
    public void SpawnNext()
    {        
        if (spawnQueue != null && spawnQueue.Count > 0)
        {
            Debug.Log("SpawnNext");
            
            SpawnAmount(spawnQueue.Dequeue());

            if (spawnQueue.Count > 0)
            {
                spawnTuple = spawnQueue.Peek();

                nextSpawning = spawnTuple.Item1;
                nextSpawnAmount = spawnTuple.Item2;
            }
        }
        else 
        {
            Debug.Log("Queue empty");
        }
    }

    public void AddToSpawnQueue(GameObject obj, int spawnAmount)
    {
        Tuple<GameObject, int> qItem = new Tuple <GameObject, int>(obj, spawnAmount);
        spawnQueue.Enqueue(qItem);
    }

    void SpawnAmount(Tuple<GameObject, int> tuple)
    {
        readyStatus = false;
        
        GameObject obj = tuple.Item1;
        int spawnCount = tuple.Item2;

        for (int i = 0 ; i < spawnCount; i++)
        {
            obj = FindObjectOfType<ObjectPooler>().ActivatePooledObjectByName(obj.name + "(Clone)");
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            obj.SetActive(true);
        }

        readyStatus = true;
    }

    public bool GetReadyStatus()
    {
        return readyStatus;
    }

    /*
    void SpawnAllSameTime()
    {
        if (spawnRoundCount < spawnRounds)
        {
            for (int i = 0; i < instanceCount; i++)
            {
                GameObject soldier = ObjectPooler.SharedInstance.ActivatePooledObjectByName("SoldierGreen" + "(Clone)");
                soldierData = soldier.GetComponent<WarObject>();
                if (soldier != null)
                {
                    soldier.transform.position = transform.position;
                    soldier.transform.rotation = transform.rotation;
                    soldier.SetActive(true);
                }
            }
            spawnRoundCount++;
        }
        else
        {
            CancelInvoke("SpawnAllSameTime");
        }
        
    }

    void SpawnNext()
    {
        if (spawnedCount < instanceCount)
        {
            GameObject soldier = ObjectPooler.SharedInstance.ActivatePooledObjectByName("Soldier_Green" + "(Clone)");
            if (soldier != null)
            {
                soldier.transform.position = transform.position;
                soldier.transform.rotation = transform.rotation;
                soldier.SetActive(true);
            }
            spawnedCount++;
        }
        else
        {
            CancelInvoke("SpawnNext");
        }
    }
    */
}
