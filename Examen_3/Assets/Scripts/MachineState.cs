using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; // Importa la biblioteca de Visual Scripting de Unity
using UnityEngine;

// Define la clase MachineState que hereda de MonoBehaviour
public class MachineState : MonoBehaviour
{
    // Referencias a los diferentes estados que el enemigo puede tener
    public MonoBehaviour PatrolState;   // Estado de patrullaje
    public MonoBehaviour AlertState;    // Estado de alerta
    public MonoBehaviour PursuitState;  // Estado de persecución
    public MonoBehaviour IntitialState; // Estado inicial

    // Referencia al componente MeshRenderer que se utilizará para cambiar el color del indicador del estado
    public MeshRenderer meshRendererIndicador;

    // Variable privada que almacena el estado actual activo
    private MonoBehaviour currentStatus;

    // Este método se llama cuando el script se inicializa
    void Start()
    {
        // Activa el estado inicial configurado al inicio del juego
        ActivateState(IntitialState);
    }

    // Método público para activar un nuevo estado
    public void ActivateState(MonoBehaviour newState)
    {
        // Si hay un estado activo, lo desactiva
        if (currentStatus != null) currentStatus.enabled = false;
        // Establece el nuevo estado como el estado actual
        currentStatus = newState;
        // Activa el nuevo estado
        currentStatus.enabled = true;
    }
}

//Recursos que se usaron para el proyecto!
//https://www.youtube.com/watch?v=2A-DSbBWWXk&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=3