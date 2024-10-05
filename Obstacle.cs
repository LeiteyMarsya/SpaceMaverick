using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    public class Obstacle
    {
        public float X { get; set; }  // X coordinate
        public float Y { get; set; }  // Y coordinate
        public int Damage { get; set; } //Damage value determines how much harm this obstacle can cause
        public int Size { get; set; } //determines the physical size of the obstacle
        private Random random; // Random object for generating random numbers

        public Obstacle(float x, float y, int damage, int size)
        {
            X = x;
            Y = y;
            Damage = damage;
            Size = size;
        }

        public void Move()
        {
            // Move the obstacle downwards
            Y -= 1;

            //creating unpredictable challenges
            // Add random horizontal movement
            float horizontalMovement = (float)(random.NextDouble() * 2 - 1); // Generates a random float between -1 and 1
            X += horizontalMovement;//returns a random double-precision floating-point number between 0.0 and 1.0.
        }
    }

}