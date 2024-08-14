using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
    public Transform[] wayPoints;

    private NavMeshController navMeshController;
    private Animator animator;
    private int nextWayPoint;

    private void Awake()
    {
        navMeshController = GetComponent<NavMeshController>();
        animator = GetComponent<Animator>(); // Obtiene el Animator del enemigo.
    }

    void Update()
    {
        if (navMeshController.Arrived())
        {
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
            UpdateWayPoint();
        }

        // Actualiza el parámetro "Speed" en función de la velocidad del enemigo.
        float speed = navMeshController.GetSpeed(); // Asume que GetSpeed() devuelve la magnitud de la velocidad actual.
        animator.SetFloat("Speed", speed);
    }

    void OnEnable()
    {
        UpdateWayPoint();
    }

    void UpdateWayPoint()
    {
        navMeshController.NavMeshDestinationPoint(wayPoints[nextWayPoint].position);
    }
}
