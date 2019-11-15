using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WarObject : MonoBehaviour
{
    [SerializeField] List<GameObject> subordinates;
    public List<GameObject> commanders;
    public FactionData Faction;
    public Formation formation;

    //NavMeshAgent agent;
    GameObject objective;

    public string Name
    {
        get { return transform.root.name; }
    }

    void OnEnable()
    {
        /*
        agent = GetComponent<NavMeshAgent>();

        if (objective != null)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(objective.transform.position, path);
            agent.SetPath(path);
        }
        */
    }
}


