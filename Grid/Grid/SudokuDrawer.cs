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
        Graphics gr;
        private Panel activePanel;
        Board board;

        public SudokuDrawer(ref Board b, ref Panel pan)
        {
            activePanel = pan;
            gr = pan.CreateGraphics();
            board = b;
        }

        private static readonly int squareSize = 40;
        private readonly int xOffset = 14;
        private static readonly int yOffset = 12;
        private static readonly Font myFont = new Font("Arial", 10);

        public void DrawBoard()
        {
            gr.Clear(Color.White);
            Pen myPen = new Pen(Brushes.Black, 1);

            int NumLines = 10;
            int linelength = squareSize * 9;

            for (int i = 0, x = 0, y = 0; i < NumLines; i++)
            {
                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                gr.DrawLine(myPen, x, 0, x, linelength);
                x += squareSize;

                gr.DrawLine(myPen, 0, y, linelength, y);
                y += squareSize;
            }

            List<List<int>> copy = new List<List<int>>(board.GetCopy());

            for (int i = 0; i < copy.Count(); i++)
                for (int j = 0; j < copy[i].Count(); j++)
                    if (copy[i][j] != 0)
                        DrawInBox(copy[i][j].ToString(), new Point(j * squareSize, i * squareSize));
        }

        public void DrawInBox(string write, Point boxCoords) => gr.DrawString(write, myFont, Brushes.Black, GetMiddleOfBox(boxCoords));

        public Point GetMiddleOfBox(Point boxCoords) => new Point(boxCoords.X + xOffset, boxCoords.Y + yOffset);

        public void EraseBox(Point boxCoords)
        {
            Rectangle rect = new Rectangle(GetMiddleOfBox(boxCoords), new Size(20, 20));
            gr.FillRectangle(Brushes.White, rect);
        }
    }
}
