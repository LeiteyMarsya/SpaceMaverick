using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DestructableController))]
public class EnemyShipController : MonoBehaviour
{
    [field: SerializeField]
    public Transform FireSpawnPoint { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; } = 3;

    [field: SerializeField]
    public int direction = 1;

    [field: SerializeField]
    public GameObject LaserTemplate { get; private set; }

    [field: SerializeField]
    public float Firerate { get; private set; } = 2;

    [field: SerializeField]
    private bool isMovingHorizontal = true;

    private float LastFire;

    void Start()
    {
        LastFire = Time.time + 1f;
    }

    public static EnemyShipController Spawn(EnemyShipController enemy, GameController gameController)
    {
        EnemyShipController newEnemy = Instantiate(enemy);
        newEnemy.GetComponent<DestructableController>().GameController = gameController;
        newEnemy.transform.position = enemy.transform.position;
        return newEnemy;
    }

    void Update()
    {
        MoveShip();
        HandleFire();
    }

    private void HandleFire()
    {
        if (Time.time > (LastFire + Firerate))
        {
            GameObject laser = Instantiate(LaserTemplate);
            laser.transform.position = FireSpawnPoint.position;
            LastFire = Time.time;
        }
    }

    private void MoveShip()
    {
        float newX = transform.position.x + (Speed * Time.deltaTime * direction);
        float newY = transform.position.y;

        if (newX > 3 || newX < -3)
        {
            direction *= -1;
            isMovingHorizontal = false;
        }

        if (!isMovingHorizontal)
        {
            newY = transform.position.y + (Speed * Time.deltaTime * direction);

            if (newY > 4.5f || newY < -4.5f)
            {
                direction *= -1;
                isMovingHorizontal = true;
            }
        }

        transform.position = new Vector2(newX, newY);
    }
}
