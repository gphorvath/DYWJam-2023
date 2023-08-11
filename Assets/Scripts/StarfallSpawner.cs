using UnityEngine;
using System.Collections.Generic;

public class StarfallSpawner : MonoBehaviour
{
    public Star starPrefab;    // Drag the prefab of your falling star here

    // Define target spawn area
    public Vector2 spawnAreaCenter = new Vector2(0, 10);  // The center of the spawning area
    public Vector2 spawnAreaSize = new Vector2(10, 5);    // Width and height of the spawning area

    public float minSpawnInterval = 1f; // Minimum interval between spawns
    public float maxSpawnInterval = 3f; // Maximum interval between spawns

    public int minSpawnDensity = 1; // Minimum number of stars to spawn per spawn event
    public int maxSpawnDensity = 5; // Maximum number of stars to spawn per spawn event

    private bool isPaused = false;
    private List<Star> spawnedStars = new List<Star>(); // Store references to spawned stars

    private void Start()
    {
        // Start the spawn loop
        Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    public void ToggleSpawning(bool pause)
    {
        isPaused = pause;

        if (isPaused)
        {
            CancelInvoke("SpawnStar"); // Stop the spawn loop

            // Freeze all spawned stars
            foreach (Star star in spawnedStars)
            {
                if (star) star.StopFalling();
            }
        }
        else
        {
            Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval)); // Resume the spawn loop

            // Unfreeze all spawned stars
            foreach (Star star in spawnedStars)
            {
                if (star) star.StartFalling();
            }
        }
    }

    private void SpawnStar()
    {
        if (!isPaused)
        {
            int spawnCount = Random.Range(minSpawnDensity, maxSpawnDensity + 1);
            
            for (int i = 0; i < spawnCount; i++)
            {
                // Calculate random spawn position within the target area
                Vector3 spawnPosition = new Vector3(
                    Random.Range(spawnAreaCenter.x - spawnAreaSize.x / 2, spawnAreaCenter.x + spawnAreaSize.x / 2),
                    Random.Range(spawnAreaCenter.y - spawnAreaSize.y / 2, spawnAreaCenter.y + spawnAreaSize.y / 2),
                    0
                );

                // Create a star at that position and store reference
                Star newStar = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
                if (newStar) spawnedStars.Add(newStar);
            }

            // Schedule the next spawn
            Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }

    // This method is called to draw gizmos in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
