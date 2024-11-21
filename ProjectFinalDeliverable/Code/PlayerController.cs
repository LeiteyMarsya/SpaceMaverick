using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Fatin Naqila and Norleitey Marsya


public class PlayerController : MonoBehaviour
{
    [field:SerializeField]
    public UnityEvent<PlayerController> OnChange {  get; private set; }

    [field: SerializeField]
    public int LivesRemaining { get; private set; } = 3; //tracks how many lives player have

    [field: SerializeField]
    public Sprite LeanLeft { get; private set;}
    [field: SerializeField]
    public Sprite LeanRight { get; private set;}

    [field: SerializeField]
    public Sprite Foward { get; private set; }

    [field: SerializeField]
    public float DamageBoost { get; private set; } = 3; //temporary invicibility where player can't be hit during blinking

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
            //Mathf.Clamp is used to make sure the value stays within specific range
            _shieldPower = Mathf.Clamp(value, 0, 5);
            OnChange.Invoke(this);
        }
    }
 

    [field: SerializeField]
    public int Speed { get; private set; } = 5; //Ship's speed

    [field: SerializeField]
    public GameObject Laser { get; private set; }

    [field: SerializeField]
    public Transform FrontLaserSpawn { get; private set; } //position of the laser should be spawn

    [field: SerializeField]
    //Vector2 is a 2D vector which represent x and y
    public Vector2 velocity { get; private set; } = new Vector2(0, 0);
    // Tracks the ship's movement in the X and Y directions.

    [field: SerializeField]
    public Vector2 Min { get; private set; } //boundary for ship movement

    [field: SerializeField]
    public Vector2 Max { get; private set; } //boundary for the ship movement

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
        //OnChange is a unity event that notify other system when the player's state change
        //example: loosing shield or life
        OnChange.Invoke(this); //Invoke is a unity event that used to trigger events
                                //It will calls all method attached to the OnChange
        //notify listeners(IPlayerControllerObserver) about the player's initial state
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
        //detect collision with object (enemies / asteroids)
        //apply damage if player doesn't have damage boost
        //reduce shild power if shield are active
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
        if (Input.GetButtonDown("Fire1")) //when player click space [check on input manager]
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
        //reads input for x and y axis movement
        //updated velocity to represent the direction and speed
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
        //move ship based on the velocity and speed
        //Time.delta time represents the time in second since the last frame
        float newX = transform.position.x + (velocity.x * Speed * Time.deltaTime);
        float newY = transform.position.y + (velocity.y * Speed * Time.deltaTime);
        //clamp the ship's position within boundaries to make sure it keep on the screen
        newX = Mathf.Clamp(newX, Min.x, Max.x);
        newY = Mathf.Clamp(newY, Min.y, Max.y);
        transform.position = new Vector2(newX, newY); //update the player's position

        /*velocity here is to know the direction of the ship move based on user input.
         * velocity.x > o = player move to the right
         * velocity.x < 0 = player move to the left
         * velocity.y > 0 = player move upward
         * velocity.y < 0 = player move downward
         */

    }
}
