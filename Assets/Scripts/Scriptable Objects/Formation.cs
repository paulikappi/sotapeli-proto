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
    [SerializeField] List<Formation> subFormations;
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
