using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pegs.Views
{
    abstract class GameView
    {
        protected PegState[,] _gameState = null;
        protected Controllers.GameController _controller = null;

        public virtual void RenderTo(ContentPresenter view) {}
        public abstract void Render();
        
        public void SetController(Controllers.GameController controller)
        {
            _controller = controller;
        }

        public void DropZone_PegSwapped(object sender, IntPoint from, IntPoint to)
        {
            _controller.SwapPegs(from, to);
        }

        public void SetGameState(PegState[,] gameState)
        {
            _gameState = gameState;
        }
    }

    class RectangleGameView: GameView {
        private Grid _gridView = null;

        override public void RenderTo(ContentPresenter presenter)
        {

            _gridView = new Grid();
            presenter.Content = _gridView;
        }

        override public void Render()
        {
            // Clear board
            _gridView.RowDefinitions.Clear();
            _gridView.ColumnDefinitions.Clear();
            _gridView.Children.Clear();

            // Get Properties
            int Height = _gameState.GetLength(0);
            int Width = _gameState.GetLength(1);

            // Add Rows
            for (int i = 0; i < Height; i++)
            {
                RowDefinition rd = new RowDefinition();
                _gridView.RowDefinitions.Add(new RowDefinition());
            }

            // Add Columns
            for (int i = 0; i < Width; i++)
            {
                _gridView.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Add Pegs
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    switch (_gameState[i, j])
                    {
                        case PegState.Invalid:
                            break;

                        case PegState.Peg:
                            Peg peg = new Peg();
                            Grid.SetRow(peg, i);
                            Grid.SetColumn(peg, j);
                            _gridView.Children.Add(peg);
                            break;

                        case PegState.NoPeg:
                            PegDropZone dropZone = new PegDropZone();
                            Grid.SetRow(dropZone, i);
                            Grid.SetColumn(dropZone, j);
                            _gridView.Children.Add(dropZone);
                            dropZone.PegSwapped += DropZone_PegSwapped;
                            break;
                    }
                }
            }
        }
    }

    class TriangleGameView : GameView
    {
        public override void Render()
        {
            
        }
    }
}
