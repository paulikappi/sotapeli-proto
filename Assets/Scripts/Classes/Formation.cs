using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation
{
    string formationName;
    public GameObject Commander;
    public FactionData Faction;
    public FormationData Data = new FormationData();
    public List<Formation> subFormationList;
    public List<Formation> superFormationList;
    
    [SerializeField] List<GameObject> prefabs;

    public string Name
    {
        get { return this.formationName; }
        private set { }
    }    

    public void PoolSubFormations()
    {
        if (formationName == null && Data != null)
        {
            formationName = Data.name;
        }

        GameObject formationObject = new GameObject();
        ObjectPooler.SharedInstance.pooledObjects.Add(formationObject);
        WarObject warObject = formationObject.AddComponent<WarObject>();
        warObject.formation = this;
        formationObject.name = formationName;
        Commander = Data.Commander;
        ObjectPooler.SharedInstance.AddGameObject(Commander);

        // Pool formation's prefabs
        foreach (ObjectPoolItem item in Data.Subordinates)
        {            
            ObjectPooler.SharedInstance.AddPoolItem(item);                        
        }

        // go through subformation tree
        foreach (FormationData subFormationData in Data.SubFormationTypes)
        {
            Formation subFormation = new Formation();
            
            //get scriptable object's data
            subFormation.Data = subFormationData;
            if (subFormation.superFormationList != null && subFormation.superFormationList.Contains(this) == false)
            {                
                subFormation.superFormationList.Add(this);
            }
            //add superior formation data to scriptable object if needed
            if (subFormation.Data.SuperiorFormationTypes.Contains(this.Data) == false)
            {
                subFormation.Data.SuperiorFormationTypes.Add(this.Data);
            }
            
            if (subFormationList != null && subFormationList.Contains(subFormation) == false)
            {
                subFormationList.Add(subFormation);
            }            

            subFormation.PoolSubFormations();
        }
    }
}
