using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMonopoly
{
    public partial class Monopoly : Form
    {
        private Game game;
        private int tileSizeWidth = 40;
        private int tileSize = 70;

        public Monopoly()
        {
            game = new Game(0);

            ClientSize = new Size(700, 700);

            //background color of the form
            BackColor = Color.Aqua;

            //responsible for drawing the board and players
            Paint += MainForm_Paint;

            //initializing all the controls on the board
            InitializeComponent();

            //expand the tiles size by the board size expanding
            Resize += MainForm_Resize;

            //initialize players
            game.InitializePlayers();

            //if you wanna end the game
            End_Game_Button.Click += End_Game_Button_Click;

            //handling number of players (2-4)
            textBox1.TextChanged += textBox1_TextChanged;
        }

        //painting the board and the players.
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Board board = new Board();
            board.CreateTiles(e.Graphics, ClientSize);

            tileSize = Math.Min(ClientSize.Width / 10, ClientSize.Height / 10);

            game.showPlayersData(ClientSize, e);

            game.createPlayers(e.Graphics, tileSize);
        }


        // expand the tiles size by the board size expanding
        private void MainForm_Resize(object sender, EventArgs e)
        {
            tileSizeWidth = ClientRectangle.Width;

            //redraw ui
            Invalidate();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int numPlayer = int.Parse(textBox1.Text);
            if (numPlayer <= 1 || numPlayer > 4)
            {
                MessageBox.Show("please write a valid num of players!");
                return;
            }

            game = new Game(numPlayer); // יצירת המשחק רק כאשר הערך בתיבת הטקסט תקין
            game.InitializePlayers();
            MessageBox.Show($"There are {numPlayer} players. Click twice on OK, and then click on Play.");
            playersAmount_Button.Visible = false;
            textBox1.Visible = false;
            this.Focus(); // להעביר את הפוקוס מהתיבת הטקסט לטופס כדי שלא יהיה ריענון נוסף
            Refresh(); // ריענון הטופס כדי להציג את השחקנים על הלוח
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.RollAndPLay(ClientSize);
            Refresh();
        }

        private void End_Game_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to end the game?", "End Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show($"Game Over!");
                Close(); // Close the form
            }
        }

        private void playersAmount_Button_Click(object sender, EventArgs e)
        {

        }
    }
}
