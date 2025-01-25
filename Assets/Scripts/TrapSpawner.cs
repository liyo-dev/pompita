using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    //public float spawnDistanceZ = 50f;
    public float trapSpeed = 10f;
    public float spawnRate = 2f;
    public float spawnRangeX = 1f;
    public float velocSpeedMultiplier = 1f;
    public List<GameObject> trapPrefabs;

    // 0 -> Puntos
    // 1 -> Bomba
    // 2 -> Vida

    private void Start()
    {
        StartCoroutine(SpawnTraps());
    }

    private IEnumerator SpawnTraps()
    {
        while (true)
        {
            SpawnTrap();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void SpawnTrap()
    {
        // Calcular las posiciones en un semicírculo
        Vector3[] spawnPositions = new Vector3[10];
        float angleStep = 360f / (spawnPositions.Length - 1);
        float radius = spawnRangeX;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            float angle = angleStep * i;
            float radian = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius;
            spawnPositions[i] = new Vector3(x, y, Utils.Variables.SpawnDistanceZ);
        }

        // Elegir una posición aleatoria
        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        // Elegir un prefab aleatorio
        GameObject trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Count)];

        // Instanciar la trampa
        GameObject trap = Instantiate(trapPrefab, spawnPosition, Quaternion.identity);

        // Asignar el script de movimiento a la trampa
        trap.AddComponent<TrapMover>().speed = trapSpeed * velocSpeedMultiplier;
    }
    
    // Método para cambiar la velocidad de las trampas
    public void SetVelocSpeedMultiplier(float multiplier)
    {
        velocSpeedMultiplier = multiplier;
    }
    
    // Método para resetear la velocidad de las trampas
    public void ResetVelocSpeedMultiplier()
    {
        velocSpeedMultiplier = 1f;
    }
}