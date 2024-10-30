using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidSpawner : MonoBehaviour
{
    [field: SerializeField]
    public float SpawnRate = 2;

    [field: SerializeField]
    public float LastSpawn = 0;

    [field: SerializeField]
    public float MinRotation { get; private set; } = -90;

    [field: SerializeField]
    public float MaxRotation { get; private set; } = 90;

    [field: SerializeField]
    public Vector2 MinSpeed;

    [field: SerializeField]
    public Vector2 MaxSpeed;

    [field: SerializeField]
    public Transform MinSpawnPoint;

    [field: SerializeField]
    public Transform MaxSpawnPoint;

    public Vector2 MinSpawnVector => MinSpawnPoint.position;
    public Vector2 MaxSpawnVector => MaxSpawnPoint.position;

    [field: SerializeField]
    public AsteroidController Template;
    public GameController GameController {  get; private set; }

    void Start()
    {
        LastSpawn = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnAsteroid();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LaserControl asLaser = collision.GetComponent<LaserControl>();
        if (asLaser != null)
        {
            Destroy(this.gameObject);  // Destroy the asteroid when hit by the laser
            Debug.Log($"{this}.onTriggerEnter2D({collision})");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LaserControl laser = collision.gameObject.GetComponent<LaserControl>();
        if (laser != null)
        {
            Destroy(this.gameObject);  // Destroy the asteroid
            Destroy(laser.gameObject); // Optionally, destroy the laser as well
            Debug.Log("Asteroid hit by laser!");
        }
    }


    private void SpawnAsteroid()
    {
       float rotationSpeed = Random.Range(MinRotation, MaxRotation);
        Vector2 speed = new Vector2(Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y));
        AsteroidController ac = AsteroidController.Spawn(Template, rotationSpeed, speed, GameController);
        Vector2 spawnPoint = new(Random.Range(MinSpawnVector.x, MaxSpawnVector.x), MinSpawnVector.y);
        ac.transform.position = spawnPoint;
        LastSpawn = Time.time;
    }

    private bool ShouldSpawn()
    {
        return Time.time > (LastSpawn + SpawnRate);
    }
}
