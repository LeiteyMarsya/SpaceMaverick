using System;

namespace SpaceMaverick
{
    public class Enemy // Declare the Enemy class as public so it can be used everywhere
    {
        public float X { get; set; }
        public float Y { get; set; } // X and Y set as public so they can be accessed outside this class too
        public int Health { get; set; }
        public int Damage { get; set; }
        public float AttackRange { get; set; } // Distance within which the enemy can attack
        private Random random;

        public Enemy(float x, float y, int health, int damage)
        {
            X = x;
            Y = y;
            Health = health;
            Damage = damage;
            AttackRange = 5.0f; // Example attack range
            random = new Random();
        }

        public void Move(float playerX, float playerY) // Define how the enemy moves towards the player
        {
            // Simple AI to move towards the player
            if (Math.Abs(X - playerX) > AttackRange)
                /*absolute is to ensuring we get positive value regardless of the direction.
                -checks if the horizontal distance between the enemy and the player is greater than the AttackRange.
                -If the enemy is too close to attack (within the AttackRange), it won't move towards the player*/
            {
                // Randomly move left or right towards the player
                if (X < playerX) X += 0.5f; 
                //If the enemy is to the left of the player, it moves right by increasing its X coordinate
                else if (X > playerX) X -= 0.5f;
                //if the enemy is to the right of the player, it moves left by decreasing its X coordinate

                // Random vertical movement for added challenge
                Y += (float)(random.NextDouble() * 2 - 1); // Randomly move up or down
            }
        }

        public void Attack(SpaceCraft target) // Manage how the Enemy deals damage to the target
        {
            // calculates the distance between the enemy and the target spaceship
            if (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)) <= AttackRange)
            // Check if the enemy is within attack range
            //If true, the enemy can attack the player
            {
                target.Health -= Damage; // Deal damage to the target
                Console.WriteLine($"Enemy attacks! {target.GetType().Name} takes {Damage} damage.");
            }
        }
    }
}
