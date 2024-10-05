using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMaverick
{
    public class ScoreSystem
    {
        public int CurrentScore { get; private set; }
        /*holds the player's current score in the game,
         * and it can be read by other parts of the program but only changed within this class
         */
        public int HighScore { get; private set; }
        /*keeps track of the highest score the player has ever achieved. 
         * It will only be updated if the current score exceeds the high score
         */

        public ScoreSystem() //initialise CurrentScore and HighScore as 0
        {
            CurrentScore = 0;
            HighScore = 0;
        }

        public void AddScore(int points)
            /*keeps track of the highest score the player has ever achieved. 
             * It will only be updated if the current score exceeds the high score
             */
        {
            CurrentScore += points;
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
            }
        }

        public void ResetScore() //reset the player's score, start a new game or level.
        {
            CurrentScore = 0;
        }

        public void DisplayScore()//showing the player's score on the screen
        {
            Console.WriteLine($"Current Score: {CurrentScore}");
            Console.WriteLine($"High Score: {HighScore}");
        }
    }

}
