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
    public List<Battleside> battlesides;
    [SerializeField] GameObject battleLocation;

    public int formationCount;
    public int unitCount;
    public List<string> unitNames = new List<string>();

    [SerializeField] List<Formation> battleFormations;
    
    //public Queue<FormationData> formationDataQueue = new Queue<FormationData>();
    public Queue<Formation> formationQueue;
    

    public BattleData Battle
    {
        get { return battle; }
        set { }
    }
    private void Awake()
    {
        SharedInstance = this;
        
        pooler = ObjectPooler.SharedInstance;        
    }

    void Start()
    {
        GetBattleData(battle);
        InitializeBattleSides();        
        LoadBattleScene();        
        InitFormationQueue();
        //PoolFormationQueue();
        //ObjectPooler.SharedInstance.OptimizePoolList();
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
                Debug.LogError("Location ERROR: No scene defined");
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
            battleLocation = battle.Location;

            foreach(Battleside battleSide in battle.battlesides)
            {
                battlesides.Add(battleSide);

                foreach (FactionData factionData in battleSide.factionList)
                {
                    foreach (Formation formation in factionData.formationList)
                    {
                        battleFormations.Add(formation);
                    }
                }
                
            }
        }        
    }

    public void SetBattleData(GameObject location, List<FactionData> factions1, List<FactionData> factions2)
    {

    }
    #endregion

    #region Formations

    void InitializeBattleSides()
    {   
        foreach (Battleside battleSide in battlesides)
        {
            battleSide.Initialize();            
            
            foreach (FactionData factionData in battleSide.factionList)
            {
                factionData.Initialize();
                
            }
            foreach (Formation formation in battleSide.formations)
            {
                //Debug.Log("Initializing Formations: " + battleside.formations.Count);
                QueueFormation(formation);
                formation.hierarchyLevel = 0;
                //Debug.Log("Battle formation queued");
            }
            //Debug.Log("Battle Formations ready");
        }        
    }


    void QueueFormation(Formation formation)        
    {
        //queues every formation scriptable objects to get initialized

        //Debug.Log("Formation queued: " + formationData.name + " level: " + formationData.hierarchyLevel);
        if (formationQueue != null)
        {
            formationQueue.Enqueue(formation);

            //Debug.Log("QueueFormationData finished: " + formationData.name) ;
        }

    }

    void InitFormationQueue()
    {
        if (formationQueue != null)
        {
            int count = formationQueue.Count;

            for (int i = 0; i < count; i++)
            {
                Formation formation = formationQueue.Dequeue();
                formation.Initialize();
            }
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
                poolFormation.PoolUnits();
            }            
        }
        else
        {
            Debug.LogError("FORMATION QUEUE ERROR, queue is empty");
        }
    }       
    
    #endregion
}
