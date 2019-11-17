using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

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

    //[HideInInspector] 
    public int preCountedFormations;
    //[HideInInspector] 
    public int initializedFormations;
    //[HideInInspector] 
    public int queuedFormationDatas;
    //[HideInInspector] 
    public int readyFormations;
    //[HideInInspector] 
    public int readyBattleFormations;
    //[HideInInspector] 
    public int battleFormationsCount;

    public Queue<FormationData> formationDataQueue = new Queue<FormationData>();
    public Queue<Formation> formationQueue = new Queue<Formation>();

    public bool formationsReady;

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
        LoadBattleScene();        
        GetFormationQueue();
        PoolFormationQueue();
    }

    

    # region Battledata
    void LoadBattleScene()
    {
        if (battle.Location != null)
        {
            if (battle.Location.GetComponent<ScenePicker>().scenePath != null)
            {
                SceneManager.LoadScene(battle.Location.GetComponent<ScenePicker>().scenePath, LoadSceneMode.Additive);
                //Debug.Log("Scene loaded: " + battle.Location.GetComponent<ScenePicker>().scenePath);
            }
            else
            {
                Debug.LogError("Location prefab ERROR: No scene defined");
            }
        }
        else
        {
            Debug.LogError("BATTLE ERROR: No location defined");
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
                    if (f != null)
                    {
                        battleFormations.Add(f);
                    }                    
                }
            }
            if (faction2FormationList != null)
            {
                foreach (FormationData f in faction2FormationList)
                {
                    if (f != null)
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
    #endregion

    #region Formations

    void InitFormations()
    {        
        if (battleFormations != null)
        {
            //Debug.Log("Initializing Formations: " + battleFormations.Count);
            foreach (FormationData formationData in battleFormations)
            {
                preCountedFormations++;
                QueueFormationData(formationData);
                formationData.hierarchyLevel = 0;
                //Debug.Log("Battle formation queued");
            }
            
            //Debug.Log("Battle Formations ready");
        }
        else
        {
            Debug.LogError("BATTLE ERROR, no formations in battle");
        }
    }


    void QueueFormationData(FormationData formationData)        
    {
        //Debug.Log("Formation queued: " + formationData.name + " level: " + formationData.hierarchyLevel);
        formationDataQueue.Enqueue(formationData);
        if (formationData.SubFormations.Count > 0)
        {
            foreach (FormationData subFormationData in formationData.SubFormations)
            {
                if (subFormationData != null)
                {
                    subFormationData.hierarchyLevel = formationData.hierarchyLevel + 1;
                    preCountedFormations++;
                    QueueFormationData(subFormationData);                    
                }                
            }
        }
        //Debug.Log("QueueFormationData finished: " + formationData.name) ;
    }

    void GetFormationQueue()
    {
        int count = formationDataQueue.Count;

        for (int i = 0; i < count; i++)
        {
            FormationData data = formationDataQueue.Dequeue();
            Formation formation = new Formation();
            formation.Initialize(data);
            formationQueue.Enqueue(formation);
        } 
    }

    void PoolFormationQueue()
    {
        int count = formationQueue.Count;
        if (formationQueue != null && formationQueue.Count > 0)
        {
            //Debug.Log("Pooling formations: " + formationQueue.Count);
                
            for (int a = 0; a < count; a++)
            {
                Formation poolFormation = formationQueue.Dequeue();
                //Debug.Log("Pooling formation: " + poolFormation.formationName);
                poolFormation.Pool();
            }
            ObjectPooler.SharedInstance.OptimizePoolList();
        }
        else
        {
            Debug.LogError("FORMATION QUEUE ERROR, queue is empty");
        }
    }       
    
    #endregion
}
