using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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
        /// <param name="writer">A UI írására használt objektum</param>
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

            writer.AnnounceWinner(GetWinner());
        }
        /// <summary>
        /// A játék egy körét futtatja le.
        /// </summary>
        /// <param name="writer">A UI írására használt objektum</param>
        private void GameRound(ref UIWriter writer)
        {
            PostData(ref writer);
            Field departureField = players[turnCounter].GetPlacementField(ref fields);

            if (players[turnCounter].GetPlacementField(ref fields).ID != 0)
            {
                writer.PlacementBeforeRoll(departureField.GetNameString(),departureField.GetOwnerString(), departureField.GetPriceString());
            }
            else
            {
                writer.PlacementBeforeRoll(departureField.GetNameString());
            }

            int Rolled = DiceRoll();

            Field steppedOn = players[turnCounter].StepForward(Rolled, ref fields);
            if (steppedOn.ID < departureField.ID)
            {
                writer.WriteCrossedStart(MinFieldPriceString());
            }


            turnCounter++;
        }
        /// <summary>
        /// Elvégez egy kockadobást.
        /// </summary>
        /// <returns>int - a dobott értéket (random érték n; n>=1; n<=6;</returns>
        private static int DiceRoll()
        {
            Random r = new Random();
            return r.Next(1, 7);
        }
        /// <summary>
        /// Megszerzi a játékban győztes játékos nevét
        /// </summary>
        /// <returns>string - a játékos neve</returns>
        private string GetWinner()
        {
            int MaxID = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Money > players[MaxID].Money)
                {
                    MaxID = i;
                }
            }
            return $"{MaxID + 1}. játékos";            
        }
        /// <summary>
        /// Megadja a legkisebb értékű mező értékét.
        /// </summary>
        /// <returns>string - a legkisebb értékű mező értéke</returns>
        private string MinFieldPriceString()
        {
            Field minField = fields[1]; //startmezőt nem vesszük figyelembe
            for (int i = 1; i<fields.Length; i++) //startmezőt nem vesszük figyelembe
            {
                if (fields[i] < minField)
                {
                    minField = fields[i];
                }
            }
            return minField.GetPriceString();
        }
        /// <summary>
        /// Átadja a játék adatait a UI író felé kiírásra.
        /// </summary>
        /// <param name="writer">A UI írására használt objektum</param>
        private void PostData(ref UIWriter writer)
        {
            Console.Clear();
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].OwnerID != -1)
                {
                    writer.WriteFieldWithOwner(fields[i].GetTop(), fields[i].GetTag(), players[fields[i].OwnerID].bgColor, players[fields[i].OwnerID].fgColor, fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                else
                {
                    writer.WriteFieldNoOwner(fields[i].GetTop(), fields[i].GetTag(), fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                Player[] OnField = fields[i].GetPlayersOnField(ref players);
                for (int j = 0; j < OnField.Length; j++)
                {

                    writer.WritePlayerOnField((OnField[j].ID + 1).ToString(), fields[i].BoardPlacementLeft +2 + j, fields[i].BoardPlacementTop + 1, OnField[j].bgColor, OnField[j].fgColor);

                }
            }
            Console.SetCursorPosition(0, fields.Length / 4 * 3);
            writer.WriteDivider();
            for (int i = 0; i<players.Length; i++)
            {
                writer.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $");
            }
            writer.WriteDivider();

        }
    }
}
