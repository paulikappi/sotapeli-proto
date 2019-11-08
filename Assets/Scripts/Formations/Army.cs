using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Army : MonoBehaviour
{
    [SerializeField] Faction_SO faction;
    
    int companyCount;
    int squadCount;
    int soldierCount;
    
    [SerializeField] List<Company> companyList;
    [SerializeField] List<Squad> squadList;
    [SerializeField] List<Soldier> soldierList;

    GameObject armyCommander;

    private void Awake()
    {
        companyList = faction.CompanyList;
    }
}
