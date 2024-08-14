using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al transform del jugador
    public Vector3 offset;    // Desplazamiento de la c�mara respecto al jugador
    public float smoothSpeed = 0.125f;  // Velocidad de suavizado del movimiento de la c�mara

    // Variables p�blicas para ajustar la rotaci�n desde el Inspector
    public float rotationX = 0f;
    public float rotationY = 0f;
    public float rotationZ = 0f;

    private void LateUpdate()
    {
        // Calcula la posici�n deseada de la c�mara
        Vector3 desiredPosition = player.position + offset;

        // Interpola suavemente entre la posici�n actual de la c�mara y la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Actualiza la posici�n de la c�mara
        transform.position = smoothedPosition;

        // Aplica la rotaci�n configurada desde el Inspector
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

        // Puedes descomentar la siguiente l�nea si quieres que la c�mara mire siempre al jugador
        // transform.LookAt(player);
    }
}

