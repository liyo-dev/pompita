using UnityEngine;

public class TrapMover : MonoBehaviour
{
    public float speed = 10f;
    private TrapSpawner trapSpawner;
    bool stop = false;

    public void AddSpawner(TrapSpawner spawner)
    {
        trapSpawner = spawner;
        trapSpawner.FinishAction += StopGame;
    }

    private void Update()
    {
        if (stop)
            return;
        transform.position += Vector3.back * speed * Time.deltaTime;
        
        if (transform.position.z < -10f)
        {
            if (trapSpawner != null)
                trapSpawner.FinishAction -= StopGame;
            Destroy(gameObject);
        }
    }

    private void StopGame()
    {
        stop = true;
    }
}