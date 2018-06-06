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
        Point historyClicked;

        bool erase = false;
        bool eraseHistory = false;
        bool draw;
        bool solve;

        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (erase)
            {
                drawer.EraseBox(e.Graphics, clickedBox);
                erase = false;
            }
            else if (eraseHistory)
            {
                drawer.EraseBox(e.Graphics, historyClicked);
                eraseHistory = false;
            }
            else
                drawer.DrawBoard(e.Graphics);
        }


        bool boardHasBeenClicked = false;
        bool inputReceptive = false;
        bool switchedBoxMidInput = false;

        private static readonly int squareSize = 40;

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e)
        {
            boardHasBeenClicked = true;
            switchedBoxMidInput = false;

            int clickedBoxCoordsX = (bufferedPanel1.PointToClient(Cursor.Position).X / squareSize) * squareSize;
            int clickedBoxCoordsY = (bufferedPanel1.PointToClient(Cursor.Position).Y / squareSize) * squareSize;
            //MessageBox.Show(clickedBoxCoordsX.ToString());

            clickedBox = new Point(clickedBoxCoordsX, clickedBoxCoordsY);
            erase = true;

            this.bufferedPanel1.Invalidate();

            //drawer.EraseBox(clickedBox);
            //drawer.DrawInBox("?", clickedBox);

            if ((clickedBox != historyClicked) && inputReceptive)
                switchedBoxMidInput = true;

            if (switchedBoxMidInput)
                //drawer.EraseBox(historyClicked);

                inputReceptive = true;

            historyClicked = new Point(clickedBoxCoordsX, clickedBoxCoordsY);

        }

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (boardHasBeenClicked)
            {
                //drawer.EraseBox(clickedBox);

                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    string num = keyPressed.ToString();
                    board.insertIntoBoard(new Point(clickedBox.Y / squareSize, clickedBox.X / squareSize), Int32.Parse(num));
                }
                inputReceptive = false;
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
