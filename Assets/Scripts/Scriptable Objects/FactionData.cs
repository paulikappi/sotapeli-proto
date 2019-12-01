using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


[CreateAssetMenu(fileName = "new FactionData", menuName = "Scriptable Objects/FactionData", order = 1)]
public class FactionData : ScriptableObject

{
    public Sprite flag;
    [SerializeField] string factionName;
    [SerializeField] string nationality;
    [SerializeField] string commanderName;
    [SerializeField] Material factionMaterial;
    
    [SerializeField] string armyGroup = "Army Group";
    [SerializeField] string army = "Army";
    [SerializeField] string corps = "Corps";
    [SerializeField] string division = "Division";
    [SerializeField] string brigade = "Brigade";
    [SerializeField] string regiment = "Regiment";
    [SerializeField] string batallion = "Batallion";
    [SerializeField] string company = "Company";
    [SerializeField] string platoon = "Platoon";
    [SerializeField] string squad = "Squad";
    [SerializeField] string fireteam = "Fireteam";
    [SerializeField] string soldier = "Soldier";

    [SerializeField] int recruits;
    public float trainingSpeed = 1f;
    public int soldiersPerMinute = 200;
    [SerializeField] int soldierCount;    
    public List<Unit> soldierList;
    public List<Formation> formationList = new List<Formation>();
    public List<string> surnames = new List<string>();
    RecruitTrainer trainer = new RecruitTrainer();

    public string Name
    {
        get { return factionName; }
    }
    
    private void OnEnable()
    {
        //WriteSurnameListToDisk();
        if (surnames.Count < 1)
        {
            GenerateSurnameListFromCSV();
        }
        if (trainer != null)
        {
            trainer.faction = this;
            RecruitTroops();            
        }        

        /*
        string nameJson = "Assets/GermanSurnames.json";
        string filePath = Path.Combine(Application.streamingAssetsPath, nameJson);
        string json = JsonUtility.ToJson(filePath);
        char sep = char.Parse(",");

        string[] germanSurnames = json.Split(sep);
        Debug.Log(germanSurnames.Length);
        */
    }

    public void Initialize() 
    {
        Faction faction = new Faction();
        faction.initData = this;
    }

    void RecruitTroops()
    {
        trainer.InitRecruits(recruits);

    }

    void WriteSurnameListToJSON()
    {
        string filePath = Application.dataPath + "/Text/" + factionName + ".json";
        if (AssetDatabase.AssetPathToGUID(filePath) != null)
        {
            SurnameListObject surnameListObject = new SurnameListObject();
            foreach (string name in surnames)
            {
                surnameListObject.surnameList.Add(name);
            }
            string json = JsonUtility.ToJson(surnameListObject);
            
            File.WriteAllText(filePath, json);
        }
    }

    void GenerateSurnameListFromCSV()
    {
        string csvPath = "Text/" + factionName + ".csv";
        string filePath = Path.Combine(Application.dataPath, csvPath);
        if (File.Exists(filePath))
        {
            surnames.Clear();
            StreamReader reader = new StreamReader(filePath);
            string nameCSV = reader.ReadToEnd();
            reader.Close();

            char lineSeparator = '\n';
            char fieldSeparator = ',';
            string[] lines = nameCSV.Split(lineSeparator);
            foreach (string line in lines)
            {
                string[] fields = nameCSV.Split(fieldSeparator);
                foreach (string field in fields)
                {
                    if (surnames.Contains(field) == false)
                    {
                        surnames.Add(field);
                    }
                }
            }
        }
    }


    bool CheckAndCreateFolder(string path)
    {
        bool check = Directory.Exists(path);
        if (!check)
        {
            Directory.CreateDirectory(path);
        }
        return check;
    }

    void CreateScriptableObject<T>() where T : ScriptableObject
    {
        string tType = typeof(T).ToString();
        CheckAndCreateFolder("Assets/Scriptable Objects/" + tType + "s/");
        //double temp = (double)typeof(FactionData).GetProperty(tType).GetValue(this);
    }
}


