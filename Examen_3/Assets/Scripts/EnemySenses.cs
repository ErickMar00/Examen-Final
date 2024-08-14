using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // Necesario para usar Handles

public class EnemySenses : MonoBehaviour
{
    public Transform enemyHead;
    public float visionRange = 20f;
    public float visionAngle = 45f; // Ángulo del cono de visión
    public Vector3 offset = new Vector3(0f, 0.75f, 0f);

    private NavMeshController navMeshController;

    void Awake()
    {
        navMeshController = GetComponent<NavMeshController>();
    }

    public bool CanSeeThePlayer(out RaycastHit hit, bool lookToPlayer = false)
    {
        Vector3 vectorDirection;
        if (lookToPlayer)
        {
            vectorDirection = (navMeshController.pursuitObjective.position + offset) - enemyHead.position;
        }
        else
        {
            vectorDirection = enemyHead.forward;
        }

        return Physics.Raycast(enemyHead.position, vectorDirection, out hit, visionRange) && hit.collider.CompareTag("Player");
    }

    // Método para dibujar el cono de visión en el plano XZ
    void OnDrawGizmos()
    {
        if (enemyHead != null)
        {
            Gizmos.color = Color.red; // Color del cono de visión

            // Dirección central del cono (forward en XZ)
            Vector3 forwardXZ = new Vector3(enemyHead.forward.x, 0, enemyHead.forward.z).normalized;

            // Calcular las direcciones de los bordes del cono
            Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * forwardXZ * visionRange;
            Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * forwardXZ * visionRange;

            // Dibuja el cono de visión
            Gizmos.DrawRay(enemyHead.position, leftBoundary);
            Gizmos.DrawRay(enemyHead.position, rightBoundary);
            Gizmos.DrawRay(enemyHead.position, forwardXZ * visionRange);

#if UNITY_EDITOR
            // Dibuja un arco entre los límites del cono
            Handles.color = new Color(1, 0, 0, 0.2f); // Color del arco con transparencia
            Handles.DrawSolidArc(enemyHead.position, Vector3.up, leftBoundary.normalized, visionAngle, visionRange);
#endif
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{

    public Transform enemyHead;
    public float visionRange = 20f;
    public Vector3 offset = new Vector3(0f, 0.75f, 0f);

    private NavMeshController navMeshController;

    void Awake()
    {
        navMeshController = GetComponent<NavMeshController>();
    }

    public bool CanSeeThePlayer(out RaycastHit hit, bool lookToPlayer = false) {

        Vector3 vectorDirection;
        if (lookToPlayer)
        {
            vectorDirection = (navMeshController.pursuitObjective.position + offset) - enemyHead.position;
        }
        else {
            vectorDirection = enemyHead.forward;
        }

        return Physics.Raycast(enemyHead.position, vectorDirection, out hit, visionRange) && hit.collider.CompareTag("Player");

    }


}
*/

//Recursos para este codigo 
// https://www.youtube.com/watch?v=PeiKu7W76Vo&list=PLREdURb87ks3CSDR5GG8FysPA_d9XhYjb&index=5