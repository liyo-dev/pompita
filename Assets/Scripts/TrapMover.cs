using UnityEngine;

public class TrapMover : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
        
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}