using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Player
    {
        internal int ID;
        /*
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */
        internal int Money;
        internal int PlacementID;
        internal bool inGame;
        internal ConsoleColor bgColor;
        internal ConsoleColor fgColor;

        internal Player(int id, int money, int placement, bool inGame, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            ID = id;
            Money = money;
            PlacementID = placement;
            this.inGame = inGame;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        /// <summary>
        /// Előrelépteti a játékost, és visszaadja, hogy átlépett-e a startmezőn.
        /// </summary>
        /// <param name="rolled">A megtenni kívánt lépések száma</param>
        /// <param name="maxfields">A mezők darabszáma</param>
        /// <returns>Bool - igaz, ha átlépett a starton, hamis, ha nem.</returns>
        internal Field StepForward(int rolled, ref Field[] fields)
        {
            PlacementID += rolled;
            if (PlacementID > fields.Length)
            {
                PlacementID -= fields.Length;
            }
            return fields[PlacementID];
        }

        /// <summary>
        /// Megadja a mezőt, ahol a játékos áll.
        /// </summary>
        /// <param name="fields">A mezők tömbje.</param>
        /// <returns>Field - A mező</returns>
        internal Field GetPlacementField(ref Field[] fields)
        {
            return fields[PlacementID];
        }

    }
}
