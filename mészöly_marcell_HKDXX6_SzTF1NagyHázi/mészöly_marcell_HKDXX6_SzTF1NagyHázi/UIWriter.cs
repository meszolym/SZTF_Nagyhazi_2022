using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class UIWriter
    {
        /// <summary>
        /// Kiírja az üdvözlést
        /// </summary>
        internal void WriteWelcome()
        {
            Console.WriteLine("Üdv az SzTF Monopolyban!");
            Console.WriteLine("A legjobb játékélmény (és a helyes megjelenítés) érdekében, kérlek, teljes képernyőn jelenítsd meg a konzolt!");
        }
        /// <summary>
        /// Tájékoztatja a felhasználót a kiinduló fájl bekéréséről.
        /// </summary>
        internal void AskForPath()
        {
            Console.Write("Tedd a kiinduló fájlt a \"Source\" mappába, és itt add meg a nevét (kiterjesztéssel!): ");
        }

        /// <summary>
        /// Tájékoztatja a felhasználót a kialakult hibáról.
        /// </summary>
        /// <param name="errorDesc">Hiba leírása</param>
        internal void WriteError(string errorDesc)
        {
            Console.WriteLine($"Hiba: {errorDesc}");
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot");
        }

        /// <summary>
        /// Tájékoztatja a felhasználót a beolvasás sikerességéről.
        /// </summary>
        internal void WriteSuccessfulRead()
        {
            Console.WriteLine("---");
            Console.WriteLine("Sikeres beolvasás! A továbblépéshez nyomd meg bármely gombot.");
            Console.ReadKey();
            Console.Clear();
        }

        internal void AnnounceWinner(string winner)
        {
            Console.Clear();
            WriteHeader();
            Console.WriteLine($"A győztes: {winner}");
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot");
            Console.ReadKey();
        }

        internal void WriteHeader()
        {
            WriteBoard();
            WriteCurrentGameStatus();
        }
    }
}
