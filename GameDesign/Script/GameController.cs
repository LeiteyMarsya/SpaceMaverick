using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    public List<EnemyShipController> EnemyTypes { get; private set; }

    [field:SerializeField]
    public Transform EnemySpawnPoint {  get; private set; }

    [field:SerializeField]
    public List<Transform> WayPoints { get; set; }

    [field: SerializeField]
    public EnemySpawner  EnemySpawner {  get; private set; }

    [field: SerializeField]
    public PlayerController PlayerTemplate { get; private set; }

    [field: SerializeField]
    public Transform PlayerSpawnPoint { get; private set; }

    [field: SerializeField]
    public float SpawnDelay { get; private set; } = 3;

    [field: SerializeField]
    public float SpawnAt { get; private set; } = -1;
    [field: SerializeField]
    public List<EnemyShipController> enemies { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        List<Transform> waypoints = new() { WayPoints[0], WayPoints[1], WayPoints[2] };
        IEnemyShip enemy = new EnemyShip(EnemyTypes[0], waypoints, EnemySpawnPoint.position);

        List<IEnemyShip> enemyShips = new()
        {
            enemy, enemy, enemy
        };
        EnemySpawner.EnqueueEnemies(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        PlayerController pc = Instantiate(PlayerTemplate);
        pc.GameController = this;
        pc.transform.position = PlayerSpawnPoint.position;
        SpawnAt = -1;
    }

    public void DestroyPlayer(PlayerController toDestroy)
    { 
        Destroy(toDestroy.gameObject);
        SpawnAt = Time.time + SpawnDelay;
    }
}
