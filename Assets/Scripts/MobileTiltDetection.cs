using UnityEngine;

public class MobileTiltDetection : MonoBehaviour
{
    void Update()
    {
        // Obtiene la aceleración del dispositivo
        Vector3 tilt = Input.acceleration;

        // Extrae la inclinación horizontal (eje X)
        float horizontalTilt = tilt.x;

        // Muestra el valor en la consola
        Debug.Log("Inclinación Horizontal: " + horizontalTilt);

        // Puedes usar el valor para mover un objeto, por ejemplo:
        float speed = 50f;
        float targetPositionX = transform.position.x + horizontalTilt * speed * Time.deltaTime;
        float smoothPositionX = Mathf.Lerp(transform.position.x, targetPositionX, 0.1f); 
        transform.position = new Vector3(smoothPositionX, transform.position.y, transform.position.z);
    }
}