using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Marsya, Akmal, Tharsina, Fatin and Zalikha

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    public AsteroidSpawner AsteroidSpawner { get; private set; }

    [field: SerializeField]
    public List<EnemyShipController> EnemyTypes { get; private set; }

    [field: SerializeField]
    public Transform EnemySpawnPoint { get; private set; }

    [field: SerializeField]
    public EnemySpawner EnemySpawner { get; private set; }

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

    [field: SerializeField]
    public static GameController Instance;

    [field: SerializeField] private int _score;
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChange.Invoke(_score);
            CheckHighScore();  // Check high score when score changes
        }
    }

    public UnityEngine.Events.UnityEvent<int> OnScoreChange;
    public UnityEngine.Events.UnityEvent<int> OnLivesChange;

    [field: SerializeField] private int _livesRemaining = 3;
    public int LivesRemaining
    {
        get => _livesRemaining;
        private set
        {
            _livesRemaining = value;
            OnLivesChange.Invoke(_livesRemaining);
        }
    }

    [field: SerializeField]
    public GameObject GameOverScreen { get; private set; }

    [field: SerializeField]
    private int highScore;

    [field: SerializeField]
    private TextMeshProUGUI highScoreText;

    void Start() 
    {
        LivesRemaining = 3; //sets initial lives to 3
        SpawnPlayer(); //spawn the player when game start

        // Load the high score at the start of the game
        LoadHighScore();
        UpdateHighScoreText();

        EnemySpawner.EnqueueEnemies(enemies); //spawn the enemy with a list of enemy
    }

    void Update() 
    {
        if (SpawnAt > 0 && Time.time > SpawnAt) 
        {
            SpawnPlayer(); //player is spawn again
        }
    }

    private void SpawnPlayer()
    {
        if (LivesRemaining > 0) //spawn player if there are still have lives remaining
        {
            PlayerController pc = Instantiate(PlayerTemplate);
            pc.GameController = this;
            pc.transform.position = PlayerSpawnPoint.position; //spawn player at the player spawn point
            SpawnAt = -1;
        }
    }

    public void DestroyPlayer(PlayerController toDestroy)
    {
        Destroy(toDestroy.gameObject); 
        SpawnAt = Time.time + SpawnDelay;
        LivesRemaining--;
        if (LivesRemaining <= 0)
        {
            GameOverScreen.SetActive(true); //if there is no more lives remaining it will display game over
        }
    }

    public void Restart()
    {
        GameOverScreen.SetActive(false); //Hide game over screen
        Score = 0; //set back score to 0
        LivesRemaining = 3; //set back live remaing to 3

        //destroy all existing enemy laser and asteroid
        foreach (var enemy in FindObjectsOfType<EnemyShipController>())
        {
            Destroy(enemy.gameObject);
        }

        foreach (var laser in FindObjectsOfType<LaserController>())
        {
            Destroy(laser.gameObject);
        }

        foreach (var asteroid in FindObjectsOfType<AsteroidController>())
        {
            Destroy(asteroid.gameObject);
        }

        // Reset enemy and asteroid spawners
        EnemySpawner.ResetSpawner();
        AsteroidSpawner.ResetSpawner();

        // Re-enqueue all enemies
        EnemySpawner.EnqueueEnemies(enemies);

        // Spawn the player at the starting position
        SpawnPlayer();
    }



    private void Awake()
    //Ensures only one instance of GameController exists throughout the game.
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Prevents duplicates using DontDestroyOnLoad
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        Score += points; //Adds points to the score and updates the high score
    }

    public int GetScore()
    {
        return Score;
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        //Retrieves the saved high score from PlayerPrefs
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save(); //save the current high score
    }

    private void CheckHighScore()
    {
        if (Score > highScore) //compare current score to the high score
        {
            highScore = Score;
            SaveHighScore();
            UpdateHighScoreText(); //updated high score if current score > high score
        }
    }

    private void UpdateHighScoreText() 
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
            //Updates the UI text displaying the high score
        }
    }
}