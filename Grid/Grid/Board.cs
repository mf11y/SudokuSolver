/*
 * 
 * Class Responsible for board logic and holding data
 * 
*/

using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading;

namespace Sudoku
{
    class Board
    {
        //Where the numbers are held
        private List<List<int>> gameBoard = new List<List<int>>();

        //Holds the coordinates of all the empty cells
        private List<Point> emptyCells= new List<Point>();

        //Access to panel to call Invalidate(); Invalidate is thread safe
        BufferedPanel pan;

        //Creates empty 9x9 Grid and initialized pan to panel in mainboard
        public Board(BufferedPanel p)
        {
            for (int i = 0; i < 9; i++)
                gameBoard.Add(new List<int>());

            foreach (List<int> templist in gameBoard)
                for (int i = 0; i < 9; i++)
                    templist.Add(0);

            pan = p;
        }

        //Zeros out board. 0 = empty
        public void clear()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    gameBoard[i][j] = 0;
        }

        //Responsible for inputting data in board
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

        //Checks to see if incoming number conflicts with anything on board. Checks verical/horizontal/quadrant
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

        //Finds all the empty cells
        private void FindEmptyCells()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    if (gameBoard[i][j] == 0)
                        emptyCells.Add(new Point(i, j));
        }

        //checks to see if board has been filled
        bool isFilled()
        {
            for (int i = 0; i < gameBoard.Count(); i++)
                for (int j = 0; j < gameBoard[i].Count(); j++)
                    if (gameBoard[i][j] == 0)
                        return false;

            return true;
        }

        //Backtrack algorithm to solve puzzle
        public int solve2(bool follow, CancellationToken ct)
        {
            FindEmptyCells();
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
                            Thread.Sleep(50);
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

        //returns a copy of the data in board
        public List<List<int>> GetCopy() => gameBoard;
    }
}
