using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "new Formation", menuName = "Scriptable Objects/Formation")]
public class Formation : ScriptableObject
{
    [SerializeField] Faction faction;
    [SerializeField] string commanderName;
    [SerializeField] int soldierCount;
    public List<ObjectPoolItem> formationPrefabs;
    public int hierarchyLevel;
    public List<Formation> subFormations;
    public List<Formation> superiorFormationList;
    [SerializeField] ObjectPooler pooler;
    [SerializeField] GameManager gameManager;
    [SerializeField] BattleController battleController;    


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
    
    void OnEnable()
    {        
        gameManager = FindObjectOfType<GameManager>();
        battleController = FindObjectOfType<BattleController>();
        pooler = FindObjectOfType<ObjectPooler>();
    }

    private void OnValidate()
    {
        if (subFormations != null && subFormations.Count > 0)
        {
            soldierCount = 0;
            foreach (Formation f in subFormations)
            {
                soldierCount += f.soldierCount;
            }
        }

    }

    Formation GetSubFormation(Formation formation)
    {
        
        if (formation.subFormations.Count > 0)
        {
            return formation;
        }
        return null;
    }

    Formation GetLowestSubformation (Formation f)
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
