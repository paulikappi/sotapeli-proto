using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ArmyData", menuName = "Scriptable Objects/ArmyData", order = 5)]
public class ArmyData : ScriptableObject
{
    [SerializeField] FactionData faction;
    [SerializeField] string groupName;
    [SerializeField] SoldierData Commander;
    int squadCount;

    public List<FormationData> formationList;
    [SerializeField] List<SoldierData> soldierList;

    GameObject armyCommander;
    private string newArmyName;
}
