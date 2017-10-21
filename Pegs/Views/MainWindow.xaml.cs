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

        private enum GameType { Triangle, Cross }

        private Dictionary<GameType, PegBoard> GameBoards = new Dictionary<GameType, PegBoard> {
            {
                GameType.Cross,
                new RectanglePegBoard(new int[,] {
                    {0, 0, 1, 1, 1, 0, 0},
                    {0, 0, 1, 1, 1, 0, 0},
                    {1, 1, 1, 1, 1, 1, 1},
                    {1, 1, 1, 2, 1, 1, 1},
                    {1, 1, 1, 1, 1, 1, 1},
                    {0, 0, 1, 1, 1, 0, 0},
                    {0, 0, 1, 1, 1, 0, 0},
                })
            },
            {
                GameType.Triangle,
                new TrianglePegBoard(new int[,] {
                    {0, 0, 2, 0, 0},
                      {0, 1, 1, 0, 0},
                    {0, 1, 1, 1, 0},
                      {1, 1, 1, 1, 0},
                    {1, 1, 1, 1, 1},
                })
            }
        };

        private PegBoard gameBoard = null;
        private PegController controller = null;

        static Random random = new Random();
        
        private void NewGame(GameType gameType)
        {
            gameBoard = GameBoards[gameType].Clone() as PegBoard;
            
            if (gameType == GameType.Cross)
            {
                controller = new RectanglePegController(new Views.RectanglePegView(), gameBoard);
            } else if (gameType == GameType.Triangle)
            {
                controller = new TrianglePegController(new Views.TrianglePegView(), gameBoard);
            }
            
            controller.LoadView(MainView);
        }

        private void TriangleGameBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGame(GameType.Triangle);
        }

        private void CrossGameBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGame(GameType.Cross);
        }
    }
}