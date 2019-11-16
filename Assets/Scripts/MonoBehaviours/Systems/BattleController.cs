using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

[System.Serializable]
public class BattleController : MonoBehaviour
{
    public static BattleController SharedInstance;
    [SerializeField] BattleData battle;
    ObjectPooler pooler;
    List<FactionData> faction1List;
    List<FactionData> faction2List;
    GameObject battleLocation;
    List<FormationData> faction1FormationList;
    List<FormationData> faction2FormationList;

    [SerializeField] List<FormationData> battleFormations;
    [SerializeField] List<GameObject> formationsObjects;
    GameObject formationObject;

    public int totalFormationCount;
    public int readyFormationCount;
    [SerializeField] List<ObjectPoolItem> poolItems = new List<ObjectPoolItem>();

    public BattleData Battle
    {
        get { return battle; }
        set { }
    }
    private void Awake()
    {
        SharedInstance = this;
        GetBattleData(battle);        
        pooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();        
    }

    void Start()
    {
        InitFormations();
    }

    void InitFormations()
    {        
        if (battleFormations != null)
        {
            foreach (FormationData formationData in battleFormations)
            {
                if (formationData != null)
                {
                    Formation formationInstance = new Formation();
                    formationInstance.Data = formationData;
                    formationInstance.Initialize();
                }                
            }            
        }        
    }

    void LoadBattleScene()
    {
        if (battle.Location != null)
        {
            SceneManager.LoadScene(battle.Location.GetComponent<ScenePicker>().scenePath, LoadSceneMode.Additive);
            Debug.Log("Scene loaded: " + battle.Location.GetComponent<ScenePicker>().scenePath);
        }        
    }

    public void GetBattleData(BattleData battle)
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
                foreach (FormationData f in faction1FormationList)
                {
                    if (battleFormations != null)
                    {
                        battleFormations.Add(f);
                    }                    
                }
            }
            if (faction2FormationList != null)
            {
                foreach (FormationData f in faction2FormationList)
                {
                    if (battleFormations != null)
                    {
                        battleFormations.Add(f);
                    }
                }
            }
        }        
    }

    public void SetBattleData(GameObject location, List<FactionData> factions1, List<FactionData> factions2)
    {
        battle.Location = location;
        battle.factions1 = factions1;
        battle.factions2 = factions2;
        battle.faction1FormationList = faction1FormationList;
        battle.faction2FormationList = faction2FormationList;
    }

    public void OnFormationsReady() 
    {
        ObjectPooler.SharedInstance.OptimizePoolList();
    }
}
