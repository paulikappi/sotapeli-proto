using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BattleData of ", menuName = "Scriptable Objects/BattleData", order = 2)]
public class BattleData : ScriptableObject
{
    [SerializeField] GameObject battleLocation;

    public List<Battleside> battlesides;

    public GameObject Location
    {
        get
        {
            return battleLocation;            
        }
        set 
        {
            this.Location = Location;
        }
    }
}
