using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshVehicle : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    NavMeshPath path;
    [SerializeField] bool spawner;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (startPoint == null)
        {
            startPoint = transform;
        }

        if (endPoint == null) 
        {
            endPoint = transform;
            agent.SetDestination(endPoint.position);
        }        
    }
    public void StopNavigating()
    {        
        agent.isStopped = true;
    }

    public void StartNavigating()
    {
        agent.isStopped = false;
    }
}
