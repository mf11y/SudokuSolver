using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Board
    {
        private List<List<int>> gameBoard = new List<List<int>>();

        public Board()
        {
            for (int i = 0; i < 9; i++)
            {
                gameBoard.Add(new List<int>());
            }

            foreach (List<int> templist in gameBoard)
            {
                for (int i = 0; i < 9; i++)
                {
                    templist.Add(6);
                }
            }
        }

        public void insertIntoBoard(int x, int y, int val)
        {
            gameBoard[y][x] = val;
        }

        public string printBoard()
        {
            string start = "";
            foreach(List<int> templist in gameBoard)
            {
                foreach(int x in templist)
                {
                    start += x.ToString();
                }

                start += "\n";
            }

            return start;
        }

    }
}
