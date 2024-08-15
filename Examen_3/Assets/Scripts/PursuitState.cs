using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define la clase PursuitState que hereda de MonoBehaviour
public class PursuitState : MonoBehaviour
{
    // Color que representar� el estado de persecuci�n, por defecto es rojo
    public Color colorState = Color.red;

    // Referencia al script MachineState, que probablemente maneja las transiciones de estados
    private MachineState machineState;
    // Referencia al script NavMeshController, que controla el movimiento del enemigo usando NavMesh
    private NavMeshController navMeshController;
    // Referencia al script EnemySenses, que detecta al jugador y otras percepciones del enemigo
    private EnemySenses enemySenses;

    // Este m�todo se llama cuando el script se inicializa, antes del Start
    void Awake()
    {
        // Obtiene la referencia del componente MachineState en el mismo GameObject
        machineState = GetComponent<MachineState>();
        // Obtiene la referencia del componente NavMeshController en el mismo GameObject
        navMeshController = GetComponent<NavMeshController>();
        // Obtiene la referencia del componente EnemySenses en el mismo GameObject
        enemySenses = GetComponent<EnemySenses>();
    }

    // Este m�todo se llama cuando este estado (script) se activa
    void OnEnable()
    {
        // Cambia el color del indicador (probablemente una representaci�n visual del estado) al color del estado actual (colorState)
        machineState.meshRendererIndicador.material.color = colorState;
    }

    // Este m�todo se llama una vez por frame
    void Update()
    {
        RaycastHit hit; // Declara una variable para almacenar la informaci�n del objeto golpeado por el Raycast

        // Verifica si el enemigo no puede ver al jugador utilizando el m�todo CanSeeThePlayer de EnemySenses
        if (!enemySenses.CanSeeThePlayer(out hit, true))
        {
            // Si el jugador no est� en la vista del enemigo, activa el estado de alerta
            machineState.ActivateState(machineState.AlertState);
            return; // Sale de la funci�n para evitar que se ejecute el resto del c�digo en Update
        }

        // Establece un nuevo punto de destino en el NavMesh para perseguir al jugador
        navMeshController.NavMeshDestinationPoint();
    }
}


//Recursos del usados para el codigo
// https://www.youtube.com/watch?v=aHS38SmJ5l0&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=8