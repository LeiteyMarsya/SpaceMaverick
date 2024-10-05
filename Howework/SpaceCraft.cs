using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    public class SpaceCraft
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Health { get; set; } //tracks how much damage the spacecraft can take before destroyed
        public int Speed { get; set; } //how far the spacecraft moves in one action or in screen
        public bool ShieldActive { get; private set; }  // To track if the shield is active

        public SpaceCraft(int health, int speed, float x, float y)
        {
            Health = health;
            Speed = speed;
            X = x;
            Y = y;
            ShieldActive = false;  // Initially, the shield is inactive
        }

        //activate the shield
        public void ActivateShield()
        {
            ShieldActive = true;
            Console.WriteLine("Shield activated!");//Activates the shield and prints "Shield activated!
        }

        //increase firepower
        public void IncreaseFirepower()//increase the damage output of the spacecraft’s attacks
        {
            Console.WriteLine("Increased firepower!");
        }

        //  allow the spacecraft to move around in the game world based on its speed
        public void MoveLeft() => X -= Speed;//Moves the ship left by subtracting the speed from the X coordinate
        public void MoveRight() => X += Speed;//Moves the ship right by adding the speed to the X coordinate
        public void MoveUp() => Y += Speed;//Moves the ship upward by adding the speed to the coordinates Y
        public void MoveDown() => Y -= Speed;//Moves the ship downward by substracting the speed to the coordinate Y
    }


}
