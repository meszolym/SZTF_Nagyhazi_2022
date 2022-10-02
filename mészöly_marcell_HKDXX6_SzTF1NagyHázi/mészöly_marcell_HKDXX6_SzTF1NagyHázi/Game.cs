using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Game
    {
        internal Field[] fields;
        internal Player[] players;
        private int turnCounter;
        private int remainingPlayers;
        internal Game()
        {
            this.fields = null;
            this.players = new Player[4];
            this.turnCounter = 0;
            this.remainingPlayers = players.Length;
        }

        /// <summary>
        /// A játék futtatásáért felel.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="players"></param>
        /// <param name="fields"></param>
        internal void Run(ref UIWriter writer)
        {
            while (remainingPlayers > 2)
            {
                while (!players[turnCounter].inGame)
                {
                    turnCounter++;
                    if (turnCounter > players.Length-1)
                    {
                        turnCounter = 0;
                    }
                }
                GameRound(ref writer);
                if (turnCounter > players.Length)
                {
                    turnCounter = 0;
                }
            }
        }

        private void GameRound(ref UIWriter writer)
        {


            writer.PlacementBeforeRoll();

            players[turnCounter].StepForward();
            
            writer.PlacementAfterRoll();


            turnCounter++;
        }

        internal string GetWinner()
        {
            int MaxID = 0;
            for (int i = 0; i<players.Length; i++)
            {
                if (players[i].Money > players[MaxID].Money)
                {
                    MaxID = i;
                }
            }
            return $"{MaxID + 1}. játékos";
        }

    }
}
