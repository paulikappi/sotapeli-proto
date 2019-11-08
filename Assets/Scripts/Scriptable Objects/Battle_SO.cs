using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Battle of ", menuName = "Scriptable Objects/Battle", order = 2)]
public class Battle_SO : ScriptableObject
{
    [SerializeField] Location battleLocation;
    [SerializeField] ScenePicker battleScene = new ScenePicker();

    [SerializeField] List<Faction> side1 = new List<Faction>();
    [SerializeField] List<Faction> side2 = new List<Faction>();

    [SerializeField] List<GameObject> faction1SpawnAreas = new List<GameObject>();
    [SerializeField] List<GameObject> faction2SpawnAreas = new List<GameObject>();

    [SerializeField] List<Army> faction1ArmyList = new List<Army>();
    [SerializeField] List<Army> faction2ArmyList = new List<Army>();

    public Location Location
    {
        get
        {
            return battleLocation;            
        }
    }
}
