using System;

namespace Training.MinerCore
{
    /// <summary>
    /// Class with BL for game board of Miner game.
    /// </summary>
    public class GameBoard
    {
        private readonly bool[,] board;
        private readonly int width, height, mineCount;

        private readonly int[] shiftX = { -1, -1, -1, 0, 1, 1,  1,  0 };
        private readonly int[] shiftY = { -1,  0,  1, 1, 1, 0, -1, -1 };

        private readonly int desiredOpenCount;

        private int openCount;
        
        public delegate void OnWin();
        public delegate void OnLose();
        public delegate void OnOpen(int x, int y, int neighbors);

        public event OnWin Win;
        public event OnLose Lose;
        public event OnOpen Open;

        public GameBoard(int width, int height, int mineCount)
        {
            this.mineCount = mineCount;
            this.height = height;
            this.width = width;

            desiredOpenCount = height * width - mineCount;
            
            board = new bool[height,width];
            Init();
        }

        /// <summary>
        /// Init randomly board with mines.
        /// </summary>
        private void Init()
        {
            var range = Range.GetCombination(0, width * height, mineCount);
            foreach (var el in range)
            {
                var x = el % width;
                var y = el / width;
                board[x, y] = true;
            }
        }

        /// <summary>
        /// Open the cell.
        /// 
        /// If game ending condition is satisfied - raise the correct event.
        /// If given cell has zero neighbors with mines - open all neighbors.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void OpenCell(int x, int y)
        {
            if (!IsPositionValid(x, y))
            {
                throw new ArgumentException("Invalid coordinate provided");
            }
            if (board[x, y])
            {
                DoLose();
                return;
            }
            ++openCount;
            if (openCount == desiredOpenCount)
            {
                DoWin();
            }
            var cnt = GetNeighborsCount(x, y);
            DoOpen(x, y, cnt);
            if (cnt == 0)
            {
                OpenNeighbors(x, y);
            }
        }

        /// <summary>
        /// Raise Open event.
        /// </summary>
        private void DoOpen(int x, int y, int count)
        {
            if (Open != null)
            {
                Open(x, y, count);
            }
        }

        /// <summary>
        /// Raise Lose event.
        /// </summary>
        private void DoLose()
        {
            if (Lose != null)
            {
                Lose();
            }
        }

        /// <summary>
        /// Raise Win event.
        /// </summary>
        private void DoWin()
        {
            if (Win != null)
            {
                Win();
            }
        }

        /// <summary>
        /// Calculate count of mines in neighbors cells.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int GetNeighborsCount(int x, int y)
        {
            var cnt = 0;
            for (var i = 0; i < shiftX.Length; ++i)
            {
                var nx = x + shiftX[i];
                var ny = y + shiftY[i];
                if (!IsPositionValid(nx, ny))
                {
                    continue;
                }
                if (board[nx, ny])
                {
                    ++cnt;
                }
            }
            return cnt;
        }

        /// <summary>
        /// Open all neighbors cells for given one.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void OpenNeighbors(int x, int y)
        {
            for (var i = 0; i < shiftX.Length; ++i)
            {
                // Cells are not sharing the edge
                if (Math.Abs(shiftX[i] + shiftY[i]) != 1)
                {
                    continue;
                }
                var nx = x + shiftX[i];
                var ny = y + shiftY[i];
                if (!IsPositionValid(nx, ny))
                {
                    continue;
                }
                OpenCell(x, y);
            }
        }

        /// <summary>
        /// Check if position is inside the GameBoard.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool IsPositionValid(int x, int y)
        {
            return x >= 0 && x < height && y >= 0 && y < width;
        }
    }
}
