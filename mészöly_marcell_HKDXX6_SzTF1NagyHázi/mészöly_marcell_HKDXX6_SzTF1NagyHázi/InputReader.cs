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
        /// Az olvasás futtatásáért felel.
        /// </summary>
        /// <param name="writer">A UI írására használt objektum, hibák kiírásához</param>
        /// <param name="players">Inicializált tömb, amelybe a játékosokat kívánjuk helyezni</param>
        /// <param name="fields">Null értékű tömb, amelybe a mezőket kívánjuk helyezni</param>
        internal Game RunRead(ref UIWriter writer)
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

        #region beolvasáshoz kapcsolódó függvények és eljárások
        /// <summary>
        /// Ellenőrzi, hogy létezik-e a fájl.
        /// </summary>
        /// <param name="path">Fájl elérési útja</param>
        /// <returns></returns>
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
        /// <param name="path">Fájl elérési útja</param>
        /// <param name="players">Inicializált tömb, amelybe a játékosokat kívánjuk helyezni</param>
        /// <param name="fields">Null értékű tömb, amelybe a mezőket kívánjuk helyezni</param>
        /// <param name="errorDesc">Üres string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
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
        /// <param name="players">Inicializált tömb, amelybe a játékosokat kívánjuk helyezni</param>
        /// <param name="errorDesc">Üres string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
        private bool ReadMoney(ref StreamReader sr, ref string errorDesc)
        {
            int money;
            if (!int.TryParse(sr.ReadLine(), out money) || money <= 0)
            {
                errorDesc = "Nem megfelelő kezdőtőke.";
                return false;
            }
            for (int i = 0; i < game.players.Length; i++)
            {
                game.players[i] = new Player(i, money, 0, true);
            }
            return true;
        }

        /// <summary>
        /// Beolvassa a mezők számát és értékét, majd létrehozza a mezőket.
        /// </summary>
        /// <param name="sr">Inicializált fájlolvasó</param>
        /// <param name="fields">Null értékű tömb, amelybe a mezőket kívánjuk helyezni</param>
        /// <param name="errorDesc">Üres string, amelybe a hibaleírást helyezzük, ha történik hiba</param>
        /// <returns>True, ha hiba nélkül lefut a beolvasás, False, ha hibába ütközik</returns>
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
            game.fields[0] = new Field(0, -1, -1); //start mező
            string[] prices = sr.ReadLine().Split(";");
            for (int i = 1; i < game.fields.Length; i++)
            {
                int price;

                if (!int.TryParse(prices[i - 1], out price) || price <= 0)
                {
                    errorDesc = $"Nem megfelelő ár a(z) {i}. helyen.";
                    return false;
                }
                game.fields[i] = new Field(i, price, -1);
            }
            return true;
        }

        /// <summary>
        /// A mezőket rendezi sorba ár szerint (javított beillesztéses rendezéssel, majd újra kiadja az ID-kat.
        /// </summary>
        /// <param name="fields">Mezőket tartalmazó tömb.</param>
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
        #endregion

    }
}
