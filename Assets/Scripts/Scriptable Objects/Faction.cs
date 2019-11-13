using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


[CreateAssetMenu(fileName = "new Faction", menuName = "Scriptable Objects/Faction", order = 1)]
public class Faction : ScriptableObject 

{ 
    [SerializeField] string factionName;
    [SerializeField] string nationality;
    [SerializeField] string commanderName;
    [SerializeField] int soldierCount;
    [SerializeField] int squadCount;
    [SerializeField] int companyCount;
    [SerializeField] int batallionCount;
    [SerializeField] int regimentCount;
    [SerializeField] int divisionCount;
    
    [SerializeField] int armyCount;

    int createdArmies;
    int createdSoldiers;

    [SerializeField] Material factionMaterial;
    [SerializeField] List<string> nameList;
    string nameJson = "Assets/GermanSurnames.json";

    [SerializeField] List<Army> armyList = new List<Army>();
    [SerializeField] List<Soldier> soldierList = new List<Soldier>();

    public string Name
    {
        get { return factionName; }
    }
    /*
    private void OnEnable()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, nameJson);
        string json = JsonUtility.ToJson(filePath);
        char sep = char.Parse(",");

        string[] germanSurnames = json.Split(sep);
        Debug.Log(germanSurnames.Length);
    }
    */
    private void OnValidate()
    {
        squadCount = Mathf.RoundToInt(soldierCount / 10);       //10
        companyCount = Mathf.RoundToInt(squadCount / 10);       //100
        batallionCount = Mathf.RoundToInt(companyCount / 5);    //500
        regimentCount = Mathf.RoundToInt(batallionCount / 2);   //1,000
        divisionCount = Mathf.RoundToInt(regimentCount / 10);   //10,000        
        armyCount = Mathf.RoundToInt(divisionCount / 10);       //100,000
        
        //CreateSoldier();
    }

    void CheckFolder(string path)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
    }

    void CreateScriptableObject<T>() where T : ScriptableObject
    {
        string tType = typeof(T).ToString();
        CheckFolder("Assets/Scriptable Objects/" + tType + "s/");
        //double temp = (double)typeof(Faction).GetProperty(tType).GetValue(this);
    }

    void CreateSoldier() {

        CheckFolder("Assets/Scriptable Objects/Soldiers/");

        if (createdSoldiers != soldierList.Count)
        {
            createdSoldiers = soldierList.Count;
        }

        while (soldierList.Count < soldierCount)
        {
            createdSoldiers++;
            string newSoldierName = createdSoldiers.ToString();
            Soldier newSoldier = ScriptableObject.CreateInstance<Soldier>();
            soldierList.Add(newSoldier);
            AssetDatabase.CreateAsset(newSoldier, "Assets/Scriptable Objects/Soldiers/" + factionName + " " + newSoldierName + ".asset");
            AssetDatabase.SaveAssets();
        }        
    }

    void CreateArmy()
    {
        createdArmies++;
        string newArmyName = createdArmies + ". " + nationality + " Army";
        Army newArmy = ScriptableObject.CreateInstance<Army>();
        AssetDatabase.CreateAsset(newArmy, "Assets/Scriptable Objects/Armies/" + newArmyName + ".asset");
        AssetDatabase.SaveAssets();
        armyList.Add(newArmy);
    }

    void ArrangeTroops()
    {
        //armyCount = Mathf.RoundToInt(soldierCount / 1000);
        
    }
}
