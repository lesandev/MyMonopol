using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyMonopoly
{
    public class Player
    {
        private int playerName;
        private double amountOfMoney = 1000;
        private Tile[] currentPlayerOwnedCity = new Tile[10];
        private int playerLandedIndex = 0;
        private int gameAmountOfOwnedCities = 0;
        private int punishment;
        private Point Position;

        public Player(Point position, int playerName, int playerLandedIndex)
        {
            this.Position = position;
            this.playerName = playerName;
            this.playerLandedIndex = playerLandedIndex;
            punishment = 0;
        }
        public Point GetPosition()
        {
            return Position;
        }
        public void SetPosition(Point newPosition)
        {
            Position = newPosition;
        }
        public Player(int playerName)
        {
            this.playerName = playerName;
            punishment = 0;
        }
        public double GetAmountOfMoney()
        {
            return amountOfMoney;
        }
        public void AddPunishment(int add)
        {
            punishment += add;
        }
        public void RemovePunishment(int remove)
        {
            punishment -= remove;
        }
        public int GetPlayerHowManyIndex()
        {
            return punishment;
        }
        public void SetHowManyOwnedCities(int howManyOwnedCities)
        {
            gameAmountOfOwnedCities = howManyOwnedCities;
        }
        //public Tile FindTileByName(string cityName)
        //{
        //    for (int i = 0; i < ownedCity.Length; i++)
        //    {
        //        if (ownedCity[i] == cityName)
        //        {
        //            return ownedCity[i];
        //        }
        //    }
        //}
        public int GetPlayerName()
        {
            return playerName;
        }
        public void SetAmountOfMoney(double amountOfMoneyNew)
        {
            amountOfMoney += amountOfMoneyNew;
        }
        public int GetAmountOfOwnedCities() { return gameAmountOfOwnedCities; }
        public void SetOwnedCityPlayers(Tile[] OwnedCity)
        {
            //if(currentPlayerOwnedCity.Length > this.currentPlayerOwnedCity.Length)
            //{ 


            //    Tile[] tempOwnedCity = new Tile[currentPlayerOwnedCity.Length + 1];      

            //}
            currentPlayerOwnedCity = OwnedCity;
        }
        public int GetPunishment()
        {
            return punishment;
        }
        public void Move(Point newPosition)
        {
            Position = newPosition;
        }
        public Tile[] GetOwnedCityPlayer()
        {
            return currentPlayerOwnedCity;
        }

        //THIS IS GETTER AND SETTER

        public int PlayerLandedIndex
        {
            get { return playerLandedIndex; }
            set { playerLandedIndex = value; }
        }


        public void CreatePlayer(Graphics g, int tileSize, int counter)
        {
            int playerX = Position.X * tileSize + tileSize / 2 - 10 * counter;
            int playerY = Position.Y * tileSize + tileSize / 2 - 10 * counter;

            if (counter == 0)
            {
                g.FillEllipse(Brushes.Red, playerX, playerY, 22, 22);
                g.DrawString((counter + 1).ToString(), SystemFonts.DefaultFont, Brushes.White, playerX + 6, playerY + 6);


            }
            if (counter == 1)
            {
                g.FillEllipse(Brushes.Green, playerX, playerY, 22, 22);
                g.DrawString((counter + 1).ToString(), SystemFonts.DefaultFont, Brushes.White, playerX + 6, playerY + 6);

            }
            if (counter == 2)
            {
                g.FillEllipse(Brushes.Blue, playerX, playerY, 22, 22);
                g.DrawString((counter + 1).ToString(), SystemFonts.DefaultFont, Brushes.White, playerX + 6, playerY + 6);

            }
            if (counter == 3)
            {
                g.FillEllipse(Brushes.Brown, playerX, playerY, 22, 22);
                g.DrawString((counter + 1).ToString(), SystemFonts.DefaultFont, Brushes.White, playerX + 6, playerY + 6);

            }
        }
        public bool IsPlayerOwnerOfCity(Tile city)
        {
            for (int i = 0; i < currentPlayerOwnedCity.Length; i++)
            {
                if (currentPlayerOwnedCity[i] == city)
                {
                    return true;
                }
            }
            return false;
        }

    }
}


