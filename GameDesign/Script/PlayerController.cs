using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [field:SerializeField]
    public UnityEvent<PlayerController> OnChange {  get; private set; }

    [field: SerializeField]
    public int LivesRemaining { get; private set; } = 3;

    [field: SerializeField]
    public Sprite LeanLeft { get; private set;}
    [field: SerializeField]
    public Sprite LeanRight { get; private set;}

    [field: SerializeField]
    public Sprite Foward { get; private set; }

    [field: SerializeField]
    public float DamageBoost { get; private set; } = 3;

    public bool HasDamageBoost => DamageBoost > 0;
    public bool IsVisible => !HasDamageBoost || Mathf.Sin(Time.time * 20) > 0;

    [field: SerializeField]
    private int _shieldPower = 0;
    //The ship's shieldPower is a value between 0 and 5
    public int ShieldPower
    {
        get => _shieldPower;

        set
        {
            _shieldPower = Mathf.Clamp(value, 0, 5);
            OnChange.Invoke(this);
        }
    }
 

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
        OnChange.Invoke(this);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFire();
        HandleDamageBoost();
        MoveShip();
        HandleSprite();
    }

    private void HandleDamageBoost()
    {
        this.GetComponent<Renderer>().enabled = IsVisible;
        DamageBoost -= Time.deltaTime;
        DamageBoost = Mathf.Max(0, DamageBoost);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerImpactor asImpactor = collision.GetComponent<PlayerImpactor>();
        if (asImpactor != null && DamageBoost <= 0)
        {
            asImpactor.OnImpact(this);
            if (ShieldPower <= 0)
            {
                GameController.DestroyPlayer(this);
               
            }
            else
            {
                ShieldPower--;
            }
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
    private void HandleSprite()
    {
        if (velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanLeft;
        }
        else if (velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanRight;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Foward;
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
        newX = Mathf.Clamp(newX, Min.x, Max.x);
        newY = Mathf.Clamp(newY, Min.y, Max.y);
        transform.position = new Vector2(newX, newY);

    }
}
