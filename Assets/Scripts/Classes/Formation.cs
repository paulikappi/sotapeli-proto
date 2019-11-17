using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Formation
{
    public FormationData Data;
    public string formationName;
    public GameObject commander;
    public GameObject prefab;
    public FactionData Faction;    
    public List<Formation> subFormationList;
    public List<Formation> superFormationList;
    string folderPath = "Assets/PreFabs/Formations/";
    string commanderName;
    int soldierCount;
    public GameObject Commander;    
    public List<HierarchyItem> Subordinates;
    [SerializeField] List<FormationData> subFormationTypes;
    [SerializeField] List<FormationData> superiorFormationTypes;
    [SerializeField] List<GameObject> prefabs;

    public void Initialize(FormationData formationData)
    {
        Data = formationData;
        if (Data != null)
        {
            formationName = Data.name;
            prefab = Data.Formation;            
        }
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
        BattleController.SharedInstance.initializedFormations++;
        //Debug.Log("Initialized formation: " + this.formationName);
    }

    public void Pool()
    {
        // Check if Formation has a prefab reference
        if (Data.Formation != null)
        {
            prefab = Data.Formation;
            ObjectPooler.SharedInstance.AddGameObject(prefab);

            WarObject warObject = prefab.AddComponent<WarObject>();
            warObject.formation = this;
            //Debug.Log("Scriptable object has a prefab: " + formationPrefab.name);
        }
        // There is a prefab in the database
        else if (AssetDatabase.FindAssets(Data.name) != null)
        {
            if (folderPath != null && prefab != null)
            {
                prefab = (AssetDatabase.LoadAssetAtPath<GameObject>(folderPath + prefab.name + ".prefab"));
                ObjectPooler.SharedInstance.AddGameObject(prefab);
                Data.Formation = prefab;

                //Debug.Log("No prefab in scriptable object. Prefab found from asset database: " + formationPrefab.name);

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
            }
        }
        else
        {
            Debug.LogError("FormationERROR: Formation doesn't have prefab: " + prefab.name);
        }

        //pool formation's commander prefab
        if (Data.Commander != null)
        {
            commander = Data.Commander;
            ObjectPooler.SharedInstance.AddGameObject(commander);
        }
        else
        {
            Debug.LogError("Formation Commander ERROR: " + this.formationName);
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
    }
}


