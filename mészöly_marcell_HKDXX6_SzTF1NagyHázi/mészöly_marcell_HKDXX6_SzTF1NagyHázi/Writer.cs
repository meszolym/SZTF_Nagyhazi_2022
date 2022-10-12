using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Writer
    {
        static internal ConsoleColor BaseBgColor;
        static internal ConsoleColor BaseFgColor;

        #region indítás és fájlbeolvasás
        /// <summary>
        /// Kiírja az üdvözlést
        /// </summary>
        internal static void WriteWelcome()
        {
            Console.WriteLine(" /$$      /$$                                                   /$$          ");
            Console.WriteLine("| $$$    /$$$                                                  | $$          ");
            Console.WriteLine("| $$$$  /$$$$  /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$ | $$ /$$   /$$");
            Console.WriteLine("| $$ $$/$$ $$ /$$__  $$| $$__  $$ /$$__  $$ /$$__  $$ /$$__  $$| $$| $$  | $$");
            Console.WriteLine("| $$  $$$| $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$  \\ $$| $$| $$  | $$");
            Console.WriteLine("| $$\\  $ | $$| $$  | $$| $$  | $$| $$  | $$| $$  | $$| $$  | $$| $$| $$  | $$");
            Console.WriteLine("| $$ \\/  | $$|  $$$$$$/| $$  | $$|  $$$$$$/| $$$$$$$/|  $$$$$$/| $$|  $$$$$$$");
            Console.WriteLine("|__/     |__/ \\______/ |__/  |__/ \\______/ | $$____/  \\______/ |__/ \\____  $$");
            Console.WriteLine("                                           | $$                     /$$  | $$");
            Console.WriteLine("                                           | $$                    |  $$$$$$/");
            Console.WriteLine("                                           |__/                     \\______/ ");

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

        #region Táblához és fejléchez tartozó elemek
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
        /// Kiírja a mezőt.
        /// </summary>
        /// <param name="topRow">Az első kiírandó sora a mezőnek (Field.GetTop())</param>
        /// <param name="flag">A mező flag-je (Field.GetTag())</param>
        /// <param name="flagBgColor">A flag háttérszíne (tulajdonos háttérszíne)</param>
        /// <param name="flagFgColor">A flag szövegszíne (tulajdonos szövegszíne)</param>
        /// <param name="left">A konzol bal oldalától való távolság</param>
        /// <param name="top">A konzol tetejétől való távolság</param>

        internal static void WriteFieldToBoard(string topRow, string flag, ConsoleColor flagBgColor, ConsoleColor flagFgColor, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(topRow);
            Console.BackgroundColor = flagBgColor;
            Console.ForegroundColor = flagFgColor;
            Console.Write(flag);
            Console.ResetColor();
            Console.SetCursorPosition(left, top + 1);
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
        #endregion

        #region lépésekhez és dobásokhoz tartozó elemek
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
        /// Tájékoztatja a játékost a dobott értékről.
        /// </summary>
        /// <param name="rolledvalue">A dobott érték</param>
        internal static void WriteRolledValue(string startingFieldName, int rolledvalue)
        {
            Console.WriteLine($"📍 Mező, ahonnan indultál: {startingFieldName}");
            Console.WriteLine($"🎲 Dobott érték: {rolledvalue}");
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
        /// Tájékoztatja a játékost, hogy a köre véget ért.
        /// </summary>
        internal static void WriteEndOfRound()
        {
            Console.WriteLine("🛑 A köröd véget ért.");
            Console.WriteLine("⏩ A továbblépéshez nyomd meg bármelyik gombot.");
        }

        #endregion

        #region mezővásárláshoz tartozó elemek

        /// <summary>
        /// Tájékoztatja a játékost a vásárlási szándék bekéréséről.
        /// </summary>
        internal static void AskBuyQuestion()
        {
            Console.Write("🏨 Mező megvétele? (I/N): ");
        }

        /// <summary>
        /// Tájékoztatja a játékost, hogy a vásárlási szándékot nem megfelelően adta meg, illetve tájékoztatja az újabb bekérésről.
        /// </summary>
        internal static void WriteErrorBuyAnswer()
        {
            Console.Write("Nem megfelelő válasz. Próbálkozz újra: ");
        }

        /// <summary>
        /// Tájékoztatja a játékost, hogy az adott mezőt nem tudja megvenni pénz hiányában.
        /// </summary>
        internal static void WriteCannotBuy()
        {
            Console.WriteLine("😢 Nincs pézed megvenni ezt a mezőt.");
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
        internal static void WriteNotBoughtField()
        {
            Console.WriteLine("🙅 A mezőt nem vetted meg.");
        }
        #endregion

        #region bérleti díjhoz tartozó elemek

        /// <summary>
        /// Tájékoztatja a játékost, hogy a mező bérleti díja levonásra került a számlájáról.
        /// </summary>
        internal static void RentPayment()
        {
            Console.WriteLine($"💸 Mivel ráléptél, a mező árát átutaltuk a birtokosnak.");
        }

        /// <summary>
        /// Tájékoztatja a játékost, hogy az adott mezőn nem kell bérleti díjat fizessen, mert a tulajdonosa kiesett a játékból.
        /// </summary>
        internal static void WriteNoRent()
        {
            Console.WriteLine("🤑 Mivel a birtokos már kiesett a játékból, így nem kell fizetned.");
        }

        /// <summary>
        /// Tájékoztatja a játékost, hogy csődbe ment.
        /// </summary>
        /// 
        internal static void WentBankrupt()
        {
            Console.WriteLine("📉 Erre sajnos nem volt elég pénzed, így csődbe mentél.");
        }
        #endregion

        #region játék végéhez tartozó elemek
        /// <summary>
        /// Bejelenti a győztest.
        /// </summary>
        /// <param name="winner">A győztes "neve"</param>
        /// <param name="bgColor">A győztes háttérszíne</param>
        /// <param name="fgColor">A győztes szövegszíne</param>
        internal static void AnnounceWinner(string winner, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            Console.Write("\U0001f947 A győztes: ");
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = fgColor;
            Console.WriteLine(winner);
            Console.ResetColor();
            Console.WriteLine("A program bezárásához nyomd meg bármely gombot.");
        }
        #endregion
    }
}
