using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class HierarchyItem
{
    public GameObject GameObject;
    public int Count;
}

[CreateAssetMenu(fileName = "new FormationData", menuName = "Scriptable Objects/FormationData")]
public class FormationData : ScriptableObject
{
    public FactionData Faction;
    string commanderName;
    int soldierCount;
    public GameObject Commander;
    public GameObject Formation;
    public List<HierarchyItem> Subordinates;
    [SerializeField] List<FormationData> subFormationTypes;
    [SerializeField] List<FormationData> superiorFormationTypes;    

    public List<FormationData> SubFormations
        { 
            get { return subFormationTypes; } 
            private set { } 
        }
    public List<FormationData> SuperiorFormations
        { 
        get { return superiorFormationTypes; } 
         set { } 
        }
        

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
}
