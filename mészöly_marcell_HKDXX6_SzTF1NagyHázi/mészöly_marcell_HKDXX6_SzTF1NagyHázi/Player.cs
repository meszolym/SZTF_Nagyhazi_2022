using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mészöly_marcell_HKDXX6_SzTF1NagyHázi
{
    public class Player
    {
        private readonly int id;
        /*
         * 0 = Player1
         * 1 = Player2
         * 2 = Player3
         * 3 = Player4
         */
        public int ID { get { return id; } }

        public string Name { get { return $"{ID + 1}. játékos"; } }

        private int money;

        public int Money 
        { 
            get { return money; } 
            set {
                if (money > 0)
                {
                    money = value;
                }
                    
            }
        }

        public bool InGame
        {
            get { return money > 0; }
        }

        private int placementID;
        public int PlacementID { get { return placementID; } }

        private int finishedAt;
        public int FinishedAt
        {
            get { return finishedAt; }
            set { 
                if (!InGame)
                {
                    finishedAt = value;
                }
            }
        }

        private readonly ConsoleColor bgColor;
        public ConsoleColor BackgroundColor { get { return bgColor; } }
        private readonly ConsoleColor fgColor;
        public ConsoleColor ForegroundColor { get { return fgColor; } }
        public Player(int id, int money, ConsoleColor bgColor, ConsoleColor fgColor)
        {
            this.id = id;
            this.money = money;
            placementID = 0;
            this.bgColor = bgColor;
            this.fgColor = fgColor;
        }

        /// <summary>
        /// Előrelépteti a játékost, egy dobott értékkel.
        /// </summary>
        /// <param name="rolled">A megtenni kívánt lépések száma</param>
        /// <param name="FieldsMax">A mezőket tartalmazó tömb mérete</param>
        public void StepForward(int rolled, int FieldsMax)
        {
            placementID += rolled;
            if (placementID >= FieldsMax)
            {
                placementID -= FieldsMax;
            }
        }



    }
}
