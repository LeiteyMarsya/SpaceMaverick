using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    public float SpawnRate { get; set; } = 3;

    [field: SerializeField]
    public List<EnemyShipController> EnemyQueue { get; private set; } = new();

    [field: SerializeField]
    public GameController GameController { get; set; }

    void Start()
    {
        if (EnemyQueue.Count > 0)
        {
            this.InvokeRepeating(nameof(SpawnNext), SpawnRate, SpawnRate);
        }
    }

    public void SpawnNext()
    {
        if (EnemyQueue.Count > 0)
        {
            EnemyShipController enemy = EnemyQueue[0];
            EnemyQueue.RemoveAt(0);
            EnemyShipController spawnedEnemy = EnemyShipController.Spawn(enemy, GameController);
            spawnedEnemy.transform.position = enemy.transform.position; // Set the correct position
        }
    }

    // Add the EnqueueEnemies method here
    public void EnqueueEnemies(List<EnemyShipController> enemies)
    {
        foreach (EnemyShipController e in enemies)
        {
            EnemyQueue.Add(e);
        }
    }

    public void ResetSpawner()
    {
        CancelInvoke(nameof(SpawnNext)); // Cancel any ongoing spawning
        EnemyQueue.Clear();              // Clear existing queue

        // Re-enqueue enemies if necessary
        EnqueueEnemies(GameController.Instance.enemies);

        if (EnemyQueue.Count > 0)
        {
            InvokeRepeating(nameof(SpawnNext), SpawnRate, SpawnRate);
        }
    }


}
