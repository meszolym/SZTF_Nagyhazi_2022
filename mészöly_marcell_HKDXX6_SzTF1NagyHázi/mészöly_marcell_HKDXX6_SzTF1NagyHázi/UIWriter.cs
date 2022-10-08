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
        internal static void WriteWelcome()
        {
            Console.WriteLine("Üdv az SzTF Monopolyban!");
            Console.WriteLine("A legjobb játékélmény (és a helyes megjelenítés) érdekében, kérlek, teljes képernyőn jelenítsd meg a konzolt!");
        }
        /// <summary>
        /// Tájékoztatja a felhasználót a kiinduló fájl bekéréséről.
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
            Console.WriteLine("---");
            Console.WriteLine("Sikeres beolvasás! A továbblépéshez nyomd meg bármely gombot.");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// Bejelenti a győztest.
        /// </summary>
        /// <param name="winner">A győztes neve</param>
        internal static void AnnounceWinner(string winner)
        {
            Console.WriteLine($"A győztes: {winner}");
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot");
            Console.ReadKey();
        }
        /// <summary>
        /// Kiírja egy adott játékos státuszát
        /// </summary>
        /// <param name="playerName">Játékos "neve"</param>
        /// <param name="playerMoney">Játékos pénzösszege</param>
        internal static void WritePlayerStatus(string playerName, string playerMoney)
        {
            Console.WriteLine($"{playerName}: {playerMoney}");
        }
        /// <summary>
        /// Kiír egy elválasztót
        /// </summary>
        internal static void WriteDivider()
        {
            Console.WriteLine("---");
        }

        /// <summary>
        /// Kiírja a játékos lépés előtti státuszát, a mező tulajdonságaival együtt, amin áll.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        /// <param name="ownerName">A mező birtokosának "neve"</param>
        /// <param name="priceString">A mező ára</param>
        internal static void PlacementBeforeRoll(string fieldName, string ownerName, string priceString)
        {
            Console.WriteLine($"📍 Aktuális mező, ahol állsz: {fieldName}");
            Console.WriteLine($"📈 Ára: {priceString}");
            Console.WriteLine($"🧑 Birtokosa: {ownerName}");
            Console.WriteLine("🎲 Dobáshoz nyomd meg bármely gombot.");
        }
        /// <summary>
        /// Kiírja a játékos lépés előtti státuszát, a mező tulajdonságai nélkül, amin áll.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        internal static void PlacementBeforeRoll(string fieldName)
        {
            Console.WriteLine($"📍 Aktuális mező, ahol állsz: {fieldName}");
            Console.WriteLine("🎲 Dobáshoz nyomd meg bármely gombot.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy át-/rálépett a startmezőn/-re, illetve a jutalmáról.
        /// </summary>
        /// <param name="prize">Az átlépés jutalma</param>
        internal static void WriteCrossedStart(string prize)
        {
            Console.WriteLine($"🤑 Át-/ráléptél a start mezőn/-re így jutalmad: {prize}");
        }
        /// <summary>
        /// Kiírja a mezőt, a megadott tulajdonossal.
        /// </summary>
        /// <param name="topRow">Az első kiírandó sora a mezőnek (Field.GetTop())</param>
        /// <param name="tag">A mező tag-je (Field.GetTag())</param>
        /// <param name="tagBgColor">A tag háttérszíne (Player.BgColor)</param>
        /// <param name="tagFgColor">A tag szövegszíne (Player.FgColor)</param>
        /// <param name="left">A konzol bal oldalától való távolság</param>
        /// <param name="top">A konzol tetejétől való távolság</param>

        internal static void WriteFieldWithOwner(string topRow, string tag, ConsoleColor tagBgColor, ConsoleColor tagFgColor, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(topRow);
            Console.BackgroundColor = tagBgColor;
            Console.ForegroundColor = tagFgColor;
            Console.Write(tag);
            Console.ResetColor();
            Console.SetCursorPosition(left, top + 1);
            Console.Write("|      |");
            Console.SetCursorPosition(left, top + 2);
            Console.Write("└──────┘");
        }


        /// <summary>
        /// Kiírja a tulajdonos nélküli mezőt
        /// </summary>
        /// <param name="topRow">Az első kiírandó sora a mezőnek (Field.GetTop())</param>
        /// <param name="tag">A mező tag-je (Field.GetTag())</param>
        /// <param name="left">A konzol bal oldalától való távolság</param>
        /// <param name="top">A konzol tetejétől való távolság</param>
        internal static void WriteFieldNoOwner(string topRow, string tag, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(topRow);
            Console.Write(tag);
            Console.SetCursorPosition(left, top+1);
            Console.Write("|      |");
            Console.SetCursorPosition(left, top + 2);
            Console.Write("└──────┘");

        }
        /// <summary>
        /// Kiírja a mezőn álló egyik játékost.
        /// </summary>
        /// <param name="num">A játékos száma (id+1)</param>
        /// <param name="left">A konzol bal oldalától való távolság, ahova írhatjuk a játékos számát (A mezőhöz képest +2+ahányadik játékost írjuk)</param>
        /// <param name="top">A konzol tetejétől való távolság (A mezőhöz képest +1)</param>
        /// <param name="BgColor">A játékos háttérszíne</param>
        /// <param name="FgColor">A játékos szövegszíne</param>
        internal static void WritePlayerOnField(string num, int left, int top, ConsoleColor BgColor, ConsoleColor FgColor)
        {
            Console.SetCursorPosition(left, top);
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
            Console.Write(num);
            Console.ResetColor();
        }

    }
}
