using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierData : ScriptableObject
{
    [SerializeField] string soldierName;
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] Formation squad;
    [SerializeField] ArmyData army;
    [SerializeField] FactionData faction;
}
