/*
 * 
 * Class Responsible for managing form events
 * 
*/

using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Game : Form
    {
        //Drawer is responsible for drawing the current state of the board
        private SudokuDrawer drawer;

        //Board class contains the data for the grid as well as logic to solve the puzzle
        private Board board;

        public Game()
        {
            InitializeComponent();
            KeyPreview = true;
            
            board = new Board(GameBoardPanel);
            drawer = new SudokuDrawer(ref board);
        }

        //Holds the box that was clicked. X, Y starting from bottom left
        private Point clickedBox;

        //Set to T if user is using board. F when board isn't in use
        private bool boardInUse = false;

        //Each square size is 40 pixels across. Needed to find location of clicked box or to index into board data
        private const int squareSize = 40;

        //When mouse clicked inside panel, get location.
        private void P1_Click(object sender, EventArgs e)
        {
            boardInUse = true;

            int clickedBoxX = (int)Math.Floor((double)(GameBoardPanel.PointToClient(Cursor.Position).X / squareSize));
            int clickedBoxY = (int)Math.Floor((double)(GameBoardPanel.PointToClient(Cursor.Position).Y / squareSize));
            clickedBox = new Point(clickedBoxX, clickedBoxY);

            GameBoardPanel.Invalidate();
        }

        //Check if user has entered valid input. Changes state of boardinuse when done
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (boardInUse)
            {
                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    ClearButton.Enabled = true;
                    string num = keyPressed.ToString();
                    board.insertIntoBoard(new Point(clickedBox.Y, clickedBox.X), Int32.Parse(num));
                }
            }
            boardInUse = false;

            this.GameBoardPanel.Invalidate();
        }

        //Responsible for calling drawer to paint to handle painting sudoku grid
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics drawSurface = e.Graphics;

            drawer.DrawBoard(drawSurface);

            if (boardInUse)
                highLightBox(drawSurface);

        }

        //Used by paint to highlight active box
        private void highLightBox(Graphics e)
        {
            Rectangle rect = new Rectangle(new Point(clickedBox.X * squareSize, clickedBox.Y * squareSize), new Size(40, 40));
            e.FillRectangle(Brushes.White, rect);
        }

        //Handles what happens when solve button is clicked. Disabled buttons
        private void SolveButton_Click(object sender, EventArgs e)
        {
            boardInUse = false;
            GameBoardPanel.Enabled = false;
            Solvebutton.Enabled = false;
            CancelButton_.Enabled = true;

            SolveAsync();
        }

        //Used to cancel algorithm
        CancellationTokenSource cts;

        //Button used to change cts to let it know to cancel operation
        private void CancelButton_Click(object sender, EventArgs e) => cts.Cancel();

        //T if check box for follow algorithm is checked. F if not
        bool followAlgorithm = false;

        //Async task to handle solving puzzle. Enables buttons when done
        async private void SolveAsync()
        {
            cts = new CancellationTokenSource();
            bool solved = true;

            try
            {
                await Task.Run(() => board.solve2(followAlgorithm, cts.Token));
            }
            catch (OperationCanceledException)
            {
                board.clear();
                GameBoardPanel.Enabled = true;
                Solvebutton.Enabled = true;
                ClearButton.Enabled = false;
                solved = false;
            }

            if (solved)
                ClearButton.Enabled = true;

            CancelButton_.Enabled = false;
            GameBoardPanel.Enabled = true;
            GameBoardPanel.Invalidate();

            cts.Dispose();
        }

        //Checkbox if user wants to follow the algorith along
        private void FollowAlongCheck_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                followAlgorithm = true;
            else
                followAlgorithm = false;
        }

        //Erase board. Resets board
        private void ClearButton_Click(object sender, EventArgs e)
        {
            board.clear();
            boardInUse = false;
            Solvebutton.Enabled = true;
            ClearButton.Enabled = false;
            GameBoardPanel.Invalidate();
        }
    }
}
