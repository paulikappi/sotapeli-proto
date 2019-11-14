using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "BattleData of ", menuName = "Scriptable Objects/BattleData", order = 2)]
public class BattleData : ScriptableObject
{
    [SerializeField] GameObject battleLocation;    

    public List<FactionData> factions1 = new List<FactionData>();
    public List<FactionData> factions2 = new List<FactionData>();

    //[SerializeField] List<GameObject> faction1SpawnAreas = new List<GameObject>();
    //[SerializeField] List<GameObject> faction2SpawnAreas = new List<GameObject>();

    public List<FormationData> faction1FormationList = new List<FormationData>();
    public List<FormationData> faction2FormationList = new List<FormationData>();

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
