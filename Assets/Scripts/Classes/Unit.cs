using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
    public string rank;
    public string unitName;
    [HideInInspector] public FactionData faction;
    [HideInInspector] public GameObject prefab;

    public void Init()
    {
        BattleController.SharedInstance.unitCount++;
        BattleController.SharedInstance.unitNames.Add(unitName);
        Debug.Log(BattleController.SharedInstance.unitCount);
    }
}
