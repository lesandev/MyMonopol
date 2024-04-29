using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonopol
{
    public class Board
    {

        public static Tile[,] tiles = new Tile[10, 10]
{
            { new Tile("Start", 0, 0), new Tile("Tel-Aviv", 30,35), new Tile("Athens", 40,34), new Tile("Vienna", 50,33), new Tile("Dublin", 60, 32), new Tile("Prague", 70,31), new Tile("Warsaw", 80, 30), new Tile("Lisbon", 90, 29), new Tile("Budapest", 100, 28), new Tile("Jail", 0, 27) },
             { new Tile("Petach-Tikva", 190, 1), new Tile("",0, 0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Nicosia",10, 26) },
             { new Tile("Reykjavik", 30, 2), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0, 0),new Tile("",0,0), new Tile("",0,0), new Tile("Andorra",45, 25) },
             { new Tile("Tallinn", 20, 3), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Tirana",98, 24) },
             { new Tile("Zagreb", 60, 4), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Podgorica",65,23) },
             { new Tile("Sofia", 40, 5), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Toronto",25,22) },
             { new Tile("Bucharest", 50, 6), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("New York",10,21) },
             { new Tile("Riga", 90, 7), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Monaco",35,20) },
             { new Tile("Vilnius", 90, 8), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("London",10,19) },
             { new Tile("Go to Jail",0, 9),  new Tile("Brussels",50,10), new Tile("Oslo",70,11), new Tile("Copenhagen",90,12), new Tile("Helsinki",55,13), new Tile("Ljubljana",80,14), new Tile("Tokyo",90,15), new Tile("Bern",80,16), new Tile("Luxembourg",80,17), new Tile("Parking",0,18) },

};

        public static void CreateTiles(Graphics g, Size clientSize)
        {
            int tileSize = Math.Min(clientSize.Width / 10, clientSize.Height / 10);

            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    if (pointIsTilePlace(row, column))
                    {
                        int pointX = row * tileSize;
                        int pointY = column * tileSize;

                        g.FillRectangle(Brushes.White, pointX, pointY, tileSize, tileSize);
                        g.DrawRectangle(Pens.Black, pointX, pointY, tileSize, tileSize);
                        string tileName = tiles[row, column].getName();
                        string tilePrice = tiles[row, column].GetPrice().ToString();
                        // to check to set next line inside the "if" 
                        g.DrawString(tileName, SystemFonts.DefaultFont, Brushes.Black, pointX + 5, pointY + 5);
                        if (tilePrice != "0")
                        {
                            g.DrawString(tilePrice, SystemFonts.DefaultFont, Brushes.Black, pointX + 5, pointY + 20);

                        }
                    }

                }
            }
        }
        private static bool pointIsTilePlace(int x, int y)
        {
            if (x == 0 || x == 9) return true;
            if (y == 0 || y == 9) return true;

            return false;

        }
    }
}
