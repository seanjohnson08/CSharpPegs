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

        private PegBoard[] GameBoards = new PegBoard[] {
            new RectanglePegBoard(new int[,] {
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 2, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
            }),
            new TrianglePegBoard(new int[,] {
                {0, 0, 2, 0, 0},
                  {0, 1, 1, 0, 0},
                {0, 1, 1, 1, 0},
                  {1, 1, 1, 1, 0},
                {1, 1, 1, 1, 1},
            })
        };

        private PegBoard gameBoard = null;
        private PegController controller = null;

        static Random random = new Random();

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            // Choose a board to use
            gameBoard = GameBoards[random.Next(2)].Clone() as PegBoard;

            if (gameBoard.GetType() == typeof(TrianglePegBoard))
            {
                controller = new TrianglePegController(new Views.TrianglePegView(), gameBoard);
            } else {
                controller = new RectanglePegController(new Views.RectanglePegView(), gameBoard);
            }

            // Draw the board
            controller.LoadView(this.MainView);
        }
    }
}