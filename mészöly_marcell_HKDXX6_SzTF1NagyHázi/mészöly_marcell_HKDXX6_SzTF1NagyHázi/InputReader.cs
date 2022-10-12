using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    public class InputReader
    {
        public string path;
        public Game game;
        public string errorDesc;
        public StreamReader sr;
        public InputReader(string Path)
        {
            path = Path;
            game = new Game();
            errorDesc = string.Empty;
        }

        /// <summary>
        /// Az játékadatok beolvasásának futtatásáért felel.
        /// </summary>
        /// <returns>Game - a létrehozott játék</returns>
        public Game ReadGame()
        {
            if (!FetchFile())
            {
                Writer.WriteError("A fájl nem létezik.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            sr = new StreamReader(path);
            if (!ReadFile())
            {
                Writer.WriteError($"Hiba a fájlfeldolgozás során: {errorDesc}");
                Console.ReadKey();
                Environment.Exit(0);
            }
            SortAndAssignFields();
            return game;
        }

        /// <summary>
        /// Ellenőrzi, hogy létezik-e a fájl.
        /// </summary>
        /// <returns>bool - True, ha a fájl létezik, false, ha nem</returns>
        private bool FetchFile()
        {
            if (!File.Exists($"{path}"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Elvégzi a fájlbeolvasást.
        /// </summary>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadFile()
        {
            if (!ReadMoney())
            {
                return false;
            }
            if (!ReadFields())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Beolvassa a játékosok kezdőtőkéjét és létrehozza a játékosokat azzal.
        /// </summary>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadMoney()
        {
            ConsoleColor[,] colorSchemas = new ConsoleColor[4, 2]
            {
                {ConsoleColor.Red,ConsoleColor.White},
                {ConsoleColor.Blue,ConsoleColor.White},
                {ConsoleColor.DarkGreen,ConsoleColor.Yellow},
                {ConsoleColor.White,ConsoleColor.Black}
                //{backcolor, forecolor}
            };

            if (!int.TryParse(sr.ReadLine(), out int money) || money <= 0 || money >= int.MaxValue/2)
            {
                errorDesc = "Nem megfelelő kezdőtőke.";
                return false;
            }
            for (int i = 0; i < game.players.Length; i++)
            {
                game.players[i] = new Player(i, money, 0, true, colorSchemas[i,0], colorSchemas[i, 1]);
            }
            return true;
        }

        /// <summary>
        /// Beolvassa a mezők számát és értékét, majd létrehozza a mezőket.
        /// </summary>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadFields()
        {
            if (!int.TryParse(sr.ReadLine(), out int numOfFields) || (numOfFields + 1) % 4 != 0 || numOfFields < 7 || numOfFields > 47)
            {
                errorDesc = "Nem megfelelő mezőszám.";
                return false;
            }
            numOfFields++; //start mezőnek hely
            game.fields = new Field[numOfFields];
            game.fields[0] = new Field(0, -1, -1); //start mező
            string[] prices = sr.ReadLine().Split(";");
            for (int i = 1; i < game.fields.Length; i++)
            {

                if (!int.TryParse(prices[i - 1], out int price) || price <= 0)
                {
                    errorDesc = $"Nem megfelelő ár a(z) {i}. helyen.";
                    return false;
                }

                game.fields[i] = new Field(i, price, -1);
            }
            return true;
        }

        /// <summary>
        /// A mezőket rendezi sorba ár szerint (javított beillesztéses rendezéssel, majd újra kiadja az ID-kat és beállítja a board megjelenítés során használt helyeket.
        /// </summary> 
        private void SortAndAssignFields()
        {
            for (int i = 1; i < game.fields.Length; i++)
            {
                int j = i - 1;
                Field helper = game.fields[i];
                while (j > 0 && game.fields[j] > helper)
                {
                    game.fields[j + 1] = game.fields[j];
                    j--;
                }
                game.fields[j + 1] = helper;
            }
            int dim = game.fields.Length / 4;
            for (int i = 0; i < game.fields.Length; i++)
            {
                game.fields[i].ID = i;

                if (i < dim) //felső sorban lesz
                {
                    game.fields[i].BoardPlacementLeft = i * Field.Width;
                    game.fields[i].BoardPlacementTop = 0;
                }
                else if (i < dim * 2) //jobb oldali oszlopban lesz
                {
                    game.fields[i].BoardPlacementLeft = (dim) * Field.Width;
                    game.fields[i].BoardPlacementTop = Field.Height * (i - dim);
                }
                else if (i < dim * 3) //alsó sorban lesz
                {
                    game.fields[i].BoardPlacementLeft = (dim * 3 - i) * Field.Width;
                    game.fields[i].BoardPlacementTop = Field.Height * (dim);
                }
                else //(i < dim * 4) bal oldali oszlopban lesz
                {
                    game.fields[i].BoardPlacementLeft = 0;
                    game.fields[i].BoardPlacementTop = Field.Height * (dim * 4 - i);
                }
            }

        }

    }
}
