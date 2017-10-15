using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pegs.Views;
using Pegs.Models;

namespace Pegs.Controllers
{
    abstract class GameController
    {
        protected GameView _view;
        protected GameBoard _model;

        public GameController(GameView view, GameBoard model) {
            _view = view;
            _model = model;
        }

        public void LoadView(System.Windows.Controls.Panel panel)
        {
            _view.RenderTo(panel);
            _view.SetGameState(_model.GameState);
            _view.Render();
        }

        public abstract bool IsValidMove(IntPoint from, IntPoint to);

        public void SwapPegs(IntPoint from, IntPoint to)
        {
            if (!IsValidMove(from, to))
            {
                return;
            }

            // Empty where the peg is leaving from
            _model.SetPeg(from, PegState.NoPeg);

            // Remove the peg that was jumped
            _model.SetPeg(new IntPoint((from.X + to.X) / 2, (from.Y + to.Y) / 2), PegState.NoPeg);

            // Add the peg to its final destination
            _model.SetPeg(to, PegState.Peg);

            // Redraw the game board
            _view.Render();
        }
    }

    class RectangleGameController: GameController
    {
        public RectangleGameController(GameView view, GameBoard model) : base(view, model) { }

        public override bool IsValidMove(IntPoint from, IntPoint to)
        {
            return (
                    // Move horizontally
                    Math.Abs(from.X - to.X) == 2 && from.Y == to.Y ||
                    // Move vertically
                    Math.Abs(from.Y - to.Y) == 2 && from.X == to.X
                ) &&
                    // Check to make sure there's a peg in between
                    _model.GetPeg(new IntPoint((from.X + to.X) / 2, (from.Y + to.Y) / 2)) == PegState.Peg;
        }
    }

    class TriangleGameController: GameController
    {
        public TriangleGameController(GameView view, GameBoard model) : base(view, model) { }

        public override bool IsValidMove(IntPoint from, IntPoint to)
        {
            throw new NotImplementedException();
        }
    }
}
