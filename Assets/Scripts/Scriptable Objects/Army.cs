using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Army", menuName = "Scriptable Objects/Army", order = 5)]
public class Army : ScriptableObject
{
    [SerializeField] Faction faction;
    [SerializeField] string groupName;
    [SerializeField] Soldier Commander;
    int squadCount;

    public List<Formation> formationList;
    [SerializeField] List<Soldier> soldierList;

    GameObject armyCommander;
    private string newArmyName;
}
