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
        private static readonly int squareSize = 40;
        Board board;
        SudokuDrawer drawer;
        Panel activePanel;

        public BoardManagement(Panel pan)
        {
            board = new Board();
            drawer = new SudokuDrawer(ref board, ref pan);         

            board.ConnectDrawer(ref drawer);
            activePanel = pan;
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

            drawer.EraseBox(clickedBox);
            drawer.DrawInBox("?", clickedBox);

            if ((clickedBox != historyClicked) && inputReceptive)
                switchedBoxMidInput = true;

            if (switchedBoxMidInput)
                drawer.EraseBox(historyClicked);

            inputReceptive = true;

            historyClicked = new Point(clickedBoxCoordsX, clickedBoxCoordsY);
        }

        public void ManageKeyBoardClick(KeyEventArgs e)
        {
            if (boardHasBeenClicked)
            {
                drawer.EraseBox(clickedBox);

                char keyPressed = (char)e.KeyValue;

                if (keyPressed >= '0' && keyPressed <= '9')
                {
                    string num = keyPressed.ToString();
                    if (board.insertIntoBoard(clickedBox.Y / squareSize, clickedBox.X / squareSize, Int32.Parse(num)))
                        drawer.DrawInBox(num, clickedBox);
                }
                inputReceptive = false;
            }
            boardHasBeenClicked = false;
        }

        public void DrawBoard()
        {
            drawer.DrawBoard();
        }

        public int Solve()
        {
            board.solve2();
            //await Task.Run(() => board.solve2());
            drawer.DrawBoard();

            return 1;
        }

    }
}
