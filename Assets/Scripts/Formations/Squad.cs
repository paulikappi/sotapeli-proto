using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Squad : MonoBehaviour
{
    [SerializeField] string squadName;
    [SerializeField] GameObject squadLeader;
    [SerializeField] int soldierCount;
    [SerializeField] Faction faction;
    [SerializeField] Army army;
    [SerializeField] Company company;

    [SerializeField] GameObject spawner;

    [SerializeField] GameObject detectedSoldier;
    Ray ray;
    RaycastHit hit;
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
