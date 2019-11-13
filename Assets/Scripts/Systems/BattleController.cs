using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class BattleController : MonoBehaviour
{
    public static BattleController SharedInstance;
    [SerializeField] Battle battle;
    [SerializeField] ObjectPooler pooler;
    [SerializeField] List<Faction> faction1List;
    [SerializeField] List<Faction> faction2List;
    [SerializeField] GameObject battleLocation;
    List<Formation> faction1FormationList;
    List<Formation> faction2FormationList;

    [SerializeField] List<Formation> battleFormations;
    int hierarchyCount;

    public Battle Battle
    {
        get { return battle; }
        set { }
    }
    private void Awake()
    {
        SharedInstance = this;        
    }

    void Start()
    {   
        pooler = FindObjectOfType<ObjectPooler>();

        GetBattleData(battle);
    }

    void LoadBattleScene()
    {
        if (battle.Location != null)
        {
            SceneManager.LoadScene(battle.Location.GetComponent<ScenePicker>().scenePath, LoadSceneMode.Additive);
            Debug.Log("Scene loaded: " + battle.Location.GetComponent<ScenePicker>().scenePath);
        }        
    }

    public void GetBattleData(Battle battle)
    {
        if (battle != null)
        {
            faction1List = battle.factions1;
            faction2List = battle.factions2;
            battleLocation = battle.Location;
            faction1FormationList = battle.faction1FormationList;
            faction2FormationList = battle.faction2FormationList;
            if (faction1FormationList != null)
            {
                foreach (Formation f in faction1FormationList)
                {
                    battleFormations.Add(f);
                }
            }
            if (faction2FormationList != null)
            {
                foreach (Formation f in faction2FormationList)
                {
                    battleFormations.Add(f);
                }
            }
        }
        
    }

    public void SetBattleData(GameObject location, List<Faction> factions1, List<Faction> factions2)
    {
        battle.Location = location;
        battle.factions1 = factions1;
        battle.factions2 = factions2;
        battle.faction1FormationList = faction1FormationList;
        battle.faction2FormationList = faction2FormationList;
    }

    public void PoolBattleFormations()
    {        
        foreach (Formation f in battleFormations)
        {
        }
    }
}
