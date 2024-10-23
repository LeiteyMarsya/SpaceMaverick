using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    [field: SerializeField]
    public List<Transform> WayPoints { get; private set; } = new();

    [field: SerializeField]
    public Transform FireSpawnPoint { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; } = 3;

    [field:SerializeField]
    public int direction = 1;

    [field: SerializeField]
    public int NextWayPoint { get; private set; } = 0;

    [field: SerializeField]
    public GameObject LaserTemplate { get; private set; }

    [field: SerializeField]
    public float Firerate { get; private set; } = 2;

    [field: SerializeField]
    public float LastFire { get; private set; } = 0;

    [field:SerializeField]
    private bool isMovingHorizontal = true; // Flag to indicate horizontal or vertical movement

    void Start()
    {
        LastFire = Time.time + 1f;  // Delay firing by 1 second after spawning
    }


    public static EnemyShipController Spawn(EnemyShipController enemy)
    {
        EnemyShipController newEnemy = Instantiate(enemy);
        newEnemy.WayPoints = enemy.WayPoints;
        newEnemy.transform.position = enemy.transform.position;
        return newEnemy;
    }

    public void SetWayPoints(List<Transform> waypoints)
    {
        this.WayPoints = waypoints;
    }


    public Vector2 v;
    public Vector2 Velocity
    {
        get
        {
            Vector2 target = WayPoints[NextWayPoint].position;
            Vector2 v =  target - (Vector2)this.transform.position;
            v.Normalize();
            return v;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserControl asLaser = other.GetComponent<LaserControl>();
        if (asLaser != null)
        {
            OnLaserHit(asLaser);
        }
    }

    private void OnLaserHit(LaserControl laser)
    {
        Destroy(laser.gameObject);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip(); 
        CheckWayPoint();
        HandleFire();
    }

    private void HandleFire()
    {
        if (Time.time > (LastFire + Firerate))
        {
            GameObject laser = Instantiate(LaserTemplate); // Create the laser
            laser.transform.position = FireSpawnPoint.position; // Set the laser's position
            LastFire = Time.time; // Update the time of the last fire
        }
    }




    private void MoveShip()
    {
        // Calculate new position based on movement direction
        float newX = transform.position.x + (Speed * Time.deltaTime * direction);
        float newY = transform.position.y;

        // Check for horizontal edge of screen
        if (newX > 3 || newX < -3)
        {
            // Reverse horizontal direction
            direction *= -1;

            // Switch to vertical movement
            isMovingHorizontal = false;
        }

        // If moving vertically, update Y position
        if (!isMovingHorizontal)
        {
            // Update Y position based on direction
            newY = transform.position.y + (Speed * Time.deltaTime * direction);

            // Check for vertical edge of screen
            if (newY > 4.5f || newY < -4.5f)
            {
                // Reverse vertical direction
                direction *= -1;

                // Switch back to horizontal movement
                isMovingHorizontal = true;
            }
        }

        // Update the ship's position
        transform.position = new Vector2(newX, newY);
    }


    private void CheckWayPoint()
    {
        Vector2 target = WayPoints[NextWayPoint].position;
        float distance = Vector2.Distance(target, this.transform.position);

        if (distance < .1)
        {
            NextWayPoint = ( NextWayPoint + 1) % WayPoints.Count;
        }
    }
}
