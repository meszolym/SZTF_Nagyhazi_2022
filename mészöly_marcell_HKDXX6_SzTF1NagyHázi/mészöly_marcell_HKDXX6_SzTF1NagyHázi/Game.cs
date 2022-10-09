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
            Console.Clear();
            UIWriter.AnnounceWinner(GetWinner());
            Console.ReadKey();
        }
        /// <summary>
        /// A játék egy körét futtatja le.
        /// </summary>
        private void GameRound()
        {
            PostData();
            Field departureField = players[turnCounter].GetPlacementField(ref fields);

            if (departureField.ID != 0) //nem startmező
            {
                if (departureField.OwnerID != -1)
                {
                    Player owner = players[departureField.OwnerID];
                    UIWriter.PlacementBeforeRoll(departureField.GetNameString(), owner.GetName(), owner.bgColor, owner.fgColor, departureField.GetPriceString());
                }
                else
                {
                    UIWriter.PlacementBeforeRollNoOwner(departureField.GetNameString(), departureField.GetPriceString());
                }
            }
            else //startmező
            {
                UIWriter.PlacementBeforeRoll(departureField.GetNameString());
            }
            Console.ReadKey();

            int Rolled = DiceRoll();

            Field steppedOnField = players[turnCounter].StepForward(Rolled, ref fields);

            PostData();

            UIWriter.WriteRolledValue(departureField.GetNameString(), Rolled);

            if (steppedOnField.ID < departureField.ID)
            {
                UIWriter.WriteCrossedStart(MinFieldPriceString());
                players[turnCounter].Money += MinFieldPrice();
                PostPlayerStatuses();

            }

            Writer.WriteDivider();

            if (players[turnCounter].GetPlacementField(ref fields).ID != 0) //nem startmező
            {
                if (players[turnCounter].GetPlacementField(ref fields).OwnerID != -1) //van ownere
                {
                    Player owner = players[steppedOnField.OwnerID];
                    UIWriter.WritePlacementAfterRoll(steppedOnField.GetNameString(), owner.GetName(), owner.bgColor, owner.fgColor, steppedOnField.GetPriceString());
                    Payrent(steppedOnField);
                }
                else //nincs ownere
                {
                    UIWriter.WritePlacementAfterRollNoOwner(steppedOnField.GetNameString(), steppedOnField.GetPriceString());
                    BuyField(steppedOnField);
                }
            }
            else //startmező
            {
                UIWriter.WritePlacementAfterRoll(players[turnCounter].GetPlacementField(ref fields).GetNameString());
            }
            Writer.WriteDivider();
            UIWriter.WriteEndOfRound();
            Console.ReadKey();
            turnCounter++;
        }
        /// <summary>
        /// A bérleti díj kifizettetéséért felelős.
        /// </summary>
        /// <param name="placement">A mező, ahol a játékos áll</param>
        private void Payrent(Field placement)
        {
            if (players[turnCounter].ID != placement.OwnerID) //önmagának nem fizet bérletet
            {
                if (players[placement.OwnerID].inGame)
                {
                    UIWriter.RentPayment();
                    if (players[turnCounter].Money > placement.Price) //kifizeti
                    {
                        players[placement.OwnerID].Money += placement.Price;
                        players[turnCounter].Money -= placement.Price;
                        PostPlayerStatuses();
                    }
                    else //csődbemegy
                    {
                        players[placement.OwnerID].Money += players[turnCounter].Money;
                        players[turnCounter].Money = 0;
                        players[turnCounter].inGame = false;
                        remainingPlayers--;
                        PostPlayerStatuses();
                        UIWriter.WentBankrupt();
                    }
                }
                else
                {
                    UIWriter.WriteNoRent();
                }
            }     
        }
        /// <summary>
        /// A mezők megvásárlásáért felel
        /// </summary>
        /// <param name="placement">A mező, ahol a játékos áll</param>
        private void BuyField(Field placement)
        {
            if (players[turnCounter].Money > placement.Price) //megveheti
            {
                UIWriter.AskBuyQuestion();
                string buyYesNo = Console.ReadLine();
                if (buyYesNo == "I" || buyYesNo == "i")
                {
                    players[turnCounter].Money -= placement.Price;
                    placement.OwnerID = turnCounter;
                    UIWriter.WriteBoughtField();
                    PostData(reDraw: true);
                }
                else
                {
                    UIWriter.WriteNotBought();
                }
            }
            else
            {
                UIWriter.WriteCannotBuy();
            }
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
        /// Megadja a legkisebb értékű mező értékét.
        /// </summary>
        /// <returns>int - a legkisebb értékű mező értéke</returns>
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
        private void PostData(bool reDraw = false)
        {
            int left = 0;
            int top = 0;
            if (!reDraw)
            {
                Console.Clear();
            }
            else
            {
                left = Console.CursorLeft;
                top = Console.CursorTop;
            }
            
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].OwnerID != -1)
                {
                    UIWriter.WriteFieldWithOwner(fields[i].GetTop(), fields[i].GetFlag(), players[fields[i].OwnerID].bgColor, players[fields[i].OwnerID].fgColor, fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                else
                {
                    UIWriter.WriteFieldNoOwner(fields[i].GetTop(), fields[i].GetFlag(), fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                Player[] OnField = fields[i].GetPlayersOnField(ref players);
                for (int j = 0; j < OnField.Length; j++)
                {
                    if (OnField[j].inGame)
                    {
                        UIWriter.WritePlayerOnField((OnField[j].ID + 1).ToString(), fields[i].BoardPlacementLeft + 2 + j, fields[i].BoardPlacementTop + 1, OnField[j].bgColor, OnField[j].fgColor);
                    }
                }
            }
            PostPlayerStatuses();
            if (!reDraw)
            {
                Console.SetCursorPosition(0, (fields.Length / 4 + 1) * Field.Height + players.Length + 2);
                UIWriter.WritePlayerRound($"{turnCounter + 1}. játékos", players[turnCounter].bgColor, players[turnCounter].fgColor);
            }
            else
            {
                Console.SetCursorPosition(left, top);
            }
            
            

        }
        /// <summary>
        /// Átadja a játékosok státuszait a UI író felé kiírásra.
        /// </summary>
        private void PostPlayerStatuses()
        {
            int left = Console.CursorLeft;
            int top = Console.CursorTop;

            Console.SetCursorPosition(0, (fields.Length / 4 + 1) * Field.Height);
            Writer.WriteDivider();
            for (int i = 0; i < players.Length; i++)
            {
                Console.WriteLine(new string(' ', Console.BufferWidth)); //sor tisztítása
                Console.SetCursorPosition(0, Console.CursorTop-1); 
                if (players[i].inGame)
                {
                    UIWriter.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $", players[i].bgColor, players[i].fgColor);
                }
                else
                {
                    UIWriter.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $ (Kiesett)", players[i].bgColor, players[i].fgColor);
                }
            }
            Writer.WriteDivider();
            Console.SetCursorPosition(left, top);
        }
    }
}
