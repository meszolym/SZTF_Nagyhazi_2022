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
        internal int Placement;
        internal bool inGame;

        internal Player(int id, int money, int placement, bool inGame)
        {
            ID = id;
            Money = money;
            Placement = placement;
            this.inGame = inGame;
        }
    }
}
