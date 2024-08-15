using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define la clase AlertState que hereda de MonoBehaviour
public class AlertState : MonoBehaviour
{
    // Velocidad de rotaci�n en el estado de b�squeda (alerta)
    public float serchVelocitySpin = 120f;
    // Tiempo que el enemigo permanecer� en estado de b�squeda
    public float timeToSerch = 4f;
    // Color que representar� el estado de alerta, por defecto es amarillo
    public Color colorState = Color.yellow;

    // Referencia al script MachineState, que maneja las transiciones de estados
    private MachineState machineState;
    // Referencia al script NavMeshController, que controla el movimiento del enemigo usando NavMesh
    private NavMeshController navMeshController;
    // Referencia al script EnemySenses, que detecta al jugador y otras percepciones del enemigo
    private EnemySenses enemySenses;
    // Variable para controlar el tiempo que el enemigo ha estado buscando
    private float serchTime;

    // Este m�todo se llama cuando el script se inicializa, antes del Start
    private void Awake()
    {
        // Obtiene la referencia del componente MachineState en el mismo GameObject
        machineState = GetComponent<MachineState>();
        // Obtiene la referencia del componente NavMeshController en el mismo GameObject
        navMeshController = GetComponent<NavMeshController>();
        // Obtiene la referencia del componente EnemySenses en el mismo GameObject
        enemySenses = GetComponent<EnemySenses>();
    }

    // Este m�todo se llama cuando este estado (script) se activa
    private void OnEnable()
    {
        // Cambia el color del indicador (probablemente una representaci�n visual del estado) al color del estado actual (colorState)
        machineState.meshRendererIndicador.material.color = colorState;
        // Detiene el movimiento del agente en el NavMesh al entrar en estado de alerta
        navMeshController.StopNavMesh();
        // Reinicia el contador de tiempo de b�squeda
        serchTime = 0;
    }

    // Este m�todo se llama una vez por frame
    void Update()
    {
        RaycastHit hit; // Declara una variable para almacenar la informaci�n del objeto golpeado por el Raycast

        // Verifica si el enemigo puede ver al jugador utilizando el m�todo CanSeeThePlayer de EnemySenses
        if (enemySenses.CanSeeThePlayer(out hit))
        {
            // Si el jugador es detectado, establece el objetivo de persecuci�n como el jugador
            navMeshController.pursuitObjective = hit.transform;
            // Cambia al estado de persecuci�n
            machineState.ActivateState(machineState.PursuitState);
            return; // Sale de la funci�n para evitar que se ejecute el resto del c�digo en Update
        }

        // Si no detecta al jugador, el enemigo gira en su lugar buscando al jugador
        transform.Rotate(0f, serchVelocitySpin * Time.deltaTime, 0f);
        // Incrementa el tiempo de b�squeda
        serchTime += Time.deltaTime;

        // Si el tiempo de b�squeda ha excedido el l�mite establecido
        if (serchTime >= timeToSerch)
        {
            // Cambia al estado de patrullaje
            machineState.ActivateState(machineState.PatrolState);
            return;
        }
    }
}


//Recursos usados para el codigo
// https://www.youtube.com/watch?v=ncreicY5Q5w&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=7