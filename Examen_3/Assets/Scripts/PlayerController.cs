using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Velocidad del jugador
    public float rotationSpeed = 720f; // Velocidad de rotaci�n en grados por segundo

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener la entrada del usuario
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Normalizar el vector de movimiento para asegurar que la velocidad sea consistente en todas direcciones
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Aplicar movimiento al Rigidbody
        rb.velocity = movement * speed;

        // Actualizar el par�metro 'Speed' en el Animator basado en la magnitud del movimiento
        float movementSpeed = movement.magnitude;
        animator.SetFloat("Speed", movementSpeed);

        // Si el personaje se est� moviendo, rotarlo en la direcci�n del movimiento
        if (movementSpeed > 0.1f)
        {
            RotateCharacter(movement);
        }
    }

    void RotateCharacter(Vector3 direction)
    {
        // Calcular la rotaci�n hacia la direcci�n de movimiento
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Aplicar una rotaci�n suave hacia la direcci�n de movimiento
        rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
