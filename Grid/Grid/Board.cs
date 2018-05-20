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

        public bool insertIntoBoard(int Horizontal, int Vertical, int val)
        {
            if (isValidNum(Horizontal, Vertical, val))
            {
                gameBoard[Vertical][Horizontal] = val;
                return true;
            }

            return false;
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

        public bool isValidNum(int Horizontal, int Vertical, int val)
        {
            List<int> temp = new List<int>();
            List<int> temp2= new List<int>();

            for (int i = 0; i < gameBoard.Count();i++)
                temp.Add(gameBoard[i][Horizontal]);

            int x = (Vertical / 3) * 3;
            int y = (Horizontal / 3) * 3;

            for(int i = x; i < x + 3; i++)
            {
                for(int j = y; j < y + 3; j++)
                {
                    temp2.Add(gameBoard[i][j]);
                }
            }

            if (temp.Contains(val) || gameBoard[Vertical].Contains(val) || temp2.Contains(val))
                return false;
            else
                return true;
        }

    }
}
