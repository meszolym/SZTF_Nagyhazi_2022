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
            Console.WriteLine("---");
            Console.WriteLine("Sikeres beolvasás! A továbblépéshez nyomd meg bármely gombot.");
            Console.ReadKey();
            Console.Clear();
        }
        /// <summary>
        /// Bejelenti a győztest.
        /// </summary>
        /// <param name="winner">A győztes "neve"</param>
        internal static void AnnounceWinner(string winner)
        {
            Console.WriteLine($"\U0001f947 A győztes: {winner}");
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot.");
        }

        /// <summary>
        /// Kiírja egy adott játékos státuszát
        /// </summary>
        /// <param name="playerName">Játékos "neve"</param>
        /// <param name="playerMoney">Játékos pénzösszege</param>
        /// <param name="BgColor">Játékos háttérszíne</param>
        /// <param name="FgColor">Játékos szövegszíne</param>
        internal static void WritePlayerStatus(string playerName, string playerMoney, ConsoleColor BgColor, ConsoleColor FgColor)
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
            Console.WriteLine($"{playerName}: {playerMoney}");
            Console.ResetColor();
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
        /// <param name="ownerBgColor">A mező birtokosának háttérszíne</param>
        /// <param name="ownerFgColor">A mező birtokosának szövegszíne</param>
        /// <param name="priceString">A mező árának szövege</param>
        internal static void PlacementBeforeRoll(string fieldName, string ownerName, ConsoleColor ownerBgColor, ConsoleColor ownerFgColor, string priceString)
        {
            Console.WriteLine($"📍 Aktuális mező, ahol állsz: {fieldName}");
            Console.WriteLine($"📈 Ára: {priceString}");
            Console.Write("🧑 Birtokosa: ");
            Console.BackgroundColor = ownerBgColor;
            Console.ForegroundColor = ownerFgColor;
            Console.WriteLine(ownerName);
            Console.ResetColor();
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
        /// Kiírja a játékos lépés előtti státuszát, a mező tulajdonságaival együtt, amin áll, ha nincs a mezőnek birtokosa.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        /// <param name="priceString">A mező árának szövege</param>
        internal static void PlacementBeforeRollNoOwner(string fieldName, string priceString)
        {
            Console.WriteLine($"📍 Aktuális mező, ahol állsz: {fieldName}");
            Console.WriteLine($"📈 Ára: {priceString}");
            Console.Write("🧑 Birtokosa: Nincs");
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
            Console.Write("│      │");
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
            Console.Write("│      │");
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
        /// <summary>
        /// Tájékoztatja a játékost a dobott értékről.
        /// </summary>
        /// <param name="rolledvalue">A dobott érték</param>
        internal static void WriteRolledValue(string startingFieldName, int rolledvalue)
        {
            Console.WriteLine($"📍 Mező, ahonnan indultál: {startingFieldName}");
            Console.WriteLine($"🎲 Dobott érték: {rolledvalue}");
        }
        /// <summary>
        /// Tájékoztatja a játékosokat, hogy melyikük köre van aktuálisan.
        /// </summary>
        /// <param name="playername">A játékos neve</param>
        /// <param name="BgColor">A játékos háttérszíne</param>
        /// <param name="FgColor">A játékos szövegszíne</param>
        internal static void WritePlayerRound(string playername, ConsoleColor BgColor, ConsoleColor FgColor)
        {
            Console.BackgroundColor = BgColor;
            Console.ForegroundColor = FgColor;
            Console.WriteLine($"🎮 {playername} köre");
            Console.ResetColor();
        }
        /// <summary>
        /// Kiírja a játékos lépés utáni státuszát, a mező tulajdonságai nélkül, amire érkezett.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        internal static void WritePlacementAfterRoll(string fieldName)
        {
            Console.WriteLine($"📍 Mező, ahova léptél: {fieldName}");
            Console.WriteLine("⏩ A továbblépéshez nyomd meg bármelyik gombot.");

        }

        /// <summary>
        /// Kiírja a játékos lépés utáni státuszát, a mező tulajdonságaival együtt, amire érkezett.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        /// <param name="ownerName">A mező birtokosának "neve"</param>
        /// <param name="ownerBgColor">A mező birtokosának háttérszíne</param>
        /// <param name="ownerFgColor">A mező birtokosának szövegszíne</param>
        /// <param name="priceString">A mező ára</param>
        internal static void WritePlacementAfterRoll(string fieldName, string ownerName, ConsoleColor ownerBgColor, ConsoleColor ownerFgColor, string priceString)
        {
            Console.WriteLine($"📍 Mező, ahova léptél: {fieldName}");
            Console.WriteLine($"📈 Ára: {priceString}");
            Console.Write("🧑 Birtokosa: ");
            Console.BackgroundColor = ownerBgColor;
            Console.ForegroundColor = ownerFgColor;
            Console.WriteLine(ownerName);
            Console.ResetColor();
        }

        /// <summary>
        /// Kiírja a játékos lépés utáni státuszát, a mező tulajdonságaival együtt, amire érkezett, ha nincs a mezőnek birtokosa.
        /// </summary>
        /// <param name="fieldName">A mező "neve"</param>
        /// <param name="priceString">A mező árának szövege</param>
        internal static void WritePlacementAfterRollNoOwner(string fieldName, string priceString)
        {
            Console.WriteLine($"📍 Mező, ahova léptél: {fieldName}");
            Console.WriteLine($"📈 Ára: {priceString}");
            Console.Write("🧑 Birtokosa: Nincs");
        }

        /// <summary>
        /// Tájékoztatja a játékost, hogy a köre véget ért.
        /// </summary>
        internal static void WriteEndOfRound()
        {
            Console.WriteLine("🛑 A köröd véget ért.");
            Console.WriteLine("⏩ A továbblépéshez nyomd meg bármelyik gombot.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy az adott mezőt nem tudja megvenni pénz hiányában.
        /// </summary>
        internal static void WriteCannotBuy()
        {
            Console.WriteLine("😢 Nincs pézed megvenni ezt a mezőt.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy az adott mezőn nem kell bérleti díjat fizessen, mert a tulajdonosa kiesett a játékból.
        /// </summary>
        internal static void WriteNoRent()
        {
            Console.WriteLine("🤑 Mivel a birtokos már kiesett a játékból, így nem kell fizetned.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy a mező bérleti díja levonásra került a számlájáról.
        /// </summary>
        internal static void RentPayment()
        {
            Console.WriteLine($"💸 Mivel ráléptél, a mező árát átutaltuk a birtokosnak.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy csődbe ment.
        /// </summary>
        internal static void WentBankrupt()
        {
            Console.WriteLine("📉 Erre sajnos nem volt elég pénzed, így csődbe mentél.");
        }
        /// <summary>
        /// Tájékoztatja a játékost a vásárlási szándék bekéréséről.
        /// </summary>
        internal static void AskBuyQuestion()
        {
            Console.Write("🏨 Mező megvétele? (I/N): ");
        }
        /// <summary>
        /// Tájékoztatja a játékost a vásárlás sikerességéről, és az ár levonásáról.
        /// </summary>
        internal static void WriteBoughtField()
        {
            Console.WriteLine("💸 A mezőt megvetted. Az összeget levontuk a számládról.");
        }
        /// <summary>
        /// Tájékoztatja a játékost, hogy a mezőt nem vásárolta meg.
        /// </summary>
        internal static void WriteNotBought()
        {
            Console.WriteLine("🙅 A mezőt nem vetted meg.");
        }
    }
}
