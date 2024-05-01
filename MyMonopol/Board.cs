using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonopoly
{
    public class Board
    {

        public Tile[,] tiles = new Tile[10, 10]
        {
            { new Tile("Start", 0, 0), new Tile("Tel-Aviv", 300,35), new Tile("Athens", 140,34), new Tile("Vienna", 140,33), new Tile("Dublin", 140, 32), new Tile("Prague", 140,31), new Tile("Warsaw", 140, 30), new Tile("Lisbon", 140, 29), new Tile("Budapest", 140, 28), new Tile("Jail", 0, 27) },
             { new Tile("Pettah-Tikva", 60, 1), new Tile("",0, 0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Nicosia",140, 26) },
             { new Tile("Reykjavik", 60, 2), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0, 0),new Tile("",0,0), new Tile("",0,0), new Tile("Andorra",125, 25) },
             { new Tile("Tallinn", 65, 3), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Tirana",125, 24) },
             { new Tile("Zagreb", 65, 4), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Podgorica",125,23) },
             { new Tile("Sofia", 70, 5), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Toronto",120,22) },
             { new Tile("Bucharest", 70, 6), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("New York",120,21) },
             { new Tile("Riga", 80, 7), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("Monaco",120,20) },
             { new Tile("Vilnius", 80, 8), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0), new Tile("",0,0),new Tile("",0,0), new Tile("",0,0), new Tile("London",120,19) },
             { new Tile("Go to Jail",0, 9),  new Tile("Brussels",95,10), new Tile("Oslo",95,11), new Tile("Copenhagen",100,12), new Tile("Helsinki",100,13), new Tile("Ljubljana",110,14), new Tile("Tokyo",110,15), new Tile("Bern",110,16), new Tile("Luxembourg",110,17), new Tile("Parking",0,18) },

        }; //creating the cities at the tiles by using tiles. what's in the middle is written nothing baecuse there is no city in there.

        public void CreateTiles(Graphics g, Size clientSize)
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
                        string tileName = tiles[row, column].GetName();
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
        }//creates on the board the tile that are 
        private static bool pointIsTilePlace(int x, int y)
        {
            if (x == 0 || x == 9) return true;
            if (y == 0 || y == 9) return true;
            return false;
        }
    }
}
