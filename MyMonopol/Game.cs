using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
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


        public Game()
        {
            players = new Player[4];
        }
        public Game(int howMany)
        {
            players = new Player[howMany];
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


            //currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
            //return currentPlayerIndex;



        }

        public void InitializePlayers()
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Player(new Point(0, 0), i+1, 0);
            }   
            //players[0] = new Player(new Point(0, 0), 1, 0);
            //players[1] = new Player(new Point(0, 0), 2, 0);
            //players[2] = new Player(new Point(0, 0), 3, 0);
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
            move = Throw_Dice();
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

            MessageBox.Show($" Index = {playerPlaceIndex} City =  {cityName}");
            bool ifOwned = CheckIfCityOwned(tile);
            if (!ifOwned)
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
        private bool CheckIfCityOwned(Tile tile)
        {
            for (int i = 0; i < gameOwnedCities.Length; i++)
            {
                if (tile == gameOwnedCities[i])
                {
                    for (int j = 0; j < players.Length; j++)
                    {
                        for (int k = 0; k < players[j].GetOwnedCityPlayer().Length; k++)
                        {
                            if (tile == players[j].GetOwnedCityPlayer()[k])
                            {
                                if (players[j] != players[currentPlayerIndex])
                                {
                                    double penaltyprice = tile.GetPrice() * 0.2;
                                    players[currentPlayerIndex].SetAmountOfMoney(-1 * penaltyprice);
                                    players[j].SetAmountOfMoney(penaltyprice);
                                    return true;
                                }

                            }
                        }
                    }
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
                }
                else
                {
                    gameOwnedCities[ownedCitiesHowMany + 1] = tile;
                }

                players[currentPlayerIndex].SetHowManyOwnedCities(players[currentPlayerIndex].GetAmountOfOwnedCities() + 1);

                //if (players[currentPlayerIndex].GetOwnedCityPlayer().Length == players[currentPlayerIndex].GetHowManyOwnedCities() + 1)
                //{
                //    string[] tempOwnedCityPlayer = new string[players[currentPlayerIndex].GetOwnedCityPlayer().Length + 10];
                //    for (int i = 0; i < tempOwnedCityPlayer.Length; i++)
                //    {
                //        //tempOwnedCityPlayer[i] = players[currentPlayerIndex].GetOwnedCityPlayer()[i];
                //    }
                //    tempOwnedCityPlayer[players[currentPlayerIndex].GetOwnedCityPlayer().Length + 1] = tile.ToString();
                //    players[currentPlayerIndex].SetOwnedCityPlayers(tempOwnedCityPlayer)
                //        ;
                //}

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
                e.Graphics.DrawString($"Player {i + 1} Name:", font, brush, playerInfoX + 10, playerInfoY + offsetY);
                e.Graphics.DrawString(playerName, font, brush, playerInfoX + 10, playerInfoY + offsetY + 20);

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


                offsetY += 150; // Increase the offset for the next player
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





