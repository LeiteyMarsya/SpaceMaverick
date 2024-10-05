using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    using System.Collections.Generic;

    public class EnemySwarm //Enemy swarm is things that will manages the enemy object
    {
        public List<Enemy> Enemies { get; set; } //collection that will hold multiple object of the same type of enemy
         //Swarm enemy will be stored here
        public EnemySwarm()
        {
            Enemies = new List<Enemy>(); //When EnemySwarm is created, it will store in enemies list
        }

        // Add an enemy to the swarm
        public void AddEnemy(Enemy enemy) //To add enemy object into the list
        {
            Enemies.Add(enemy);
        }

        // Move all enemies in the swarm
        public void MoveSwarm(float playerX, float playerY) // Accept player's coordinates
        {
            foreach (var enemy in Enemies) // Goes through each enemy in the Enemies list one by one
            {
                enemy.Move(playerX, playerY); // Pass player's coordinates
            }
        }
    }


}
