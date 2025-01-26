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
    private VelocityGameController velocityGameController;

    // 0 -> Puntos
    // 1 -> Bomba
    // 2 -> Vida

    private void Start()
    {
        velocityGameController = FindObjectOfType<VelocityGameController>();
        velocityGameController.OnIncrementLevel += IncrementLevel;
        StartCoroutine(SpawnTraps());
    }

    private void IncrementLevel()
    {
        // Aumentar la velocidad de las trampas
        velocSpeedMultiplier += 0.2f;

        // Disminuir la tasa de generación de trampas para que salgan más rápido
        spawnRate = Mathf.Max(0.5f, spawnRate * 0.9f); // Se reduce hasta un mínimo de 0.5 segundos

        // Modificar la proporción de trampas generadas
        AdjustTrapProbabilities();
    }
    
    // Método para ajustar las probabilidades de las trampas
private void AdjustTrapProbabilities()
{
    // Filtrar los prefabs por tipo
    List<GameObject> bombs = new List<GameObject>();
    List<GameObject> lives = new List<GameObject>();
    List<GameObject> points = new List<GameObject>();

    foreach (var trap in trapPrefabs)
    {
        if (trap.name.Contains("BOMBA")) 
            bombs.Add(trap);
        else if (trap.name.Contains("EXTRALIFE")) 
            lives.Add(trap);
        else
            points.Add(trap);
    }

    // Ajustar probabilidades dinámicamente
    trapPrefabs.Clear();

    int bombCount = Mathf.Clamp(5 + velocityGameController.Level * 3, 5, 50);  // Aumenta con el nivel
    int lifeCount = Mathf.Clamp(5 - velocityGameController.Level, 0, 5);       // Reduce con el nivel
    int pointCount = Mathf.Clamp(5 + velocityGameController.Level * 2, 5, 100); // Más puntos con nivel

    // Añadir bombas a la lista
    for (int i = 0; i < bombCount; i++)
    {
        if (bombs.Count > 0) trapPrefabs.Add(bombs[Random.Range(0, bombs.Count)]);
    }

    // Añadir vidas a la lista (menos con el nivel)
    for (int i = 0; i < lifeCount; i++)
    {
        if (lives.Count > 0) trapPrefabs.Add(lives[Random.Range(0, lives.Count)]);
    }

    // Añadir puntos a la lista
    for (int i = 0; i < pointCount; i++)
    {
        if (points.Count > 0) trapPrefabs.Add(points[Random.Range(0, points.Count)]);
    }
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