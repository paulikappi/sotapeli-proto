using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Battle of ", menuName = "Scriptable Objects/Battle", order = 2)]
public class Battle : ScriptableObject
{
    [SerializeField] GameObject battleLocation;    

    public List<Faction> factions1 = new List<Faction>();
    public List<Faction> factions2 = new List<Faction>();

    //[SerializeField] List<GameObject> faction1SpawnAreas = new List<GameObject>();
    //[SerializeField] List<GameObject> faction2SpawnAreas = new List<GameObject>();

    public List<Formation> faction1FormationList = new List<Formation>();
    public List<Formation> faction2FormationList = new List<Formation>();

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
