using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonopoly
{
    public class Tile
    {
        private string name;
        private double price;
        public bool isPurchased = false;
        public int placeIndex;

        public string Name() { return name; }

        public Tile() // DEFAULT VALUE CONSTRUCTOR
        {
            name = "just an abended city";
            price = 30;
            placeIndex = 0;
        }

        public Tile(string name, int price, int placeIndex)// CONSTRUCTOR WITH INDEX
        {
            this.name = name;
            this.price = price;
            this.placeIndex = placeIndex;
        }
        public double GetPrice()// GET PRICE OF PLACE
        {
            return price;
        }
        public string GetName()//GET NAME OF
        {
            return name;
        }


        public Tile FindTileByIndex(int index)
        {
            Board board = new Board();
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (board.tiles[row, col].placeIndex == index)
                    {
                        return board.tiles[row, col];
                    }
                }
            }

            return null;
        }
    }
}
