using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Importa la biblioteca necesaria para trabajar con NavMesh

// Define la clase NavMeshController que hereda de MonoBehaviour
public class NavMeshController : MonoBehaviour
{
    // Variable p�blica que almacena el objetivo de la persecuci�n, pero que no se muestra en el Inspector de Unity
    [HideInInspector]
    public Transform pursuitObjective;

    // Variable privada que almacena la referencia al componente NavMeshAgent del GameObject
    private NavMeshAgent navMeshAgent;

    // Este m�todo se llama cuando el script se inicializa, antes del Start
    void Awake()
    {
        // Obtiene la referencia del componente NavMeshAgent en el mismo GameObject
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // M�todo p�blico que establece un punto de destino en el NavMesh utilizando un Vector3
    public void NavMeshDestinationPoint(Vector3 destinationPoint)
    {
        // Establece el punto de destino del agente en el NavMesh
        navMeshAgent.destination = destinationPoint;
        // Reanuda el movimiento del agente si estaba detenido
        navMeshAgent.Resume();
    }

    // Sobrecarga del m�todo NavMeshDestinationPoint() que establece el destino como la posici�n del objetivo de persecuci�n
    public void NavMeshDestinationPoint()
    {
        // Llama al m�todo NavMeshDestinationPoint con la posici�n del objetivo de persecuci�n
        NavMeshDestinationPoint(pursuitObjective.position);
    }

    // M�todo p�blico que detiene el movimiento del agente en el NavMesh
    public void StopNavMesh()
    {
        // Detiene al agente en su posici�n actual
        navMeshAgent.Stop();
    }

    // M�todo p�blico que verifica si el agente ha llegado a su destino
    public bool Arrived()
    {
        // Devuelve true si el agente ha llegado a su destino, es decir, si la distancia restante es menor o igual a la distancia de frenado y no hay una ruta pendiente
        return navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending;
    }

    // Nuevo m�todo para obtener la velocidad actual del agente en el NavMesh
    public float GetSpeed()
    {
        // Devuelve la magnitud de la velocidad actual del agente (la magnitud del vector de velocidad)
        return navMeshAgent.velocity.magnitude;
    }
}


//Recursos usados para el codigo 
//https://www.youtube.com/watch?v=XSTtkGPDbSE&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=4