using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMonopol
{
    public partial class Monopoly : Form
    {
        private Game game;
        private int tileSizeWidth = 40;
        private int tileSize = 70;
        private int howMany = 0;
        //private Graphics g;
         private bool goLeft, goRight, goUp, goDown;
        private int playerSpeed = 8;
        private int speed = 12;
        private object button1_1;
        private int playersAmount;

        public Monopoly()
        {
            game = new Game();
            ClientSize = new Size(700, 700);



            BackColor = Color.Aqua;

            Paint += MainForm_Paint;

            InitializeComponent();
            Resize += MainForm_Resize;
            game.InitializePlayers();
            End_Game_Button.Click += End_Game_Button_Click;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Board.CreateTiles(e.Graphics, ClientSize);

            tileSize = Math.Min(ClientSize.Width / 10, ClientSize.Height / 10);

            game.showPlayersData(ClientSize, e);

            game.createPlayers(e.Graphics, tileSize);
            //Player[] players = game.GetPlayers();
            //for (int i = 0; i < 2; i++)
            //{
            //    players[i].CreatePlayer(e.Graphics, tileSize, i);
            //}
           // game.createPlayersData(ClientSize, e);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            tileSizeWidth = ClientRectangle.Width;
            Invalidate();
        }


        private void Start_Button_Click(object sender, EventArgs e)
        {

          //  Start_Button.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            playersAmount = int.Parse(textBox1.Text);
            MessageBox.Show(playersAmount.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            game.RollAndPLay(ClientSize);
            Refresh();
            Invalidate();
        }

        private void End_Game_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to end the game?", "End Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Game Over!");
                // For example: Close the form, show a game-over screen, etc.
                Close(); // Close the form
            }

        }
    }
}
