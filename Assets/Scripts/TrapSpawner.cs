using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrapSpawner : MonoBehaviour
{
    public float trapSpeed = 10f;
    public float spawnRate = 2f;
    public float spawnRangeX = 1f;
    public float velocSpeedMultiplier = 1f;
    public List<GameObject> trapPrefabs;
    private VelocityGameController velocityGameController;
    public UnityAction FinishAction;

    private void Start()
    {
        velocityGameController = FindObjectOfType<VelocityGameController>();
        velocityGameController.OnIncrementLevel += IncrementLevel;
        StartCoroutine(SpawnTraps());
    }

    private void IncrementLevel()
    {
        velocSpeedMultiplier += 0.2f;
        spawnRate = Mathf.Max(0.5f, spawnRate * 0.9f);
        AdjustTrapProbabilities();
    }

    private void AdjustTrapProbabilities()
    {
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

        trapPrefabs.Clear();

        int bombCount = Mathf.Clamp(10 + velocityGameController.Level * 4, 10, 100);
        int lifeCount = Mathf.Clamp(2 + (5 - velocityGameController.Level / 2), 1, 10);
        int pointCount = Mathf.Clamp(15 + velocityGameController.Level * 3, 15, 150);

        for (int i = 0; i < bombCount; i++)
        {
            if (bombs.Count > 0) trapPrefabs.Add(bombs[Random.Range(0, bombs.Count)]);
        }

        for (int i = 0; i < lifeCount; i++)
        {
            if (lives.Count > 0) trapPrefabs.Add(lives[Random.Range(0, lives.Count)]);
        }

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
        Vector3[] spawnPositions = new Vector3[10];
        float angleStep = 180f / (spawnPositions.Length - 1); // Solo ángulos inferiores
        float radius = spawnRangeX;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            float angle = angleStep * i - 90f; // Limitamos de -90° a 90°
            float radian = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(radian) * radius;
            float y = Mathf.Sin(radian) * radius * 0.5f; // Mantener las trampas abajo
            spawnPositions[i] = new Vector3(x, y, Utils.Variables.SpawnDistanceZ);
        }

        Vector3 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
        GameObject trapPrefab = trapPrefabs[Random.Range(0, trapPrefabs.Count)];
        GameObject trap = Instantiate(trapPrefab, spawnPosition, Quaternion.identity);

        TrapMover trapmover = trap.AddComponent<TrapMover>();
        trapmover.speed = trapSpeed * velocSpeedMultiplier;
        trapmover.AddSpawner(this);
    }

    public void SetVelocSpeedMultiplier(float multiplier)
    {
        if (multiplier == 0)
        {
            StopAllCoroutines();
            FinishAction.Invoke();
        }

        velocSpeedMultiplier = multiplier;
    }

    public void ResetVelocSpeedMultiplier()
    {
        velocSpeedMultiplier = 1f;
    }
}
