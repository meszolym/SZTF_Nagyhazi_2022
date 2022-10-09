using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Writer
    {
        #region indítás és fájlbeolvasás
        /// <summary>
        /// Kiírja az üdvözlést
        /// </summary>
        internal static void WriteWelcome()
        {
            Console.WriteLine("Üdv az SzTF Monopolyban!");
            Console.WriteLine("A legjobb játékélmény érdekében, kérlek, teljes képernyőn jelenítsd meg a konzolt!");
        }
        /// <summary>
        /// Tájékoztatja a felhasználót a kiinduló fájl nevének bekéréséről.
        /// </summary>
        internal static void AskForPath()
        {
            Console.Write("Tedd a kiinduló fájlt a \"Source\" mappába, és itt add meg a nevét (kiterjesztéssel!): ");
        }

        /// <summary>
        /// Tájékoztatja a felhasználót a kialakult hibáról.
        /// </summary>
        /// <param name="errorDesc">Hiba leírása</param>
        internal static void WriteError(string errorDesc)
        {
            Console.WriteLine($"Hiba: {errorDesc}");
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot");
        }

        /// <summary>
        /// Tájékoztatja a felhasználót a beolvasás sikerességéről.
        /// </summary>
        internal static void WriteSuccessfulRead()
        {
            Console.WriteLine("Sikeres beolvasás! A továbblépéshez nyomd meg bármely gombot.");
            Console.ReadKey();
            Console.Clear();
        }
        #endregion

        #region univerzális elemek
        /// <summary>
        /// Kiír egy elválasztót
        /// </summary>
        internal static void WriteDivider()
        {
            Console.WriteLine("---");
        }
        #endregion
    }
}
