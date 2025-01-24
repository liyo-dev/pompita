using UnityEngine;
using Utils;

public class Movement : MonoBehaviour
{
    //public float radius = 5f; // Radio del círculo
    //public float moveSpeed = 50f; // Velocidad de movimiento angular (grados por segundo)

    private Rigidbody _rb;
    private float currentAngle = 0f; // Ángulo actual en grados

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Pone en la posición inicial
        // Convierte el ángulo a radianes
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calcula la nueva posición en el círculo en el plano X-Y
        float x = Mathf.Cos(radians) * Utils.Variables.Radius;
        float y = Mathf.Sin(radians) * Utils.Variables.Radius;

        // Calcula la nueva posición en 3D y aplica al Rigidbody
        Vector3 newPosition = new Vector3(x, y, transform.position.z); // Mantener la componente Z original
        _rb.MovePosition(newPosition);
    }

    private void Update()
    {
        currentAngle += GetInput() * Utils.Variables.PlayerSpeed * Time.deltaTime;

        // Mantén el ángulo dentro de 0-360 grados
        currentAngle %= 360f;

        // Convierte el ángulo a radianes
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calcula la nueva posición en el círculo en el plano X-Y
        float x = Mathf.Cos(radians) * Utils.Variables.Radius;
        float y = Mathf.Sin(radians) * Utils.Variables.Radius;

        // Calcula la nueva posición en 3D y aplica al Rigidbody
        Vector3 newPosition = new Vector3(x, y, transform.position.z); // Mantener la componente Z original
        _rb.MovePosition(newPosition);
    }

    private float GetInput ()
    {
        #if UNITY_EDITOR
        float input = 0;

        if (Input.GetKey(KeyCode.A))
            input--;
        if (Input.GetKey(KeyCode.D))
            input++;

        return input;
        #endif

        float horizontalTilt = Input.acceleration.x;

        return horizontalTilt;
    }

}
