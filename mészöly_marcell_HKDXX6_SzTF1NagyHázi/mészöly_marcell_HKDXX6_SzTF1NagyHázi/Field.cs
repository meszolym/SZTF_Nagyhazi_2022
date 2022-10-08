using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    internal class Field
    {
        internal int ID;
        internal int Price;
        internal int OwnerID;
        internal int BoardPlacementLeft;
        internal int BoardPlacementTop;
        /*
         * -1 = No owner
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */
        internal Field(int id, int price, int ownerID, int boardPlacementLeft, int boardPlacementTop)
        {
            ID = id;
            Price = price;
            OwnerID = ownerID;
            BoardPlacementLeft = boardPlacementLeft;
            BoardPlacementTop = boardPlacementTop;
        }
        /// <summary>
        /// Összehasonlítja két mező árát.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True, ha a ára nagyobb mint b ára, false, ha nem</returns>
        public static bool operator >(Field a, Field b)
        {
            return a.Price > b.Price;
        }
        /// <summary>
        /// Összehasonlítja két mező árát.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>True, ha b ára nagyobb mint a ára, false, ha nem</returns>
        public static bool operator <(Field a, Field b)
        {
            return a.Price < b.Price;
        }

        internal string GetOwnerString()
        {
            if (OwnerID == -1)
            {
                return "Nincs";
            }
            return $"{OwnerID + 1}. játékos";
        }

        internal string GetPriceString()
        {
            if (ID == 0)
            {
                return string.Empty;
            }
            return $"{Price} $";
        }

        internal string GetNameString()
        {
            if (ID == 0)
            {
                return "🏁 Startmező";
            }
            return $"{ID}. mező";
        }

        internal string GetTag()
        {
            if (ID == 0)
            {
                return "S";
            }
            if (OwnerID != -1)
            {
                return $"{ID}🏨";
            }
            return ID.ToString();
        }

        internal string GetTop()
        {
            string tag = GetTag();

            if (tag.Length == 1)
            {
                return $"┌────┤{tag}";
            }
            if (tag.Length == 2)
            {
                return $"┌───┤{tag}";
            }
            return $"┌──┤{tag}";
        }
        /// <summary>
        /// Megadja azoknak a játékosoknak az ID-ját, amelyek az adott mezőn állnak.
        /// </summary>
        /// <param name="players">A játékosokat tartalmazó tömb</param>
        /// <returns>int[] - A mezőn álló játékosok</returns>
        internal Player[] GetPlayersOnField(ref Player[] players)
        {
            
            int db = 0;
            for (int i = 0; i<players.Length; i++)
            {
                if (players[i].PlacementID == this.ID)
                {
                    db++;
                }
            }
            Player[] result = new Player[db];
            db = 0;
            for (int i = 0; i<players.Length; i++)
            {
                if (players[i].PlacementID == this.ID)
                {
                    result[db] = players[i];
                    db++;
                }
            }

            return result;
        }

    }
}
