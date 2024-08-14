using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{

    [HideInInspector]
    public Transform pursuitObjective;

    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void NavMeshDestinationPoint(Vector3 destinationPoint)
    {
        navMeshAgent.destination = destinationPoint;
        navMeshAgent.Resume();
    }

    public void NavMeshDestinationPoint()
    {
        NavMeshDestinationPoint(pursuitObjective.position);
    }

    public void StopNavMesh()
    {
        navMeshAgent.Stop();
    }

    public bool Arrived()
    {
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }

    // Nuevo método para obtener la velocidad actual
    public float GetSpeed()
    {
        return navMeshAgent.velocity.magnitude;
    }
}

//Recursos usados para el codigo 
//https://www.youtube.com/watch?v=XSTtkGPDbSE&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=4