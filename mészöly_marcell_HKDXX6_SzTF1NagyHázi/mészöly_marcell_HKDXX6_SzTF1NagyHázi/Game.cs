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
        internal void Run()
        {

            while (remainingPlayers > 2)
            {
                if (turnCounter > players.Length - 1)
                {
                    turnCounter = 0;
                }
                while (!players[turnCounter].inGame)
                {
                    turnCounter++;
                    if (turnCounter > players.Length-1)
                    {
                        turnCounter = 0;
                    }
                }
                GameRound();
                if (turnCounter > players.Length)
                {
                    turnCounter = 0;
                }
            }

            UIWriter.AnnounceWinner(GetWinner());
        }
        /// <summary>
        /// A játék egy körét futtatja le.
        /// </summary>
        private void GameRound()
        {
            PostData();
            Field departureField = players[turnCounter].GetPlacementField(ref fields);

            if (players[turnCounter].GetPlacementField(ref fields).ID != 0)
            {
                UIWriter.PlacementBeforeRoll(departureField.GetNameString(),departureField.GetOwnerString(), departureField.GetPriceString());
            }
            else
            {
                UIWriter.PlacementBeforeRoll(departureField.GetNameString());
            }
            Console.ReadKey();

            int Rolled = DiceRoll();

            Field steppedOn = players[turnCounter].StepForward(Rolled, ref fields);

            PostData();

            if (steppedOn.ID < departureField.ID)
            {
                UIWriter.WriteCrossedStart(MinFieldPriceString());
                players[turnCounter].Money += MinFieldPrice();

            }

            Console.ReadKey();
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
        private int MinFieldPrice()
        {
            Field minField = fields[1]; //startmezőt nem vesszük figyelembe
            for (int i = 1; i < fields.Length; i++) //startmezőt nem vesszük figyelembe
            {
                if (fields[i] < minField)
                {
                    minField = fields[i];
                }
            }
            return minField.Price;
        }
        /// <summary>
        /// Átadja a játék adatait a UI író felé kiírásra.
        /// </summary>
        private void PostData()
        {
            Console.Clear();
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].OwnerID != -1)
                {
                    UIWriter.WriteFieldWithOwner(fields[i].GetTop(), fields[i].GetTag(), players[fields[i].OwnerID].bgColor, players[fields[i].OwnerID].fgColor, fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                else
                {
                    UIWriter.WriteFieldNoOwner(fields[i].GetTop(), fields[i].GetTag(), fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                Player[] OnField = fields[i].GetPlayersOnField(ref players);
                for (int j = 0; j < OnField.Length; j++)
                {

                    UIWriter.WritePlayerOnField((OnField[j].ID + 1).ToString(), fields[i].BoardPlacementLeft +2 + j, fields[i].BoardPlacementTop + 1, OnField[j].bgColor, OnField[j].fgColor);

                }
            }
            Console.SetCursorPosition(0,(fields.Length/4+1)*3);
            UIWriter.WriteDivider();
            for (int i = 0; i<players.Length; i++)
            {
                UIWriter.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $");
            }
            UIWriter.WriteDivider();

        }
    }
}
