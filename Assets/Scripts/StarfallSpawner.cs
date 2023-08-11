using UnityEngine;

public class StarfallSpawner : MonoBehaviour
{
    public GameObject starPrefab;    // Drag the prefab of your falling star here
    public Transform playerTransform; // Reference to your player

    public float minSpawnInterval = 1f; // Minimum interval between spawns
    public float maxSpawnInterval = 3f; // Maximum interval between spawns

    public float spawnHeightAbovePlayer = 10f; // Average height above the player to spawn the stars
    public float spawnHeightVariation = 2f;    // Variation in the spawn height
    public float spawnHorizontalRange = 5f;    // Horizontal range for randomness

    public int minSpawnDensity = 1; // Minimum number of stars to spawn per spawn event
    public int maxSpawnDensity = 5; // Maximum number of stars to spawn per spawn event

    private void Start()
    {
        // Start the spawn loop
        Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    private void SpawnStar()
    {
        if (playerTransform)
        {
            int spawnCount = Random.Range(minSpawnDensity, maxSpawnDensity + 1);
            
            for (int i = 0; i < spawnCount; i++)
            {
                float variedHeight = Random.Range(-spawnHeightVariation, spawnHeightVariation);

                // Calculate random spawn position above the player with varied height
                Vector3 spawnPosition = playerTransform.position + new Vector3(
                    Random.Range(-spawnHorizontalRange, spawnHorizontalRange),
                    spawnHeightAbovePlayer + variedHeight,
                    0
                );

                // Create a star at that position
                Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            }

            // Schedule next spawn
            Invoke("SpawnStar", Random.Range(minSpawnInterval, maxSpawnInterval));
        }
    }
}
