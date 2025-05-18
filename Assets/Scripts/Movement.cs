using System;
using UnityEngine;
using Utils;

public class Movement : MonoBehaviour
{
    //public float radius = 5f; // Radio del c�rculo
    //public float moveSpeed = 50f; // Velocidad de movimiento angular (grados por segundo)

    private Rigidbody _rb;
    private float currentAngle = 270f; // �ngulo actual en grados
    private float currentZAxis = 0f;
    private float speed;
    private float targetSpeed;
    private float smoothSpeed;
    private float accelerationTime = 1f;
    private bool isGoingDown;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Pone en la posici�n inicial
        // Convierte el �ngulo a radianes
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calcula la nueva posici�n en el c�rculo en el plano X-Y
        float x = Mathf.Cos(radians) * Utils.Variables.Radius;
        float y = Mathf.Sin(radians) * Utils.Variables.Radius;

        // Calcula la nueva posici�n en 3D y aplica al Rigidbody
        Vector3 newPosition = new Vector3(x, y, transform.position.z); // Mantener la componente Z original
        currentZAxis = transform.position.z;
        _rb.MovePosition(newPosition);
        speed = Utils.Variables.PlayerInitialSpeed;
    }

    private void Update()
    {

        float nextAngle = currentAngle + GetInput() * speed * Time.deltaTime;
        if (nextAngle < 0)
            nextAngle += 360f;
        nextAngle %= 360f;

        speed = CalculateNewSpeed(nextAngle);

        currentAngle = nextAngle;

        MoveToNewPosition();
    }

    private void LateUpdate()
    {
        if (currentAngle < 45 || currentAngle > 135)
            return;

        currentAngle = currentAngle - 90 > 0 ? 134 : 46;

        MoveToNewPosition();
    }

    private void MoveToNewPosition()
    {
        // Convierte el �ngulo a radianes
        float radians = currentAngle * Mathf.Deg2Rad;

        // Calcula la nueva posici�n en el c�rculo en el plano X-Y
        float x = Mathf.Cos(radians) * Utils.Variables.Radius;
        float y = Mathf.Sin(radians) * Utils.Variables.Radius;

        // Calcula la nueva posici�n en 3D y aplica al Rigidbody
        Vector3 newPosition = new Vector3(x, y, currentZAxis); // Mantener la componente Z original
        transform.rotation = Quaternion.Euler(0, 180f, 0);
        _rb.MovePosition(newPosition);
    }


    private float CalculateNewSpeed(float nextAngle)
    {
        if (IsGoingDown(currentAngle, nextAngle))
        {
            targetSpeed = Variables.PlayerSpeed;
        }
        else
        {
            targetSpeed = Variables.PlayerInitialSpeed;
        }

        return Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeed, accelerationTime);
    }

    private bool IsGoingDown(float currentAngle, float nextAngle)
    {
        bool inRangeIzda = nextAngle < 270 && nextAngle > 135;

        if (inRangeIzda)
            return nextAngle < currentAngle;
        else
        {
            float aux1 = currentAngle + 90;

            if (aux1 > 360)
                aux1 -= 360;

            float aux2 = nextAngle + 90;
            if (aux2 > 360)
                aux2 -= 360;

            return aux2 > aux1;
        }
    }

    private float GetInput()
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
