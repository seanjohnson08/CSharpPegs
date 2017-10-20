using Pegs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pegs.Views
{
    abstract class PegBoardView
    {
        protected PegBoard _pegBoard = null;
        protected Controllers.PegController _controller = null;
        protected ContentPresenter _view;

        public virtual void RenderTo(ContentPresenter view) {
            _view = view;
        }
        public abstract void Render();
        
        public void SetController(Controllers.PegController controller)
        {
            _controller = controller;
        }

        public void DropZone_PegSwapped(object sender, IntPoint from, IntPoint to)
        {
            _controller.SwapPegs(from, to);
        }

        public void SetModel(PegBoard pegBoard)
        {
            _pegBoard = pegBoard;
        }
    }

    class RectanglePegView: PegBoardView {

        override public void Render()
        {
            Grid gridView = new Grid();

            // Clear board
            gridView.RowDefinitions.Clear();
            gridView.ColumnDefinitions.Clear();
            gridView.Children.Clear();

            // Get Properties
            int Height = _pegBoard.GameState.GetLength(0);
            int Width = _pegBoard.GameState.GetLength(1);

            // Add Rows
            for (int i = 0; i < Height; i++)
            {
                gridView.RowDefinitions.Add(new RowDefinition());
            }

            // Add Columns
            for (int i = 0; i < Width; i++)
            {
                gridView.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Add Pegs
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    switch (_pegBoard.GetPeg(new IntPoint(x, y)))
                    {
                        case PegState.Invalid:
                            break;

                        case PegState.Peg:
                            Peg peg = new Peg(new IntPoint(x, y));
                            Grid.SetRow(peg, y);
                            Grid.SetColumn(peg, x);
                            gridView.Children.Add(peg);
                            break;

                        case PegState.NoPeg:
                            PegDropZone dropZone = new PegDropZone(new IntPoint(x, y));
                            Grid.SetRow(dropZone, y);
                            Grid.SetColumn(dropZone, x);
                            gridView.Children.Add(dropZone);
                            dropZone.PegSwapped += DropZone_PegSwapped;
                            break;
                    }
                }
            }

            // Finally, set the view to the rendered grid
            _view.Content = gridView;
        }
    }

    class TrianglePegView : PegBoardView
    {
        public override void Render()
        {
            int Width = _pegBoard.GameState.GetLength(0);
            int Height = _pegBoard.GameState.GetLength(1);
            // Calculates the size of pegs given the rendering context's dimensions and the number of rows/columns required
            int PEG_SIZE = (int)Math.Min(_view.ActualHeight / Width, _view.ActualWidth / Height);

            StackPanel stackColumns = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            for (int y = 0; y < Height; y++)
            {
                // Create a new row and add it onto the stack
                StackPanel row = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Height = PEG_SIZE
                };
                stackColumns.Children.Add(row);
                int spacing = 0;

                // Each row is offset from the previous row in order to have triangles, ex:
                //   o
                //  o o
                // o o o
                if (y % 2 == 1)
                {
                    Thickness margin = row.Margin;
                    margin.Left = PEG_SIZE / 2;
                    row.Margin = margin;
                }

                // Add Pegs
                for (int x = 0; x < Width; x++)
                {
                    Control cell = null;
                    switch (_pegBoard.GetPeg(new IntPoint(x, y)))
                    {
                        case PegState.Invalid:
                            spacing++;
                            break;

                        case PegState.Peg:
                            cell = new Peg(new IntPoint(x, y));
                            break;

                        case PegState.NoPeg:
                            PegDropZone pegDropZone = new PegDropZone(new IntPoint(x, y));
                            pegDropZone.PegSwapped += DropZone_PegSwapped;
                            cell = pegDropZone as Control;
                            break;
                    }

                    if (cell != null)
                    {
                        cell.Width = PEG_SIZE;

                        // In order to keep spacing correct for runs of PegState.Invalid, spacing is accumulated
                        // and used as the margin here.
                        Thickness margin = cell.Margin;
                        margin.Left = spacing * PEG_SIZE;
                        cell.Margin = margin;
                        spacing = 0;

                        // Finally add the cell into the row
                        row.Children.Add(cell);
                    }
                }
            }

            // Finally, set the view to the stack
            _view.Content = stackColumns;
        }
    }
}
