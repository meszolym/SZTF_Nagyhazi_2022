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
        /*
         * -1 = No owner
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */
        internal Field(int id, int price, int ownerID)
        {
            ID = id;
            Price = price;
            OwnerID = ownerID;
        }
        public static bool operator >(Field a, Field b)
        {
            return a.Price > b.Price;
        }
        public static bool operator <(Field a, Field b)
        {
            return a.Price < b.Price;
        }
    }
}
