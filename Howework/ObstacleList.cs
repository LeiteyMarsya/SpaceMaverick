using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    using System.Collections.Generic;

    public class ObstacleList
    {
        public List<Obstacle> Obstacles { get; set; }//collection of obstacles will be stored

        public ObstacleList()
        {
            Obstacles = new List<Obstacle>(); //initializes the Obstacles property as an empty lis
        }

        // Add obstacle to the list
        public void AddObstacle(Obstacle obstacle) //call this to add an obstacle object in the list
        {
            Obstacles.Add(obstacle);
        }

        // Move all obstacles
        public void MoveObstacles() // to move all the obstacles in the list
        {
            foreach (var obstacle in Obstacles)//allows to apply the same action to every obstacle in the list.
            {
                obstacle.Move();//makes each obstacle "move" according to whatever behavior
            }
        }
    }

}