using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pegs.Views;
using Pegs.Models;

namespace Pegs.Controllers
{
    abstract class PegController
    {
        protected PegBoardView _view;
        protected PegBoard _model;

        public PegController(PegBoardView view, PegBoard model) {
            _view = view;
            _model = model;
            _view.SetController(this);
        }

        public void LoadView(System.Windows.Controls.ContentPresenter panel)
        {
            _view.SetModel(_model);
            _view.RenderTo(panel);
            _view.Render();
        }

        public abstract bool IsValidMove(IntPoint from, IntPoint to);
        
        public IntPoint GetMidPoint(IntPoint from, IntPoint to)
        {
            return new IntPoint((from.X + to.X) / 2, (from.Y + to.Y) / 2);
        }

        public abstract void SwapPegs(IntPoint from, IntPoint to);
    }

    class RectanglePegController: PegController
    {
        public RectanglePegController(PegBoardView view, PegBoard model) : base(view, model) { }

        public override bool IsValidMove(IntPoint from, IntPoint to)
        {
            return (
                    // Move horizontally
                    Math.Abs(from.X - to.X) == 2 && from.Y == to.Y ||
                    // Move vertically
                    Math.Abs(from.Y - to.Y) == 2 && from.X == to.X
                ) &&
                    // Check to make sure there's a peg in between
                    _model.GetPeg(GetMidPoint(from, to)) == PegState.Peg;
        }

        public override void SwapPegs(IntPoint from, IntPoint to)
        {
            if (!IsValidMove(from, to))
            {
                return;
            }

            // Empty where the peg is leaving from
            _model.SetPeg(from, PegState.NoPeg);

            // Remove the peg that was jumped
            _model.SetPeg(GetMidPoint(from, to), PegState.NoPeg);

            // Add the peg to its final destination
            _model.SetPeg(to, PegState.Peg);

            // Redraw the game board
            _view.Render();
        }
    }

    class TrianglePegController: PegController
    {
        // Board looks like:
        // 0 1 2 3 4 5
        //  0 1 2 3 4
        // 0 1 2 3 4 5
        //  0 1 2 3 4

        public TrianglePegController(PegBoardView view, PegBoard model) : base(view, model) { }

        private IntPoint GetMidPoint(IntPoint from, IntPoint to)
        {
            // Moving Vertically
            if (from.Y != to.Y)
            {
                // Because each row is offset, I need to take this into account when addressing the X coordinate of the row
                // being jumped.
                bool rowIsEven = from.Y % 2 == 0;

                // Moving Left
                if (from.X > to.X)
                {
                    return new IntPoint(from.X - (rowIsEven ? 1 : 0), (from.Y + to.Y) / 2);
                } else

                // Moving Right
                {
                    return new IntPoint(from.X + (rowIsEven ? 0 : 1), (from.Y + to.Y) / 2);
                }

            // Moving Horizontally
            } else
            {
                return new IntPoint((from.X + to.X) / 2, (from.Y + to.Y) / 2);
            }
        }

        public override bool IsValidMove(IntPoint from, IntPoint to)
        {
            return (
                // Move Vertically...
                Math.Abs(from.Y - to.Y) == 2 && (
                    // Left
                    from.X - to.X == 1 && _model.GetPeg(GetMidPoint(from, to)) == PegState.Peg ||
                    // Right
                    from.X - to.X == -1 && _model.GetPeg(GetMidPoint(from, to)) == PegState.Peg
                ) ||

                // Move Horizontally
                Math.Abs(from.X - to.X) == 2 && from.Y == to.Y && _model.GetPeg(GetMidPoint(from, to)) == PegState.Peg
            );
        }

        public override void SwapPegs(IntPoint from, IntPoint to)
        {
            if (!IsValidMove(from, to))
            {
                return;
            }

            // Empty where the peg is leaving from
            _model.SetPeg(from, PegState.NoPeg);

            // Remove the peg that was jumped
            _model.SetPeg(GetMidPoint(from, to), PegState.NoPeg);

            // Add the peg to its final destination
            _model.SetPeg(to, PegState.Peg);

            // Redraw the game board
            _view.Render();
        }
    }
}
