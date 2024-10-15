using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [field: SerializeField]
    public Sprite LeanLeft {  get; private set; }

    [field: SerializeField]
    public Sprite LeanRight { get; private set; }

    [field: SerializeField]
    public float DamageBoost { get; private set; } = 3;

    [field: SerializeField]
    public int Speed { get; private set; } = 5;

    [field: SerializeField]
    public GameObject Laser { get; private set; }

    [field: SerializeField]
    public Transform FrontLaserSpawn { get; private set; }

    [field: SerializeField]
    public Vector2 velocity { get; private set; } = new Vector2(0, 0);

    [field: SerializeField]
    public Vector2 Min { get; private set; }

    [field: SerializeField]
    public Vector2 Max { get; private set; }

    // Reference to the GameController instance
    public GameController GameController { get; set; }

    public static PlayerController Spawn(PlayerController template, GameController controller)
    {
        PlayerController player = Instantiate(template);
        player.GameController = controller;
        return player;
    }

    void Start()
    {
        // You can initialize any required variables here
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFire();
        HandleDamageBoost();
        MoveShip();
    }

    private void HandleDamageBoost()
    {
        DamageBoost -= Time.deltaTime;
        DamageBoost = Mathf.Max(0, DamageBoost);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AsteroidController asAsteroid = collision.gameObject.GetComponent<AsteroidController>();
        if (asAsteroid != null && DamageBoost <= 0)
        {
            GameController.DestroyPlayer(this); // This will work if GameController is properly assigned
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AsteroidController asAsteroid = collision.GetComponent<AsteroidController>();
        if (asAsteroid != null)
        {
            Debug.Log("Collision detected, destroying player!");
            GameController.DestroyPlayer(this); // Use the GameController instance
        }
    }

    private void HandleFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laser = Instantiate(Laser);
            laser.transform.position = FrontLaserSpawn.position;
            Debug.Log("Laser fired!");
        }
    }

    private void HandleMovement()
    {
        velocity = HandleHorizontal(Input.GetAxis("Horizontal"));
        velocity += HandleVertical(Input.GetAxis("Vertical"));
    }

    private Vector2 HandleVertical(float v)
    {
        return new Vector2(0, Mathf.Clamp(v, -1, 1));
    }

    private Vector2 HandleHorizontal(float h)
    {
        return new Vector2(Mathf.Clamp(h, -1, 1), 0);
    }

    private void MoveShip()
    {
        float newX = transform.position.x + (velocity.x * Speed * Time.deltaTime);
        float newY = transform.position.y + (velocity.y * Speed * Time.deltaTime);
        transform.position = new Vector2(newX, newY);

    }
}
