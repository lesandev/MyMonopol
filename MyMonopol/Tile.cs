using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonopol
{
    public class Tile
    {
        private string name;
        private double price;
        public bool isPurchased = false;
        public int placeIndex;

        public string Name() { return name; }

        public Tile() 
        {
            name = "just an abanded city";
            price = 30;
            placeIndex = 0;

        }

        public Tile(string name, int price, int placeIndex)
        {
            this.name = name;
            this.price = price;
            this.placeIndex = placeIndex;

        }

        public Tile(string name, double price)
        {
            this.name = name;
            this.price = price;
        }


        public double GetPrice()
        {
            return price;
        }
        public string getName()
        {
            return name;
        }


        public Tile FindTileByIndex(int index)
        {
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    if (Board.tiles[row, col].placeIndex == index)
                    {
                        return Board.tiles[row, col];
                    }
                }
            }

            return null;
        }

        //internal string GetName()
        //{
        //    throw new NotImplementedException();
        //}

        //public override string ToString()
        //{
        //    return base.ToString();
        //}
    }
}
