using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachineState : MonoBehaviour {
    public MonoBehaviour PatrolState;
    public MonoBehaviour AlertState;
    public MonoBehaviour PursuitState;
    public MonoBehaviour IntitialState;

    private MonoBehaviour currentStatus;

    void Start() {
        ActivateState(IntitialState);
    }

    public void ActivateState(MonoBehaviour newState) {
        if(currentStatus != null) currentStatus.enabled = false;
        currentStatus = newState;
        currentStatus.enabled = true;
    }

}

//Recursos que se usaron para el proyecto!
//https://www.youtube.com/watch?v=2A-DSbBWWXk&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=3