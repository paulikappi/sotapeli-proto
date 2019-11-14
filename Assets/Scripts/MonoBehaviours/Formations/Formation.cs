using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Formation : MonoBehaviour
{
    public FormationData Data;
    [SerializeField] List<FormationData> subFormations;

    [SerializeField] List<GameObject> Prefabs;

    ObjectPooler pooler;

    private void Awake()
    {
    
    }

    void SetHierarchyValues()
    {
        if (subFormations.Count > 0)
        {
            foreach (FormationData f in subFormations)
            {
                //f.GetSuperiorFormation();
            }
        }
    }
}
