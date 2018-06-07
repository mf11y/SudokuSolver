using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    class SudokuDrawer
    {
        Board board;

        public SudokuDrawer(ref Board b) => board = b;

        private static readonly int squareSize = 40;
        private readonly int xOffset = 14;
        private static readonly int yOffset = 12;
        private static readonly Font myFont = new Font("Arial", 10);

        public void DrawBoard(Graphics e)
        {
            Pen myPen = new Pen(Brushes.Black, 1);

            int NumLines = 10;
            int linelength = squareSize * 9;

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

            List<List<int>> copy = new List<List<int>>(board.GetCopy());

            for (int i = 0; i < copy.Count(); i++)
                for (int j = 0; j < copy[i].Count(); j++)
                    if (copy[i][j] > 0 && copy[i][j] < 10)
                        DrawInBox(e,copy[i][j].ToString(), new Point(j * squareSize, i * squareSize));
        }

        public void DrawInBox(Graphics e,string write, Point boxCoords) => e.DrawString(write, myFont, Brushes.Black, GetMiddleOfBox(boxCoords));


        public Point GetMiddleOfBox(Point boxCoords) => new Point(boxCoords.X + xOffset, boxCoords.Y + yOffset);

        public void EraseBox(Graphics e, Point boxCoords)
        {
            board.delete(boxCoords);
            DrawBoard(e);
        }
    }
}
