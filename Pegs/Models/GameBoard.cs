using System;

namespace Pegs.Models
{
    public class GameBoard : ICloneable
    {
        public PegState[,] GameState { get; private set; }

        public GameBoard(int[,] board)
        {
            GameState = new PegState[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    GameState[i, j] = (PegState)board[i, j];
                }
            }
        }

        public void SetPeg(IntPoint peg, PegState state)
        {
            GameState[peg.X, peg.Y] = state;
        }

        public PegState GetPeg(IntPoint peg)
        {
            return GameState[peg.X, peg.Y];
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class RectangleGameBoard : GameBoard
    {
        public RectangleGameBoard(int[,] board) : base(board) { }
    }

    public class TriangleGameBoard : GameBoard
    {
        public TriangleGameBoard(int[,] board) : base(board) { }
    }
}

