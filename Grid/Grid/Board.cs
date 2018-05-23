using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Sudoku
{
    class Board
    {
        private List<List<int>> gameBoard = new List<List<int>>();
        private List<Point> emptyCells= new List<Point>();

        public Board()
        {
            for (int i = 0; i < 9; i++)
                gameBoard.Add(new List<int>());

            foreach (List<int> templist in gameBoard)
                for (int i = 0; i < 9; i++)
                    templist.Add(0);
        }

        public bool insertIntoBoard(int i1, int i2, int val)
        {
            if (isValidNum(i1, i2, val))
            {
                gameBoard[i1][i2] = val;
                return true;
            }
            else
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

        public bool isValidNum(int i1, int i2, int val)
        {
            List<int> verticalCheck = new List<int>();
            List<int> quadrantCheck= new List<int>();

            for (int i = 0; i < gameBoard.Count();i++)
                verticalCheck.Add(gameBoard[i][i2]);

            int x = (i1 / 3) * 3;
            int y = (i2 / 3) * 3;

            for(int i = x; i < x + 3; i++)
                for(int j = y; j < y + 3; j++)
                    quadrantCheck.Add(gameBoard[i][j]);

            if (verticalCheck.Contains(val) || gameBoard[i1].Contains(val) || quadrantCheck.Contains(val))
                return false;
            else
                return true;
        }

        private void FindEmptyCells()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    if (gameBoard[i][j] == 0)
                        emptyCells.Add(new Point(i, j));
        }

        bool isFilled()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    if (gameBoard[i][j] == 0)
                        return false;

            return true;
        }

        public bool solve(int position)
        {
            if(isFilled())
                return true;

            for(int i = 1; i < 10; i++)
            {
                if (isValidNum(emptyCells[position].X, emptyCells[position].Y, i))
                {
                    gameBoard[emptyCells[position].X][emptyCells[position].Y] = i;
                    if (solve(position + 1))
                        return true;
                }
            }

            gameBoard[emptyCells[position].X][emptyCells[position].Y] = 0;
            return false;
        }

        public void solve2()
        {
            FindEmptyCells();
            //Saves which empty cell was just filled
            Stack<int> savedMoves = new Stack<int>();

            for(int potentialCandidate = 1, currentEmptyCell = 0; true;)
            {
                for(; potentialCandidate < 10; potentialCandidate++)
                {
                    if(isValidNum(emptyCells[currentEmptyCell].X, emptyCells[currentEmptyCell].Y, potentialCandidate))
                    {
                        gameBoard[emptyCells[currentEmptyCell].X][emptyCells[currentEmptyCell].Y] = potentialCandidate;
                        savedMoves.Push(potentialCandidate);
                        currentEmptyCell++;
                        potentialCandidate = 0;

                        if (isFilled())
                            return;
                    }
                }
                currentEmptyCell--;
                potentialCandidate = savedMoves.Peek() + 1;
                gameBoard[emptyCells[currentEmptyCell].X][emptyCells[currentEmptyCell].Y] = 0;

                savedMoves.Pop();
            }
        }
    }
}
