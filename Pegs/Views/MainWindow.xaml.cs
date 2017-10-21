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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Dictionary<PegGameType, PegBoard> GameBoards = new Dictionary<PegGameType, PegBoard> {
            {
                PegGameType.Cross,
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
                PegGameType.Triangle,
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
        
        private void NewGame(PegGameType gameType)
        {
            gameBoard = GameBoards[gameType].Clone() as PegBoard;

            controller = PegControllerFactory.CreateForGameType(gameType, gameBoard);
            
            controller.LoadView(MainView);
        }

        private void TriangleGameBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGame(PegGameType.Triangle);
        }

        private void CrossGameBtn_Click(object sender, RoutedEventArgs e)
        {
            NewGame(PegGameType.Cross);
        }
    }
}