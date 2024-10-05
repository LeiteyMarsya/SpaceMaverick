using SpaceMaverick;
using System.Collections.Generic;
using System;

public class GameControl // Controls the game flow
{
    //private set; means that only this class can modify this lis
    public List<Obstacle> Obstacles { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public List<PowerUp> PowerUps { get; private set; }  // List of Power-ups
    public SpaceCraft Player { get; private set; }
    public ScoreSystem ScoreSystem { get; private set; }

    public GameControl()
    {
        Obstacles = new List<Obstacle>();
        Enemies = new List<Enemy>();
        PowerUps = new List<PowerUp>();  // Initialize the list of power-ups
        Player = new SpaceCraft(100, 5, 0, 0);  // Using x, y coordinates
        ScoreSystem = new ScoreSystem();
    }

    // Spawn Obstacle with manual x and y positions
    public void SpawnObstacle(float x, float y, int damage, int size)
        //creates a new Obstacle at specified coordinates with given damage and size
    {
        Obstacles.Add(new Obstacle(x, y, damage, size));//adds the newly created obstacle to the Obstacles list
    }

    // Spawn Enemy with manual x and y positions
    public void SpawnEnemy(float x, float y, int health, int damage)
    {
        Enemies.Add(new Enemy(x, y, health, damage));//creates and adds a new Enemy
    }

    // Spawn Power-up
    public void SpawnPowerUp(float x, float y, string type)
    {
        PowerUps.Add(new PowerUp(x, y, type));
        /*creates a new PowerUp at the specified coordinates with a type like speed or shield
         * and adds it to the PowerUps list
         */
    }

    public void UpdateGameState()
    {
        // Increase difficulty over time
        if (ScoreSystem.CurrentScore % 1000 == 0) // Every 1000 points
        {
            // Spawn more enemies, make obstacles faster, etc.
            Console.WriteLine("Increasing difficulty!");
        }

        // Update obstacles
        foreach (var obstacle in Obstacles)
        {
            obstacle.Move();//loop goes through each obstacle in the Obstacles list and calls their Move()
        }

        // Update enemies
        // Update enemies
        foreach (var enemy in Enemies) // Updates each enemy's position and allows it to attack the player
        {
            enemy.Move(Player.X, Player.Y); // Pass player's coordinates
            enemy.Attack(Player);
        }


        // Update power-ups and check if player collects them
        foreach (var powerUp in PowerUps)
        {
            if (CheckCollision(Player, powerUp))
            /* checks if the player collects any power-ups. It calls CheckCollision,
             * and if there's a collision with the power item or basically if player take the powerup items, 
             * it activates the power-up and prints a message
             */
            {
                powerUp.Activate(Player);
                Console.WriteLine($"Player collected {powerUp.Type} Power-up!");
            }
        }

        // Check if player is alive
        if (Player.Health <= 0)
        {
            Console.WriteLine("Game Over!");
            ResetGame();
            /* if the player's health is zero or less. If it is, it prints "Game Over!" 
             * and calls ResetGame() to restart the game
             */
        }
    }

    // Check if player collides with power-up
    private bool CheckCollision(SpaceCraft player, PowerUp powerUp)
    {
        /* return statement compares the player's and the power-up's coordinates. 
          If they match, it returns true, indicating a collision */
        return player.X == powerUp.X && player.Y == powerUp.Y;
    }

    // Reset the game when player loses
    public void ResetGame()
    {
        Obstacles.Clear();
        Enemies.Clear();//remove all items from their respective lists
        PowerUps.Clear();  // Clear power-ups as well
        Player.Health = 100;//resets the player's health back to 100
        ScoreSystem.ResetScore();//resets the player's score
    }
}