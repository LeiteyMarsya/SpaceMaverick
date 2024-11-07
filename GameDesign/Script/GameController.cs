using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        LivesRemaining = 3;
        SpawnPlayer();

        // Load the high score at the start of the game
        LoadHighScore();
        UpdateHighScoreText();

        EnemySpawner.EnqueueEnemies(enemies);
    }

    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (LivesRemaining > 0)
        {
            PlayerController pc = Instantiate(PlayerTemplate);
            pc.GameController = this;
            pc.transform.position = PlayerSpawnPoint.position;
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
            GameOverScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        GameOverScreen.SetActive(false);
        Score = 0;
        LivesRemaining = 3;

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
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public int GetScore()
    {
        return Score;
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void CheckHighScore()
    {
        if (Score > highScore)
        {
            highScore = Score;
            SaveHighScore();
            UpdateHighScoreText();
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }
}
