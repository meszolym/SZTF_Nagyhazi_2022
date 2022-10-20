using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    public class Game
    {
        private Field[] fields;

        public Field[] Fields
        {
            get { return fields; }
        }

        private Player[] players;
        public Player[] Players
        {
            get { return players; }
        }

        private int turnCounter;
        private int remainingPlayers;
        public Game(Field[] fields, Player[] players)
        {
            this.fields = fields;
            this.players = players;
            this.turnCounter = 0;
            this.remainingPlayers = players.Length;
        }

        /// <summary>
        /// Egy szövegből Game objektumot készít, ha az megfelel.
        /// </summary>
        /// <param name="input">A bemeneti szöveg, ami tartalmazza a Game adatait a ReadMe szerint</param>
        /// <param name="errorDesc">Az esetleges hibákat nyilvántartó string</param>
        /// <returns>null - Ha hibába ütközik az olvasás, Game - ha az olvasás hiba nélkül lefut.</returns>
        public static Game Parse(string input, ref string errorDesc)
        {
            string[] inputDivided = input.Split("\r\n");

            ConsoleColor[,] colorSchemas = new ConsoleColor[4, 2]
            {
                {ConsoleColor.Red,ConsoleColor.White},
                {ConsoleColor.Blue,ConsoleColor.White},
                {ConsoleColor.DarkGreen,ConsoleColor.Yellow},
                {ConsoleColor.White,ConsoleColor.Black}
                //{backcolor, forecolor}
            };

            Player[] players = new Player[4];


            if (!int.TryParse(inputDivided[0], out int startmoney))
            {
                errorDesc = "Nem megfelelő kezdőtőke.";
                return null;
            }
            for (int i = 0; i<4; i++)
            {
                players[i] = new Player(i, startmoney, colorSchemas[i, 0], colorSchemas[i, 1]);
            }

            if (!int.TryParse(inputDivided[1], out int numOfFields) || (numOfFields + 1) % 4 != 0 || numOfFields < 7 || numOfFields > 47)
            {
                errorDesc = "Nem megfelelő mezőszám.";
                return null;
            }
            numOfFields++; // startmezőnek hely
            Field[] fields = new Field[numOfFields];
            fields[0] = new Field(0, -1, -1); //startmező

            string[] prices = inputDivided[2].Split(";");
            for (int i = 1; i < fields.Length; i++)
            {
                if (!int.TryParse(prices[i - 1], out int price) || price <= 0)
                {
                    errorDesc = $"Nem megfelelő ár a(z) {i}. helyen.";
                    return null;
                }

                fields[i] = new Field(i, price, -1);
            }

            Field.SortAndAssignFields(ref fields);

            return new Game(fields,players);
        }

        /// <summary>
        /// A játék futtatásáért felel.
        /// </summary>
        public void Run()
        {

            while (remainingPlayers > 2)
            {
                while (!players[turnCounter].InGame)
                {
                    turnCounter++;
                    if (turnCounter >= players.Length)
                    {
                        turnCounter = 0;
                    }
                }
                GameRound();
                if (turnCounter >= players.Length)
                {
                    turnCounter = 0;
                }
            }

            int winnerID = GetWinnerID();

            for (int i = 0; i<players.Length; i++)
            {
                if (players[i].InGame)
                {
                    if (players[i].ID != winnerID)
                    {
                        players[i].Money = 0; //kiléptetés a játékból (InGame = false redundáns lenne!)
                        players[i].FinishedAt = 2;
                    }
                    else
                    {
                        players[i].Money = 0; //kiléptetés a játékból (InGame = false redundáns lenne!)
                        players[i].FinishedAt = 1;
                    }
                }
            }

            Console.Clear();

            PostRankingData();
            Writer.AnnounceEnd();
            Console.ReadKey();
        }

        /// <summary>
        /// A játék egy körét futtatja le.
        /// </summary>
        private void GameRound()
        {
            PostData();
            Field departureField = fields[players[turnCounter].PlacementID];

            if (departureField.ID != 0) //nem startmező
            {
                if (departureField.OwnerID != -1)
                {
                    if (departureField.OwnerID != players[turnCounter].ID)
                    {
                        Player owner = players[departureField.OwnerID];
                        Writer.PlacementBeforeRoll(departureField.Name, owner.Name, owner.BackgroundColor, owner.ForegroundColor, departureField.PriceString);
                    }
                    else
                    {
                        Player owner = players[departureField.OwnerID];
                        Writer.PlacementBeforeRoll(departureField.Name, "Te", owner.BackgroundColor, owner.ForegroundColor, departureField.PriceString);
                    }
                }
                else
                {
                    Writer.PlacementBeforeRoll(departureField.Name, "Nincs", Writer.BaseBgColor, Writer.BaseFgColor, departureField.PriceString);
                }
            }
            else //startmező
            {
                Writer.PlacementBeforeRoll(departureField.Name);
            }
            Console.ReadKey();

            int Rolled = DiceRoll();

            players[turnCounter].StepForward(Rolled, fields.Length);

            Field steppedOnField = fields[players[turnCounter].PlacementID];

            PostData();

            Writer.WriteRolledValue(departureField.Name, Rolled);

            if (steppedOnField.ID < departureField.ID)
            {
                Writer.WriteCrossedStart(MinFieldPriceString());
                players[turnCounter].Money += MinFieldPrice();
                PostPlayerStatuses();

            }

            Writer.WriteDivider();

            if (steppedOnField.ID != 0) //nem startmező
            {
                if (steppedOnField.OwnerID != -1) //van ownere
                {
                    if (steppedOnField.OwnerID != players[turnCounter].ID)
                    {
                        Player owner = players[steppedOnField.OwnerID];
                        Writer.WritePlacementAfterRoll(steppedOnField.Name, owner.Name, owner.BackgroundColor, owner.ForegroundColor, steppedOnField.PriceString);
                        Payrent(steppedOnField);
                    }
                    else
                    {
                        Player owner = players[steppedOnField.OwnerID];
                        Writer.WritePlacementAfterRoll(steppedOnField.Name, "Te", owner.BackgroundColor, owner.ForegroundColor, steppedOnField.PriceString);
                    }
                   
                }
                else //nincs ownere
                {
                    Writer.WritePlacementAfterRoll(steppedOnField.Name, "Nincs", Writer.BaseBgColor,Writer.BaseFgColor, steppedOnField.PriceString);
                    BuyField(ref steppedOnField);
                }
            }
            else //startmező
            {
                Writer.WritePlacementAfterRoll(fields[players[turnCounter].PlacementID].Name);
            }
            Writer.WriteDivider();
            Writer.WriteEndOfRound();
            Console.ReadKey();
            turnCounter++;
        }
        /// <summary>
        /// A bérleti díj kifizettetéséért felelős.
        /// </summary>
        /// <param name="placement">A mező, ahol a játékos áll</param>
        private void Payrent(Field placement)
        {

            if (players[placement.OwnerID].InGame)
            {
                Writer.RentPayment();
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
                    players[turnCounter].FinishedAt = remainingPlayers;
                    remainingPlayers--;
                    PostPlayerStatuses();
                    Writer.WentBankrupt();
                }
            }
            else
            {
                Writer.WriteNoRent();
            }

        }
        /// <summary>
        /// A mezők megvásárlásáért felel
        /// </summary>
        /// <param name="placement">A mező, ahol a játékos áll</param>
        private void BuyField(ref Field placement)
        {
            if (players[turnCounter].Money > placement.Price) //megveheti
            {
                Writer.AskBuyQuestion();
                string buyYesNo = Console.ReadLine();
                while (buyYesNo != "I" && buyYesNo != "i" && buyYesNo != "N" && buyYesNo != "n")
                {
                    Writer.WriteErrorYesNoAnswer();
                    buyYesNo = Console.ReadLine();
                }

                if (buyYesNo == "I" || buyYesNo == "i")
                {
                    placement.Buy(ref players[turnCounter]);
                    Writer.WriteBoughtField();
                    PostData(reDraw: true);
                }
                else
                {
                    Writer.WriteNotBoughtField();
                }
            }
            else
            {
                Writer.WriteCannotBuy();
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
        /// Megszerzi a játékban győztes játékos ID-ját.
        /// </summary>
        /// <returns>int - a játékos ID-ja</returns>
        private int GetWinnerID()
        {
            Player MaxPlayer = players[0];
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Money > MaxPlayer.Money)
                {
                    MaxPlayer = players[i];
                }
            }
            return MaxPlayer.ID;            
        }
        /// <summary>
        /// Átadja a helyezésadatokat a UI író felé kiírásra
        /// </summary>
        private void PostRankingData()
        {

            int[] PlayersFields = GetPlayersFields();
            int[] Ranking = new int[players.Length];

            for (int i = 0; i < players.Length; i++)
            {
                Ranking[players[i].FinishedAt - 1] = players[i].ID;
            }

            for (int i = 0; i < Ranking.Length; i++)
            {
                Writer.AnnounceFinishers(i+1, players[Ranking[i]].Name, PlayersFields[Ranking[i]], players[Ranking[i]].BackgroundColor, players[Ranking[i]].ForegroundColor);
            }

        }
        /// <summary>
        /// Megadja, hogy mely játékosnak hány mező volt a birtokában
        /// </summary>
        /// <returns>int[], amely ID helyesn tartalmazza a mezők darabszámát játékosonként</returns>
        private int[] GetPlayersFields()
        {
            int[] fieldCounts = new int[players.Length];
            for (int i = 0; i<fields.Length; i++)
            {
                if (fields[i].OwnerID != -1)
                {
                    fieldCounts[fields[i].OwnerID]++;
                }
            }

            return fieldCounts;
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
            return minField.PriceString;
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
                    Writer.WriteFieldToBoard(fields[i].GetTop(), fields[i].Flag, players[fields[i].OwnerID].BackgroundColor, players[fields[i].OwnerID].ForegroundColor, fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                else
                {
                    Writer.WriteFieldToBoard(fields[i].GetTop(), fields[i].Flag,Writer.BaseBgColor,Writer.BaseFgColor, fields[i].BoardPlacementLeft, fields[i].BoardPlacementTop);
                }
                Player[] OnField = fields[i].GetPlayersOnField(ref players);
                for (int j = 0; j < OnField.Length; j++)
                {
                    if (OnField[j].InGame)
                    {
                        Writer.WritePlayerOnField((OnField[j].ID + 1).ToString(), fields[i].BoardPlacementLeft + 2 + j, fields[i].BoardPlacementTop + 1, OnField[j].BackgroundColor, OnField[j].ForegroundColor);
                    }
                }
            }
            PostPlayerStatuses();
            if (!reDraw)
            {
                Console.SetCursorPosition(0, (fields.Length / 4 + 1) * Field.Height + players.Length + 2);
                Writer.WritePlayerRound($"{turnCounter + 1}. játékos", players[turnCounter].BackgroundColor, players[turnCounter].ForegroundColor);
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
                if (players[i].InGame)
                {
                    Writer.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $", players[i].BackgroundColor, players[i].ForegroundColor);
                }
                else
                {
                    Writer.WritePlayerStatus($"{i + 1}. játékos", $"{players[i].Money} $ (Kiesett, {players[i].FinishedAt}. helyezett)", players[i].BackgroundColor, players[i].ForegroundColor);
                }
            }
            Writer.WriteDivider();
            Console.SetCursorPosition(left, top);
        }
    }
}
