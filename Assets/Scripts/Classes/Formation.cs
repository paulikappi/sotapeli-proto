using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Formation
{
    string formationName;
    public GameObject commanderPrefab;
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
        }
        Object.Destroy(formationPrefab);
        Pool();
    }
    void Pool()
    {
        // Formation has prefab reference
        if (Data.Formation != null)
        {
            formationPrefab = Data.Formation;
            ObjectPooler.SharedInstance.AddGameObject(formationPrefab);

            WarObject warObject = formationPrefab.AddComponent<WarObject>();
            warObject.formation = this;
        }
        // There is a prefab in the database
        else if (AssetDatabase.FindAssets(Data.name) != null) 
        { 
            formationPrefab = (AssetDatabase.LoadAssetAtPath<GameObject>(folderPath + formationPrefab.name + ".prefab"));
            ObjectPooler.SharedInstance.AddGameObject(formationPrefab);
            Data.Formation = formationPrefab;

            if (formationPrefab.TryGetComponent(out WarObject wo) == false)
            {
                WarObject warObject = formationPrefab.AddComponent<WarObject>();
                warObject.formation = this;
            }
            else 
                wo.formation = this;
        }
        else
        {
            Debug.LogError("FormationERROR: Formation doesn't have prefab: " + formationPrefab.name);
        }

        //pool formation's commander prefab
        commanderPrefab = Data.Commander;
        ObjectPooler.SharedInstance.AddGameObject(commanderPrefab);
        PoolSubordinates();        
    }
    void PoolSubordinates()
    {
        if (Data.Subordinates.Count > 0)
        {
            foreach (ObjectPoolItem item in Data.Subordinates)
            {
                if (item != null)
                ObjectPooler.SharedInstance.AddPoolItem(item);
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
                if (subFormationData != null)
                {
                    Formation subFormation = new Formation();

                    //get scriptable object's data
                    subFormation.Data = subFormationData;
                    if (subFormation.superFormationList != null && subFormation.superFormationList.Contains(this) == false)
                    {
                        subFormation.superFormationList.Add(this);
                    }
                    //add superior formation data to scriptable object if needed
                    if (subFormation.Data.SuperiorFormations.Contains(this.Data) == false)
                    {
                        subFormation.Data.SuperiorFormations.Add(this.Data);
                    }
                    //add subformation to formation
                    if (subFormationList != null && subFormation != null)
                    {
                        subFormationList.Add(subFormation);
                    }
                    
                    subFormation.Initialize();
                }
            }
        }
        BattleController.SharedInstance.readyFormationCount++;
        if (BattleController.SharedInstance.readyFormationCount == BattleController.SharedInstance.totalFormationCount)
        {
            BattleController.SharedInstance.OnFormationsReady();
        }
    }
}
