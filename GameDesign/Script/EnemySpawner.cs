using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field: SerializeField]
    public float SpawnRate { get; set; } = 3;

    [field: SerializeField]
    public List<EnemyShipController> EnemyQueue { get; private set; } = new();

    void Start()
    {
        // Check if EnemyQueue has elements before invoking SpawnNext()
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
            EnemyShipController spawnedEnemy = EnemyShipController.Spawn(enemy);
            spawnedEnemy.transform.position = enemy.transform.position; // Set the correct position
            spawnedEnemy.SetWayPoints(enemy.WayPoints);

        }
    }



    public void EnqueueEnemies(List<EnemyShipController> enemies)
    {
        foreach (EnemyShipController e in enemies)
        {
            EnemyQueue.Add(e);
        }
    }

}
