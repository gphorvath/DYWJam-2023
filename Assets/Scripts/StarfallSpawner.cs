using UnityEngine;
using System.Collections.Generic;

public class StarfallSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public int maxSpawnedStars = 50;
    public Star starPrefab;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    private int currentSpawnCount = 0;


    [Header("Spawn Area")]
    public Vector2 spawnAreaCenter = new Vector2(0, 10);
    public Vector2 spawnAreaSize = new Vector2(10, 5);

    private bool isPaused = false;
    private List<Star> spawnedStars = new List<Star>();

    private void Start()
    {
        Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    public void ToggleSpawning(bool pause)
    {
        isPaused = pause;

        if (isPaused)
        {
            CancelInvoke("SpawnStar");

            foreach (Star star in spawnedStars)
            {
                if (star) star.StopFalling();
            }
        }
        else
        {
            Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));

            foreach (Star star in spawnedStars)
            {
                if (star) star.StartFalling();
            }
        }
    }

    private void SpawnStar()
    {
        if (!isPaused && currentSpawnCount < maxSpawnedStars)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                0
            );

            Star newStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            if (newStar)
            {
                spawnedStars.Add(newStar);
                currentSpawnCount++;
            }

            Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
