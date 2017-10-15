using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Pegs.Models;
using Pegs.Controllers;

namespace Pegs
{
    public enum PegState { Invalid, Peg, NoPeg };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private GameBoard[] GameBoards = new GameBoard[] {
            new SquareGameBoard(new int[,] {
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 2, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
            }),
            new TriangleGameBoard(new int[,] {
                {0, 0, 2, 0, 0},
                  {0, 1, 1, 0, 0},
                {0, 1, 1, 1, 0},
                  {1, 1, 1, 1, 0},
                {1, 1, 1, 1, 1},
            })
        };

        private GameBoard gameBoard = null;
        private GameController controller = null;

        static Random random = new Random();

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            // Choose a board to use
            gameBoard = GameBoards[0].Clone() as GameBoard;

            if (gameBoard.GetType() == typeof(TriangleGameBoard))
            {
                controller = new TriangleGameController(new Views.TriangleGameView(), gameBoard);
            } else {
                controller = new RectangleGameController(new Views.RectangleGameView(), gameBoard);
            }

            // Draw the board
            controller.LoadView(this.MainView);
        }
    }
}