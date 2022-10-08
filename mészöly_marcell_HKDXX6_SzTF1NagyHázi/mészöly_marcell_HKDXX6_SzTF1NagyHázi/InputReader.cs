using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class InputReader
    {
        internal string path;
        internal Game game;
        internal InputReader(string Path)
        {
            path = Path;
            game = new Game();
        }

        /// <summary>
        /// Az játékadatok beolvasásának futtatásáért felel.
        /// </summary>
        /// <param name="writer">A UI írására használt objektum, hibák kiírásához</param>
        /// <returns>Game - a létrehozott játék</returns>
        internal Game ReadGame(ref UIWriter writer)
        {
            Console.ReadLine();
            if (!fetchFile())
            {
                writer.WriteError("A fájl nem létezik.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string errorDesc = string.Empty;
            if (!ReadFile(ref errorDesc))
            {
                writer.WriteError($"Hiba a fájlfeldolgozás során: {errorDesc}");
                Console.ReadKey();
                Environment.Exit(0);
            }
            SortFields();
            return game;
        }

        /// <summary>
        /// Ellenőrzi, hogy létezik-e a fájl.
        /// </summary>
        /// <returns>bool - True, ha a fájl létezik, false, ha nem</returns>
        private bool fetchFile()
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
        /// <param name="errorDesc">Üres string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadFile(ref string errorDesc)
        {
            StreamReader sr = new StreamReader(path);
            if (!ReadMoney(ref sr, ref errorDesc))
            {
                return false;
            }
            if (!ReadFields(ref sr, ref errorDesc))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Beolvassa a játékosok kezdőtőkéjét és létrehozza a játékosokat azzal.
        /// </summary>
        /// <param name="sr">Inicializált fájlolvasó</param>
        /// <param name="errorDesc">Üres string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadMoney(ref StreamReader sr, ref string errorDesc)
        {
            ConsoleColor[,] colorSchemas = new ConsoleColor[4, 2]
            {
                {ConsoleColor.Red,ConsoleColor.White},
                {ConsoleColor.Blue,ConsoleColor.White},
                {ConsoleColor.DarkGreen,ConsoleColor.Yellow},
                {ConsoleColor.White,ConsoleColor.Black}
                //{backcolor, forecolor}
            };

            int money;
            if (!int.TryParse(sr.ReadLine(), out money) || money <= 0)
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
        /// <param name="sr">Inicializált fájlolvasó</param>
        /// <param name="errorDesc">res string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>bool - True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadFields(ref StreamReader sr, ref string errorDesc)
        {
            int numOfFields;
            if (!int.TryParse(sr.ReadLine(), out numOfFields) || (numOfFields + 1) % 4 != 0 || numOfFields < 0 || numOfFields > 48)
            {
                errorDesc = "Nem megfelelő mezőszám.";
                return false;
            }
            numOfFields++; //start mezőnek hely
            game.fields = new Field[numOfFields];
            game.fields[0] = new Field(0, -1, -1, 0, 0); //start mező
            string[] prices = sr.ReadLine().Split(";");
            int dim = numOfFields / 4;
            for (int i = 1; i < game.fields.Length; i++)
            {
                int price;

                if (!int.TryParse(prices[i - 1], out price) || price <= 0)
                {
                    errorDesc = $"Nem megfelelő ár a(z) {i}. helyen.";
                    return false;
                }

                if (i < dim) //felső sorban lesz
                {
                    game.fields[i] = new Field(i, price, -1,i*9,0);
                }
                else if (i < dim*2) //jobb oldali oszlopban lesz
                {
                    game.fields[i] = new Field(i, price, -1, (dim)*9, 3*(i-dim));
                }
                else if (i<dim*3) //alsó sorban lesz
                {
                    game.fields[i] = new Field(i, price, -1,(dim*3-i)*9,3*(dim));
                }
                else //(i < dim * 4) bal oldali oszlopban lesz
                {
                    game.fields[i] = new Field(i, price, -1,0,3*(dim*4-i));
                }
            }
            return true;
        }

        /// <summary>
        /// A mezőket rendezi sorba ár szerint (javított beillesztéses rendezéssel, majd újra kiadja az ID-kat.
        /// </summary>
        private void SortFields()
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
            for (int i = 0; i < game.fields.Length; i++)
            {
                game.fields[i].ID = i;
            }
        }

    }
}
