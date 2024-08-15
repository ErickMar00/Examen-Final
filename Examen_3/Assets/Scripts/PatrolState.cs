using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define la clase PatrolState que hereda de MonoBehaviour
public class PatrolState : MonoBehaviour
{
    // Array de puntos de ruta que el enemigo seguirá mientras patrulla
    public Transform[] wayPoints;
    // Color que representará el estado de patrullaje, por defecto es verde
    public Color colorState = Color.green;

    // Referencia al script MachineState, que maneja las transiciones de estados
    private MachineState machineState;
    // Referencia al script NavMeshController, que controla el movimiento del enemigo usando NavMesh
    private NavMeshController navMeshController;
    // Referencia al script EnemySenses, que detecta al jugador y otras percepciones del enemigo
    private EnemySenses enemySenses;
    // Referencia al componente Animator para controlar las animaciones del enemigo
    private Animator animator;
    // Índice del siguiente punto de ruta al que el enemigo se dirigirá
    private int nextWayPoint;

    // Este método se llama cuando el script se inicializa, antes del Start
    private void Awake()
    {
        // Obtiene la referencia del componente MachineState en el mismo GameObject
        machineState = GetComponent<MachineState>();
        // Obtiene la referencia del componente NavMeshController en el mismo GameObject
        navMeshController = GetComponent<NavMeshController>();
        // Obtiene la referencia del componente EnemySenses en el mismo GameObject
        enemySenses = GetComponent<EnemySenses>();
        // Obtiene la referencia del componente Animator en el mismo GameObject
        animator = GetComponent<Animator>();
    }

    // Este método se llama una vez por frame
    void Update()
    {
        RaycastHit hit; // Declara una variable para almacenar la información del objeto golpeado por el Raycast

        // Verifica si el enemigo puede ver al jugador utilizando el método CanSeeThePlayer de EnemySenses
        if (enemySenses.CanSeeThePlayer(out hit))
        {
            // Si el jugador es detectado, establece el objetivo de persecución como el jugador
            navMeshController.pursuitObjective = hit.transform;
            // Cambia al estado de persecución
            machineState.ActivateState(machineState.PursuitState);
            return; // Sale de la función para evitar que se ejecute el resto del código en Update
        }

        // Verifica si el enemigo ha llegado a su punto de destino actual
        if (navMeshController.Arrived())
        {
            // Actualiza el índice para ir al siguiente punto de ruta
            nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
            // Actualiza el punto de destino del NavMesh al siguiente punto de ruta
            UpdateWayPoint();
        }

        // Obtiene la velocidad actual del enemigo y actualiza el parámetro "Speed" del Animator
        float speed = navMeshController.GetSpeed(); // Asume que GetSpeed() devuelve la magnitud de la velocidad actual.
        animator.SetFloat("Speed", speed); // Ajusta el parámetro "Speed" del Animator basado en la velocidad del enemigo.
    }

    // Este método se llama cuando este estado (script) se activa
    void OnEnable()
    {
        // Cambia el color del indicador (probablemente una representación visual del estado) al color del estado actual (colorState)
        machineState.meshRendererIndicador.material.color = colorState;
        // Actualiza el punto de destino del NavMesh al siguiente punto de ruta
        UpdateWayPoint();
    }

    // Método para actualizar el punto de destino del NavMesh al siguiente punto de ruta
    void UpdateWayPoint()
    {
        // Establece el punto de destino del NavMesh al siguiente punto de ruta
        navMeshController.NavMeshDestinationPoint(wayPoints[nextWayPoint].position);
    }

    // Método que se llama cuando otro objeto entra en el trigger del enemigo
    public void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es el jugador y si este estado (PatrolState) está habilitado
        if (other.gameObject.CompareTag("Player") && enabled)
        {
            // Cambia al estado de alerta
            machineState.ActivateState(machineState.AlertState);
        }
    }
}

//Recursos usados en este codigo
// https://www.youtube.com/watch?v=BXLCUg1jzSs&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=6