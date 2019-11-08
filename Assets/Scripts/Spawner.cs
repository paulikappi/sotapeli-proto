using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Faction faction;


    public int instanceCount = 1;
    public int spawnRounds = 1;
    public bool spawnBurst;
    public float spawnInterval = 0.2f;
    
    int spawnedCount;
    int spawnRoundCount;

    private void Start()
    {
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
            InvokeRepeating("Spawn", 0.5f, spawnInterval);
        }
        
    }   
    void SpawnAllSameTime()
    {
        if (spawnRoundCount < spawnRounds)
        {
            for (int i = 0; i < instanceCount; i++)
            {
                GameObject soldier = ObjectPooler.SharedInstance.GetPooledObject("Soldier_Green" + "(Clone)");
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

    void Spawn()
    {
        if (spawnedCount < instanceCount)
        {
            GameObject soldier = ObjectPooler.SharedInstance.GetPooledObject("Soldier_Green" + "(Clone)");
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
            CancelInvoke("Spawn");
        }
    }
}
