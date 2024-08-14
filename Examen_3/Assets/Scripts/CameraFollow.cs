using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public Vector3 offset;    // Desplazamiento de la cámara respecto al jugador
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado del movimiento de la cámara

    // Variables públicas para ajustar la rotación desde el Inspector
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float rotationZ = 0f;

    private void LateUpdate()
    {
        // Calcula la posición deseada de la cámara
        Vector3 desiredPosition = player.position + offset;

        // Interpola suavemente entre la posición actual de la cámara y la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posición de la cámara
        transform.position = smoothedPosition;

        // Aplica la rotación configurada desde el Inspector
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

        // Puedes descomentar la siguiente línea si quieres que la cámara mire siempre al jugador
        // transform.LookAt(player);
    }
}

