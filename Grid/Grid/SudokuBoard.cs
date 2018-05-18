using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Game : Form
    {
        int clickedBoxCoordsX;
        int clickedBoxCoordsY;
        int historyPrevClickX = -1;
        int historyPrevClickY = -1; 

        bool boardHasBeenClicked = false;
        bool inputReceptive = false;
        bool switchedBoxMidInput = false;

        Board board = new Board();

        const int xOffset = 14;
        const int yOffset = 12;

        const int squareSize = 40;

        Graphics gr;
        Font myFont = new Font("Arial", 10);


        public Game()
        {
            InitializeComponent();
            gr = panel1.CreateGraphics();
        }

        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Brushes.Black, 1);
            int NumLines = 10;

            int x = 0;
            int y = 0;

            int linelength = squareSize * 9;

            for (int i = 0; i < NumLines; i++)
            {
                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                gr.DrawLine(myPen, x, y, x, linelength);
                x += squareSize;
            }

            x = 0;

            for (int i = 0; i < NumLines; i++)
            {

                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                gr.DrawLine(myPen, x, y, linelength, y);
                y += squareSize;
            }

            
        }

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e)
        {
            boardHasBeenClicked = true;
            switchedBoxMidInput = false;

            clickedBoxCoordsX = (panel1.PointToClient(Cursor.Position).X / squareSize) * squareSize;
            clickedBoxCoordsY = (panel1.PointToClient(Cursor.Position).Y / squareSize) * squareSize;

            EraseBox();
            DrawInBox("?");

            if ((historyPrevClickX != clickedBoxCoordsX || historyPrevClickY != clickedBoxCoordsY) && inputReceptive)
                switchedBoxMidInput = true;

            if (switchedBoxMidInput)
                EraseBox();

            inputReceptive = true;
            historyPrevClickX = clickedBoxCoordsX;
            historyPrevClickY = clickedBoxCoordsY;
        }

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (boardHasBeenClicked)
            {
                EraseBox();

                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    string num = keyPressed.ToString();
                    board.insertIntoBoard(clickedBoxCoordsX / 40 , clickedBoxCoordsY / 40, Int32.Parse(num));
                    DrawInBox(num);
                }
                inputReceptive = false;
                MessageBox.Show(board.printBoard());
            }

            boardHasBeenClicked = false;
        }

        private void EraseBox()
        {
            Rectangle rect = new Rectangle();
            rect.Size = new Size(20, 20);

            if (switchedBoxMidInput)
                rect.Location = new Point(GetMiddleOfBox("X"), GetMiddleOfBox("Y"));
            else
                rect.Location = new Point(GetMiddleOfBox("X"), GetMiddleOfBox("Y"));

            gr.FillRectangle(Brushes.White, rect);

        }

        private void DrawInBox(string write)
        {
            gr.DrawString(write, myFont, Brushes.Black, GetMiddleOfBox("X"), GetMiddleOfBox("Y"));
        }

        private int GetMiddleOfBox(string axis)
        {
            if (axis == "X")
                if (switchedBoxMidInput)
                    return historyPrevClickX + xOffset;
                else
                    return clickedBoxCoordsX + xOffset;
            else
                if (switchedBoxMidInput)
                    return historyPrevClickY + yOffset;
                else
                    return clickedBoxCoordsY + yOffset;
        }
    }
}
