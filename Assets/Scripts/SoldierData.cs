using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SoldierData : MonoBehaviour
{
    [SerializeField] string soldierName;
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] Squad squad;
    [SerializeField] Company company;
    [SerializeField] Army army;
    [SerializeField] Faction faction;

    NavMeshAgent agent;
    GameObject objective;

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


