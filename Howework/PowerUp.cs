using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    public class PowerUp
    {
        public float X { get; set; }
        public float Y { get; set; }
        public string Type { get; set; }
        //what kind of power-up this object represents and what effect will it have when activated

        public PowerUp(float x, float y, string type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public void Activate(SpaceCraft ship)
        {
            // Apply power-up effects based on type
            switch (Type) //checks the type of power-up and applies the appropriate effect
            {
                case "Shield": //player's ship gains a shield that probably protects it from damage
                    ship.ActivateShield();
                    break;
                case "SpeedBoost": //player's ship moves faster
                    ship.Speed += 10;  // Increase speed
                    break;
                case "AttackPower": //player's ship deals more damage in combat
                    ship.IncreaseFirepower();
                    break;
            }
        }
    }

}
