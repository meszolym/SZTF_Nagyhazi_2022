using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    public class Player
    {
        public int ID;
        /*
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */
        public int Money;
        public int PlacementID;
        public bool inGame;
        public ConsoleColor bgColor;
        public ConsoleColor fgColor;

        public Player(int id, int money, int placement, bool inGame, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            ID = id;
            Money = money;
            PlacementID = placement;
            this.inGame = inGame;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        /// <summary>
        /// Előrelépteti a játékost, és visszaadja, hogy hová lépett.
        /// </summary>
        /// <param name="rolled">A megtenni kívánt lépések száma</param>
        /// <param name="fields">A mezőket tartalmazó tömb</param>
        /// <returns>Field - A mező, ahova a játékos érkezett</returns>
        public Field StepForward(int rolled, ref Field[] fields)
        {
            PlacementID += rolled;
            if (PlacementID >= fields.Length)
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
        public Field GetPlacementField(ref Field[] fields)
        {
            return fields[PlacementID];
        }

        public string GetName()
        {
            return $"{ID + 1}. játékos";
        }

    }
}
