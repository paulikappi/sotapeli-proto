using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new FormationData", menuName = "Scriptable Objects/FormationData")]
public class FormationData : ScriptableObject
{
    [SerializeField] FactionData faction;
    [SerializeField] string commanderName;
    [SerializeField] int soldierCount;
    public List<ObjectPoolItem> formationPrefabs;
    public int hierarchyLevel;
    public List<FormationData> subFormations;
    public List<FormationData> superiorFormationList;

    List<Tuple<GameObject, int>> formationDB;
    /*
    [SerializeField] string SMG;
    [SerializeField] int SMGAmount;
    [SerializeField] string AssaultRifle;
    [SerializeField] int AssaultRifleAmount;
    [SerializeField] string rifle;
    [SerializeField] int rifleAmount;
    [SerializeField] string autoRifle;
    [SerializeField] int autoRifleAmount;
    [SerializeField] string MG;
    [SerializeField] int MGAmount;
    [SerializeField] string fieldCannon;
    [SerializeField] int fieldCannonAmount;
    [SerializeField] string ATCannon;
    [SerializeField] int ATCannonAmount;
    [SerializeField] string ATRifle;
    [SerializeField] int ATRifleAmount;
    [SerializeField] string mortar;
    [SerializeField] int mortarAmount;
    [SerializeField] string pistol;
    [SerializeField] int pistolAmount;
    */

    public string Name
    {
        get { return this.name; }
    }

    private void OnEnable()
    {
        //SetHierarchyValues();
    }

    private void OnValidate()
    {
        if (subFormations != null && subFormations.Count > 0)
        {
            soldierCount = 0;
            foreach (FormationData f in subFormations)
            {
                soldierCount += f.soldierCount;
            }
        }

    }


    FormationData GetSubFormation(FormationData formation)
    {        
        if (formation.subFormations.Count > 0)
        {
            return formation;
        }
        return null;
    }

    FormationData GetLowestSubformation (FormationData f)
    {
        if (f.subFormations.Count != 0)
        {
            int levels = f.hierarchyLevel;
            for (int i = levels; i > 0; i--)
            {                
                f = f.subFormations[0];
            }
            return f;
        }
        else return f;
    }
    

}
