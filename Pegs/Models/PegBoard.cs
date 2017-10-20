using System;

namespace Pegs.Models
{
    public class PegBoard : ICloneable
    {
        public PegState[,] GameState { get; private set; }

        public PegBoard(int[,] board)
        {
            GameState = new PegState[board.GetLength(0), board.GetLength(1)];
            for (int y = 0; y < board.GetLength(0); y++)
            {
                for (int x = 0; x < board.GetLength(1); x++)
                {
                    GameState[y, x] = (PegState)board[y, x];
                }
            }
        }

        public void SetPeg(IntPoint peg, PegState state)
        {
            GameState[peg.Y, peg.X] = state;
        }

        public PegState GetPeg(IntPoint peg)
        {
            return GameState[peg.Y, peg.X];
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class RectanglePegBoard : PegBoard
    {
        public RectanglePegBoard(int[,] board) : base(board) { }
    }

    public class TrianglePegBoard : PegBoard
    {
        public TrianglePegBoard(int[,] board) : base(board) { }
    }
}

