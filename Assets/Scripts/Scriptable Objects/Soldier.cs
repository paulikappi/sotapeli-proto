using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : ScriptableObject
{
    [SerializeField] string soldierName;
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] Squad squad;
    [SerializeField] Company company;
    [SerializeField] Army army;
    [SerializeField] Faction faction;
}
