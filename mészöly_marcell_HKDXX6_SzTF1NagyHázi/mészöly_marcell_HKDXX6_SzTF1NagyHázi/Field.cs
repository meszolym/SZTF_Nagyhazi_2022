﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    public class Field
    {
        
        public const int Height = 3; //kiírási magasság
        public const int Width = 9; //kiírási szélesség, beleszámítva egy szóköz elválasztást
        private int id;
        public int ID
        {
            get { return id; }
        }
        private int price;

        public int Price
        {
            get { return price; }
        }

        public int OwnerID;
        /*
         * -1 = No owner
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */

        private int boardPlacementLeft;
        public int BoardPlacementLeft
        {
            get { return boardPlacementLeft; }
        }
        private int boardPlacementTop;
        public int BoardPlacementTop
        {
            get { return boardPlacementTop; }
        }

        public Field(int _id, int price, int ownerID)
        {
            id = _id;
            this.price = price;
            OwnerID = ownerID;
        }
        /// <summary>
        /// Összehasonlítja két mező árát.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Bool - True, ha a ára nagyobb mint b ára, false, ha nem</returns>
        public static bool operator >(Field a, Field b)
        {
            return a.price > b.price;
        }
        /// <summary>
        /// Összehasonlítja két mező árát.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Bool - True, ha b ára nagyobb mint a ára, false, ha nem</returns>
        public static bool operator <(Field a, Field b)
        {
            return a.price < b.price;
        }
        /// <summary>
        /// Megadja a mező árát mértékegységgel.
        /// </summary>
        /// <returns>String - A mező ára mértékegységgel</returns>
        public string GetPriceString()
        {
            if (ID == 0)
            {
                return string.Empty;
            }
            return $"{price} $";
        }

        /// <summary>
        /// Megadja a mező "nevét".
        /// </summary>
        /// <returns>String - A mező "neve"</returns>
        public string GetNameString()
        {
            if (ID == 0)
            {
                return "🏁 Startmező";
            }
            return $"{ID}. mező";
        }

        /// <summary>
        /// Megadja egy mező jobb felső sarkában feltüntetendő Flag-et.
        /// </summary>
        /// <returns>string - a Flag</returns>
        public string GetFlag()
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
        /// <summary>
        /// Megadja a mező kiírásakor használandó felső border sort, a flag nélkül.
        /// </summary>
        /// <returns>String - A mező felső border sora</returns>
        public string GetTop()
        {
            string flag = GetFlag();
            if (flag.Length == 1)
            {
                return $"┌─────┤";
            }
            if (flag.Length == 2)
            {
                return $"┌────┤";
            }
            return $"┌───┤";
        }
        /// <summary>
        /// Megadja azoknak a játékosoknak az ID-ját, amelyek az adott mezőn állnak.
        /// </summary>
        /// <param name="players">A játékosokat tartalmazó tömb</param>
        /// <returns>int[] - A mezőn álló játékosok</returns>
        public Player[] GetPlayersOnField(ref Player[] players)
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
