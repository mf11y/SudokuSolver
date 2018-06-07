using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Game : Form
    {
        private SudokuDrawer drawer;
        private Board board;
        private BufferedPanel pan;

        public Game()
        {
            InitializeComponent();

            pan = bufferedPanel1;
            board = new Board(pan);
            drawer = new SudokuDrawer(ref board);

            this.KeyPreview = true;
        }

        Point clickedBox;

        bool highlight = false;

        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if(highlight)
            {
                Rectangle rect = new Rectangle(new Point(clickedBox.X, clickedBox.Y), new Size(40, 40));
                e.Graphics.FillRectangle(Brushes.Wheat, rect);
                highlight = false;
            }

            drawer.DrawBoard(e.Graphics);
        }


        bool boardHasBeenClicked = false;

        private static readonly int squareSize = 40;

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e)
        {
            boardHasBeenClicked = true;

            int clickedBoxCoordsX = (bufferedPanel1.PointToClient(Cursor.Position).X / squareSize) * squareSize;
            int clickedBoxCoordsY = (bufferedPanel1.PointToClient(Cursor.Position).Y / squareSize) * squareSize;
            clickedBox = new Point(clickedBoxCoordsX, clickedBoxCoordsY);  

            highlight = true;

            this.bufferedPanel1.Invalidate();
        }

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (boardHasBeenClicked)
            {
                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    string num = keyPressed.ToString();
                    board.insertIntoBoard(new Point(clickedBox.Y / squareSize, clickedBox.X / squareSize), Int32.Parse(num));
                }
            }
            boardHasBeenClicked = false;

            this.bufferedPanel1.Invalidate();
        }

        async private void solve_button(object sender, EventArgs e)
        {
            bufferedPanel1.Enabled = false;
            button1.Enabled = false;
            await Task.Run(() => board.solve2());
            bufferedPanel1.Enabled = true;
            button1.Enabled = true;
        }
    }
}
