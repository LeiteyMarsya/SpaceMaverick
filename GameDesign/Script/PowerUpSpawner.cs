using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject shieldPowerUpPrefab; // Assign your ShieldPowerUp prefab here
    public float spawnInterval = 10f; // Time in seconds between spawns
    public Vector2 spawnAreaMin; // Minimum x and y values for the spawn area
    public Vector2 spawnAreaMax; // Maximum x and y values for the spawn area

    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Generate a random position within the spawn area
            Vector2 spawnPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            // Instantiate the ShieldPowerUp at the spawn position
            Instantiate(shieldPowerUpPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
