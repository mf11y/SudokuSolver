using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;

namespace Sudoku
{
    class Board
    {
        private List<List<int>> gameBoard = new List<List<int>>();
        private List<Point> emptyCells= new List<Point>();
        BufferedPanel pan;

        public Board(BufferedPanel p)
        {
            for (int i = 0; i < 9; i++)
                gameBoard.Add(new List<int>());

            foreach (List<int> templist in gameBoard)
                for (int i = 0; i < 9; i++)
                    templist.Add(0);

            pan = p;
        }


        public bool insertIntoBoard(Point coords, int val)
        {
            if (isValidNum(coords, val))
            {
                gameBoard[coords.X][coords.Y] = val;
                return true;
            }
            else
                return false;
        }

        public void clear()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    gameBoard[i][j] = 0;
        }

        public bool isValidNum(Point coords, int val)
        {
            List<int> verticalCheck = new List<int>();
            List<int> quadrantCheck= new List<int>();

            for (int i = 0; i < gameBoard.Count();i++)
                verticalCheck.Add(gameBoard[i][coords.Y]);

            int x = (coords.X / 3) * 3;
            int y = (coords.Y / 3) * 3;

            for(int i = x; i < x + 3; i++)
                for(int j = y; j < y + 3; j++)
                    quadrantCheck.Add(gameBoard[i][j]);

            if (verticalCheck.Contains(val) || gameBoard[coords.X].Contains(val) || quadrantCheck.Contains(val))
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

        public int solve2(bool follow, CancellationToken ct)
        {
            FindEmptyCells();
            //Saves which empty cell was just filled
            Stack<int> savedMoves = new Stack<int>();

            for(int potentialCandidate = 1, currentEmptyCell = 0; true;)
            {
                for(; potentialCandidate < 10; potentialCandidate++)
                {

                    if(isValidNum(new Point(emptyCells[currentEmptyCell].X, emptyCells[currentEmptyCell].Y), potentialCandidate))
                    {
                        ct.ThrowIfCancellationRequested();
                        gameBoard[emptyCells[currentEmptyCell].X][emptyCells[currentEmptyCell].Y] = potentialCandidate;
                        savedMoves.Push(potentialCandidate);
                        currentEmptyCell++;
                        potentialCandidate = 0;

                        if (follow)
                        {
                            pan.Invalidate();
                            Thread.Sleep(10);
                        }

                        if (isFilled())
                            return 1;
                    }
                }
                ct.ThrowIfCancellationRequested();
                currentEmptyCell--;
                potentialCandidate = savedMoves.Peek() + 1;
                gameBoard[emptyCells[currentEmptyCell].X][emptyCells[currentEmptyCell].Y] = 0;
                savedMoves.Pop();

                if(follow)
                    pan.Invalidate();
            }
        }

        public List<List<int>> GetCopy() => gameBoard;
    }
}
