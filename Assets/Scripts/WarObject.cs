using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WarObject : MonoBehaviour
{
    [SerializeField] string objectName;
    public List<Formation> commandingFormations;
    [SerializeField] Faction faction;

    NavMeshAgent agent;
    GameObject objective;

    public string Name
    {
        get { return objectName; }
    }
    public Faction Faction
    {
        get { return faction; }
    }

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        if (objective != null)
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(objective.transform.position, path);
            agent.SetPath(path);
        }
    }
}


