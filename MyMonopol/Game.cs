﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMonopol
{
    public class Game
    {
        private Player[] players;
        Tile currentTile = new Tile();
        private int move = 0;
        private int currentPlayerIndex = 0;
        private Tile[] gameOwnedCities = new Tile[10];
        private int ownedCitiesHowMany = 0;
        private Button button1_Click;
        private bool[] noPunishment;
        

        public Game()
        {

            players = new Player[4];

            noPunishment = new bool[4];
            for (int i = 0; i < noPunishment.Length; i++)
            {
                noPunishment[i] = true;
            }
        }
        public Game(int howMany)
        {
            players = new Player[howMany];
            noPunishment = new bool[howMany];
            for (int i = 0; i < noPunishment.Length; i++)
            {
                noPunishment[i] = true;
            }
        }
        public void MoveCurrentPlayer(int move, int currentPlayerIndex, Size ClientSize)
        {
            Player currentPlayer = players[currentPlayerIndex];
            int tileSize = Math.Min(ClientSize.Width / 10, ClientSize.Height / 10);
            Point newPosition;
            //   Board.CreateTiles(g, ClientSize);
            for (int i = 0; i < move; i++)
            {
                if (currentPlayer.Position.Y == 0 && currentPlayer.Position.X < 9)
                {
                    newPosition = new Point(currentPlayer.Position.X + 1, currentPlayer.Position.Y);
                    currentPlayer.Move(newPosition);
                    //          AnimatePlayerMove(currentPlayer, newPosition, tileSize, g);
                }
                else if (currentPlayer.Position.Y < 9 && currentPlayer.Position.X == 9)
                {
                    newPosition = new Point(currentPlayer.Position.X, currentPlayer.Position.Y + 1);
                    currentPlayer.Move(newPosition);
                    //           AnimatePlayerMove(currentPlayer, newPosition, tileSize, g);

                }
                else if (currentPlayer.Position.Y == 9 && currentPlayer.Position.X > 0)
                {
                    newPosition = new Point(currentPlayer.Position.X - 1, currentPlayer.Position.Y);
                    currentPlayer.Move(newPosition);
                    ///         AnimatePlayerMove(currentPlayer, newPosition, tileSize, g);

                }
                else
                {
                    newPosition = new Point(currentPlayer.Position.X, currentPlayer.Position.Y - 1);
                    currentPlayer.Move(newPosition);
                    //        AnimatePlayerMove(currentPlayer, newPosition, tileSize, g);
                }
            }


            if (currentPlayer.Position.Y == 0 && currentPlayer.Position.X == 9)
            {
                MessageBox.Show("you're at jail! you miss one turn");
                currentPlayer.AddPunishment(2);
                noPunishment[currentPlayerIndex] = false;
            }
            else if (currentPlayer.Position.Y == 9 && currentPlayer.Position.X == 0)
            {
                MessageBox.Show("you're going to jail! you miss one turn");
                newPosition = new Point(9, 0);
                currentPlayer.Move(newPosition);
                currentPlayer.AddPunishment(2);
                noPunishment[currentPlayerIndex] = false;
            }


            //currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
            //return currentPlayerIndex;



        }

        public void InitializePlayers()
        {
            for (int i = 0; i < playersAmount; i++)
            {
                players[i] = new Player(new Point(0, 0), i + 1, 0);
            }

        }
        public Player[] GetPlayers()
        {
            return players;
        }

        public int Throw_Dice()
        {
            Random rnd = new Random();
            int move = rnd.Next(1, 7);
            move += rnd.Next(0, 7);

            return move;
        }

        public void RollAndPLay(Size ClientSize)
        {
            for(int i = 0;i < players.Length; i++)
            {
                if (players[currentPlayerIndex].GetPunishment() > 0)
                {
                    players[currentPlayerIndex].RemovePunishment(1);
                    currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
                }
            }


            move = 12;

            MessageBox.Show($"Player {currentPlayerIndex + 1} moving " + move.ToString() + " slots");
            MoveCurrentPlayer(move, currentPlayerIndex, ClientSize);

            //get current player index of city
            int playerPlaceIndex = players[currentPlayerIndex].PlayerLandedIndex + move;

            if (playerPlaceIndex > 35)
            {
                // when pass through the Start tile
                playerPlaceIndex = playerPlaceIndex - 36;

            }

            //set updated city index to player
            players[currentPlayerIndex].PlayerLandedIndex = playerPlaceIndex;
            Tile tile = currentTile.FindTileByIndex(playerPlaceIndex);

            string cityName = tile.getName();
            if (players[currentPlayerIndex].GetPunishment() == 0)
            {
                MessageBox.Show($" Index = {playerPlaceIndex} City =  {cityName}");
            }


     
            bool ifOwnedByItsOwner = CheckIfCityOwnedByItsOwner(tile);
            bool OtherPlayerOwner = false;
            if (!ifOwnedByItsOwner)
            {
                OtherPlayerOwner = IfOwnedByOtherPlayer(tile);
            }
            if (!ifOwnedByItsOwner && noPunishment[currentPlayerIndex] == true && !OtherPlayerOwner)
            {
                DoYouWannaOwn(tile);
            }
            if (players[currentPlayerIndex].GetAmountOfMoney() < 10)
            {
                EndGameDueToMoney(currentPlayerIndex, (Monopoly)Application.OpenForms["Monopoly"]);
                return;
            }
            //    DoYouWannaEndGame();
            // Player currentPlayer = players[currentPlayerIndex];
            if (players[currentPlayerIndex].GetPunishment() > 0)
            {
                players[currentPlayerIndex].RemovePunishment(1);
                noPunishment[currentPlayerIndex] = false;
            }
           
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

        }

        private void EndGameDueToMoney(int currentPlayerIndex, Monopoly form)
        {
            MessageBox.Show($"Player {currentPlayerIndex + 1} has run out of money.Game Over!");

            form.Close(); // Close the form
        }
        public void PurchaseTheCity(int currentPlayerIndex, Player player)
        {

        }
        //    to check all functions to be private/public
        private bool CheckIfCityOwnedByItsOwner(Tile tile)
        {
            if (tile != null)
            {
                if (players[currentPlayerIndex].IsPlayerOwnerOfCity(tile))
                {
                    MessageBox.Show($"you stepped into your property!");
                    return true;
                }
            }

            return false;

        }
        private bool IfOwnedByOtherPlayer(Tile tile)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (i != currentPlayerIndex && players[i].IsPlayerOwnerOfCity(tile))
                {
                    MessageBox.Show($"Player {players[currentPlayerIndex].GetPlayerName()} landed on a property owned by {players[i].GetPlayerName()}");
                    double rent = tile.GetPrice() * 0.2;
                    players[currentPlayerIndex].SetAmountOfMoney(-rent);
                    players[i].SetAmountOfMoney(rent);
                    return true;
                }

            }
            return false;
        }
        private void DoYouWannaOwn(Tile tile)
        {
            DialogResult result = MessageBox.Show("Do you approve?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                double Pay = -1 * (tile.GetPrice());
                players[currentPlayerIndex].SetAmountOfMoney(Pay);
                if (gameOwnedCities.Length == ownedCitiesHowMany + 1)
                {
                    Tile[] ownedCitiesTemp = new Tile[gameOwnedCities.Length + 10];
                    for (int i = 0; i < gameOwnedCities.Length; i++)
                    {
                        ownedCitiesTemp[i] = gameOwnedCities[i];
                    }
                    ownedCitiesHowMany = ownedCitiesTemp.Length + 1;
                    ownedCitiesTemp[ownedCitiesHowMany] = tile;
                    gameOwnedCities = ownedCitiesTemp;
                }
                else
                {
                    gameOwnedCities[ownedCitiesHowMany + 1] = tile;
                }

                players[currentPlayerIndex].SetHowManyOwnedCities(players[currentPlayerIndex].GetAmountOfOwnedCities() + 1);

                players[currentPlayerIndex].GetOwnedCityPlayer()[players[currentPlayerIndex].GetAmountOfOwnedCities()] = tile;

            }

        }
        public void createPlayers(Graphics e, int tileSize)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].CreatePlayer(e, tileSize, i);
            }
        }
        public void showPlayersData(Size ClientSize, PaintEventArgs e)
        {
            int playerInfoWidth = ClientSize.Width / 4;

            // Draw the player information panel to the right of the game board
            int playerInfoX = ClientSize.Width - playerInfoWidth;
            int playerInfoY = 0;
            int playerInfoHeight = ClientSize.Height;

            // Fill the player info panel with a light gray color
            e.Graphics.FillRectangle(Brushes.LightGray, playerInfoX, playerInfoY, playerInfoWidth, playerInfoHeight);

            // Define font and brush for drawing text
            Font font = SystemFonts.DefaultFont;
            Brush brush = Brushes.Black;

            int offsetY = 10;

            // Draw player data for each player
            for (int i = 0; i < players.Length; i++)
            {
                // Draw player name
                string playerName = players[i].GetPlayerName().ToString();
                e.Graphics.DrawString($"Player {i + 1} Name: ", font, brush, playerInfoX + 10, playerInfoY + offsetY);
                e.Graphics.DrawString(playerName, font, brush, playerInfoX + 10, playerInfoY + offsetY + 20);

                offsetY += 35;
                //DRAW AMOUNT OF PLAYERS PUNISHMENT
                int punishmentPlayer = players[i].GetPunishment();
                e.Graphics.DrawString($"Player {i + 1} Amount Of Punishment: ", font, brush, playerInfoX + 10, playerInfoY + offsetY);
                e.Graphics.DrawString(punishmentPlayer.ToString(), font, brush, playerInfoX + 10, playerInfoY + offsetY + 20);
                // Draw player money amount
                double playerMoney = players[i].GetAmountOfMoney();
                e.Graphics.DrawString($"Player {i + 1} Money:", font, brush, playerInfoX + 10, playerInfoY + offsetY + 50);
                e.Graphics.DrawString(playerMoney.ToString(), font, brush, playerInfoX + 10, playerInfoY + offsetY + 70);

                //     Draw owned cities

                Tile[] OwnedCities = players[i].GetOwnedCityPlayer();
                e.Graphics.DrawString($"Player {i + 1} Owned Cities:", font, brush, playerInfoX + 10, playerInfoY + offsetY + 100);
                for (int j = 0; j < OwnedCities.Length; j++)
                {
                    if (OwnedCities[j] != null)
                    {

                        e.Graphics.DrawString(OwnedCities[j].getName(), font, brush, playerInfoX + 10, playerInfoY + offsetY + 120);
                        offsetY += 20;
                    }
                    // Increase the offset for the next city
                }


                offsetY += 130; // Increase the offset for the next player
            }
            //if (currentPlayerIndex == 2)
            //{
            //    currentPlayerIndex = 0;
            //}
            //currentPlayerIndex = currentPlayerIndex + 1;
            //currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;

            //return currentPlayerIndex;
        }
    }

}





