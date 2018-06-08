/*
 * 
 * Class Responsible for drawing into sudoku board
 * 
*/

using System.Collections.Generic;
using System.Linq;
using System.Drawing;


namespace Sudoku
{
    class SudokuDrawer
    {
        Board board;

        //ctor to get copy of gameboard
        public SudokuDrawer(ref Board b) => board = b;

        //Draws the grid then the numbers
        public void DrawBoard(Graphics e)
        {
            DrawGrid(e);
            DrawNumbers(e);
        }

        //Variables used to draw grid
        private static readonly Pen myPen = new Pen(Brushes.Black, 1);
        private const int NumLines = 10;
        private const int squareSize = 40;
        private const int linelength = squareSize * 9;

        private void DrawGrid(Graphics e)
        {
            for (int i = 0, x = 0, y = 0; i < NumLines; i++)
            {
                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                e.DrawLine(myPen, x, 0, x, linelength);
                x += squareSize;

                e.DrawLine(myPen, 0, y, linelength, y);
                y += squareSize;
            }
        }

        //Draws the numbers into board based on data in board
        private void DrawNumbers(Graphics e)
        {
            List<List<int>> copy = new List<List<int>>(board.GetCopy());

            for (int i = 0; i < copy.Count(); i++)
                for (int j = 0; j < copy[i].Count(); j++)
                    if (copy[i][j] > 0 && copy[i][j] < 10)
                        DrawInBox(e, copy[i][j].ToString(), new Point(j * squareSize, i * squareSize));
        }

        //font used to draw in box
        private static readonly Font myFont = new Font("Arial", 10);

        //Draws individual numbers into box
        private void DrawInBox(Graphics e,string write, Point boxCoords) => e.DrawString(write, myFont, Brushes.Black, GetMiddleOfBox(boxCoords));

        //Used to get middle of box to draw numbers
        private const int xOffset = 14;
        private const int yOffset = 12;

        //Gets middle of box
        private Point GetMiddleOfBox(Point boxCoords) => new Point(boxCoords.X + xOffset, boxCoords.Y + yOffset);
    }
}
