using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    class BoardManagement
    {
        Graphics gr;
        Panel activePanel;
        Font myFont = new Font("Arial", 10);

        Board board = new Board();

        const int squareSize = 40;
        const int xOffset = 14;
        const int yOffset = 12;

        public BoardManagement(Panel space)
        {
            activePanel = space;
            gr = activePanel.CreateGraphics();
        }

        public void DrawBoard()
        {
            Pen myPen = new Pen(Brushes.Black, 1);

            int x = 0;
            int y = 0;
            int NumLines = 10;
            int linelength = squareSize * 9;

            for (int i = 0; i < NumLines; i++)
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

        }

        bool boardHasBeenClicked = false;
        bool inputReceptive = false;
        bool switchedBoxMidInput = false;

        Point clickedBox;
        Point historyClicked;

        public void ManagePanelClick()
        {
            boardHasBeenClicked = true;
            switchedBoxMidInput = false;

            int clickedBoxCoordsX = (activePanel.PointToClient(Cursor.Position).X / squareSize) * squareSize;
            int clickedBoxCoordsY = (activePanel.PointToClient(Cursor.Position).Y / squareSize) * squareSize;

            clickedBox = new Point(clickedBoxCoordsX, clickedBoxCoordsY);

            EraseBox(clickedBox);
            DrawInBox("?", clickedBox);

            if ((clickedBox != historyClicked) && inputReceptive)
                switchedBoxMidInput = true;

            if (switchedBoxMidInput)
                EraseBox(historyClicked);

            inputReceptive = true;

            historyClicked = new Point(clickedBoxCoordsX, clickedBoxCoordsY);
        }

        public void ManageKeyBoardClick(KeyEventArgs e)
        {
            if (boardHasBeenClicked)
            {
                EraseBox(clickedBox);

                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    string num = keyPressed.ToString();
                    if (board.insertIntoBoard(clickedBox.Y / squareSize, clickedBox.X / squareSize, Int32.Parse(num)))
                        DrawInBox(num, clickedBox);
                }
                inputReceptive = false;
            }
            boardHasBeenClicked = false;
        }

        private void EraseBox(Point boxCoords)
        {
            Rectangle rect = new Rectangle(GetMiddleOfBox(boxCoords), new Size(20, 20));
            gr.FillRectangle(Brushes.White, rect);
        }
        private void DrawInBox(string write, Point boxCoords) => gr.DrawString(write, myFont, Brushes.Black, GetMiddleOfBox(boxCoords));

        private Point GetMiddleOfBox(Point boxCoords) => new Point(boxCoords.X + xOffset, boxCoords.Y + yOffset);
    }
}
