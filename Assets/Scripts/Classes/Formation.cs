using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Formation
{

    public string formationName;
    //public FormationData initData;
    //public Sprite formationFlag;
    [HideInInspector] public int hierarchyLevel;
    GameObject prefab;
    FactionData Faction;
    string folderPath = "Assets/PreFabs/Formations/";
    string commanderName;
    int soldierCount;
    List<HierarchyItem> Subordinates;
    public Commander Commander;
    List<Unit> units;
    List<ObjectPoolItem> poolItemList = new List<ObjectPoolItem>();
    public Formation superiorFormation;


    public void Initialize()
    {
        Formation formation = new Formation();
        BattleController.SharedInstance.formationCount++;
    }

    public void InitializeData(FormationData formationData)
    {
        
        /*
        initData = formationData;
        if (initData != null)
        {
            formationName = initData.name;
            prefab = initData.prefab;
            hierarchyLevel = initData.hierarchyLevel;
        }
        */
        CreatePrefab();
    }

    void CreatePrefab()
    {
        //Create gameobject for formation
        prefab = new GameObject();
        prefab.name = formationName;

        if (prefab != null)
        {
            //create prefab if not already existing
            if (AssetDatabase.Contains(prefab.GetInstanceID()) == false)
            {


                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    AssetDatabase.GUIDToAssetPath(folderPath);
                    PrefabUtility.SaveAsPrefabAsset(prefab, folderPath);
                }
                else
                {
                    Directory.CreateDirectory(folderPath);
                    AssetDatabase.CreateFolder("Assets/PreFabs/", "Formations");
                    AssetDatabase.GUIDToAssetPath(folderPath);
                    PrefabUtility.SaveAsPrefabAsset(prefab, folderPath + prefab.name + ".prefab");
                }
                Object.Destroy(prefab);
            }
        }

        //Debug.Log("Initialized formation: " + this.formationName);
    }
    void InitUnits()
    {
        //if (initData.prefab != null)
        
            //prefab = initData.prefab;
            /*
            WarObject warObject = prefab.AddComponent<WarObject>();
            warObject.formation = this;
            */
            //Debug.Log("Scriptable object has a prefab: " + formationPrefab.name);
        
        //else if (AssetDatabase.FindAssets(initData.name) != null)

        if (folderPath != null && prefab != null)
        {
            prefab = (AssetDatabase.LoadAssetAtPath<GameObject>(folderPath + prefab.name + ".prefab"));
            //initData.prefab = prefab;

            //Debug.Log("No prefab in scriptable object. Prefab found from asset database: " + formationPrefab.name);
            /*
            if (prefab.TryGetComponent(out WarObject wo) == false)
            {
                WarObject warObject = prefab.AddComponent<WarObject>();
                warObject.formation = this;
                //Debug.Log("Added warcomponent");
            }
            else
            {
                wo.formation = this;
            }
            */
        }


        //pool commander
        //if (initData.Commander != null)

        Unit unitInstance = new Unit();
        //Commander = initData.Commander;
        //unitInstance.Name = Commander.name;
        unitInstance.Init();


        //InitSubUnits();
    } 

    public void QueueSubFormations()
    {
        /*
        if (subFormations.Count > 0)
        {
            for (int i = 0; i < subFormations.Count; i++)
            //foreach (Formation subFormation in subFormations)
            {
                string subFormation = subFormations[i];
                if (subFormation != null)
                {
                    subFormation.hierarchyLevel = hierarchyLevel + 1;
                    BattleController.SharedInstance.formationQueue.Enqueue(subFormation);
                }
            }
        }
        */
    }

    void InitSubUnits()
    {
        if (units.Count > 0)
        {
            foreach (Unit unit in units)
            {
                
             /*   ObjectPoolItem newItem = new ObjectPoolItem();
                newItem.AmountToPool = item.Count;
                newItem.objectToPool = item.GameObject;
                poolItemList.Add(newItem);
                
                for (int i = 0; i < item.Count; i++)
                {
                    Unit unitInstance = new Unit();
                    unitInstance.unitName = item.GameObject.name;
                    unitInstance.Init();
                }*/
            }
        }
    }

    public void PoolUnits()
    {        
        if (prefab != null)
        {
            // pool formation
            ObjectPooler.SharedInstance.AddGameObject(prefab);
        }        
        else
        {
            // create formation
            InitUnits();
        }
        
        if (Commander != null)
        {
            // pool commander
            ObjectPooler.SharedInstance.AddGameObject(Commander.prefab);
        }

        PoolSubordinates();
    }
    void PoolSubordinates()
    {
        if (poolItemList.Count > 0)
        {
            foreach (ObjectPoolItem newItem in poolItemList)
            {                
                ObjectPooler.SharedInstance.AddPoolItem(newItem);

            }
        }
    }
}


