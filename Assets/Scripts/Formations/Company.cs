using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Company : MonoBehaviour
{
    string companyName;
    GameObject objective;
    GameObject spawnArea;
    GameObject companyCommander;
    Faction faction;
    Army army;
    int squadCount;
    int soldierCount;
}
