using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Blank Faction", menuName = "Scriptable Objects/Faction", order = 1)]
public class Faction_SO : ScriptableObject
{
    [SerializeField] string factionName;
    [SerializeField] string commanderName;
    [SerializeField] int soldierCount;
    [SerializeField] int squadCount;
    [SerializeField] int companyCount;
    [SerializeField] int armyCount;
    [SerializeField] Material factionMaterial;

    [SerializeField] List<Army> armyList = new List<Army>();
    [SerializeField] List<Company> companyList = new List<Company>();
    [SerializeField] List<Squad> squadList = new List<Squad>();
    [SerializeField] List<Soldier> soldierList = new List<Soldier>();
        
    public string CommanderName
    {
        get 
        { 
            return commanderName;  
        }
    }
    public List<Army> ArmyList
    {
        get
        {
            return armyList;
        }
    }
    public List<Company> CompanyList
    {
        get
        {
            return companyList;
        }
    }
}
