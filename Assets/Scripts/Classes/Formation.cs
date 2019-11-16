using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Formation
{
    string formationName;
    public GameObject commander;
    public GameObject formationPrefab;
    public FactionData Faction;
    public FormationData Data;
    public List<Formation> subFormationList;
    public List<Formation> superFormationList;
    string folderPath = "Assets/PreFabs/Formations/";

    [SerializeField] List<GameObject> prefabs;

    public string Name
    {
        get { return this.formationName; }
        private set { }
    }

    public void Initialize()
    {
        BattleController.SharedInstance.totalFormationCount++;

        if (formationName == null && Data != null)
        {
            formationName = Data.name;
        }
        CreatePrefab();
    }

    void CreatePrefab()
    {
        //Create gameobject for formation
        formationPrefab = new GameObject();
        formationPrefab.name = formationName;

        if (formationPrefab != null)
        {
            //create prefab if not already existing
            if (AssetDatabase.Contains(formationPrefab.GetInstanceID()) == false)
            {


                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    AssetDatabase.GUIDToAssetPath(folderPath);
                    PrefabUtility.SaveAsPrefabAsset(formationPrefab, folderPath);
                }
                else
                {
                    Directory.CreateDirectory(folderPath);
                    AssetDatabase.CreateFolder("Assets/PreFabs/", "Formations");
                    AssetDatabase.GUIDToAssetPath(folderPath);
                    PrefabUtility.SaveAsPrefabAsset(formationPrefab, folderPath + formationPrefab.name + ".prefab");
                }
                Object.Destroy(formationPrefab);
            }
        }
        Pool();
    }
    void Pool()
    {
        // Check if Formation has a prefab reference
        if (Data.Formation != null)
        {
            formationPrefab = Data.Formation;
            ObjectPooler.SharedInstance.AddGameObject(formationPrefab);

            WarObject warObject = formationPrefab.AddComponent<WarObject>();
            warObject.formation = this;
            //Debug.Log("Scriptable object has a prefab: " + formationPrefab.name);
        }
        // There is a prefab in the database
        else if (AssetDatabase.FindAssets(Data.name) != null)
        {
            if (folderPath != null && formationPrefab != null)
            {
                formationPrefab = (AssetDatabase.LoadAssetAtPath<GameObject>(folderPath + formationPrefab.name + ".prefab"));
                ObjectPooler.SharedInstance.AddGameObject(formationPrefab);
                Data.Formation = formationPrefab;

                //Debug.Log("No prefab in scriptable object. Prefab found from asset database: " + formationPrefab.name);

                if (formationPrefab.TryGetComponent(out WarObject wo) == false)
                {
                    WarObject warObject = formationPrefab.AddComponent<WarObject>();
                    warObject.formation = this;
                    //Debug.Log("Added warcomponent");
                }
                else
                {
                    wo.formation = this;
                }
            }
        }
        else
        {
            Debug.LogError("FormationERROR: Formation doesn't have prefab: " + formationPrefab.name);
        }

        //pool formation's commander prefab
        if (Data.Commander != null)
        {
            commander = Data.Commander;
            ObjectPooler.SharedInstance.AddGameObject(commander);
        }
        else 
        {
            Debug.LogError("Formation Commander ERROR: " + this.Name);
        }
        
        PoolSubordinates();
    }
    void PoolSubordinates()
    {
        
        if (Data.Subordinates.Count > 0)
        {            
            foreach (HierarchyItem item in Data.Subordinates)
            {
                ObjectPoolItem newItem = new ObjectPoolItem();
                newItem.AmountToPool = item.Count;
                newItem.objectToPool = item.GameObject;
                ObjectPooler.SharedInstance.AddPoolItem(newItem);
            }            
        }
        PoolSubformations();
    }

    void PoolSubformations()
    {
        // Pool formation's prefabs

        if (Data.SubFormations.Count > 0)
        {
            // go through subformation tree
            foreach (FormationData subFormationData in Data.SubFormations)
            {
                //Debug.Log("Formation has subformation: " + Data.name + " > " + subFormationData.name);
                if (subFormationData != null)
                {
                    Formation createdFormation = new Formation();

                    //reference to scriptable object's data
                    createdFormation.Data = subFormationData;
                    if (createdFormation.superFormationList != null && createdFormation.superFormationList.Contains(this) == false)
                    {
                        createdFormation.superFormationList.Add(this);
                    }
                    //add superior formation data to subformation's scriptable object
                    if (createdFormation.Data.SuperiorFormations.Contains(this.Data) == false)
                    {
                        createdFormation.Data.SuperiorFormations.Add(this.Data);
                    }
                    //add created subformation to this formation's list
                    if (subFormationList != null && createdFormation != null)
                    {
                        subFormationList.Add(createdFormation);
                    }

                    createdFormation.Initialize();
                }
            }
        }
        else
        {
            //Debug.Log("Formation doesn't have subformations: " + Data.name);
        }

        BattleController.SharedInstance.readyFormationCount++;
        if (BattleController.SharedInstance.readyFormationCount == BattleController.SharedInstance.totalFormationCount)
        {
            BattleController.SharedInstance.OnFormationsReady();
        }
    }
}

