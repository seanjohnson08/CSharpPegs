using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

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

        private int[][,] GameBoards = new int[][,] {
            new int[,] {
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 2, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {0, 0, 1, 1, 1, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
            }
        };

        private void DrawBoard(int[,] board)
        {
            int Height = board.GetLength(0);
            int Width = board.GetLength(1);

            // Add Rows
            for (int i = 0; i < Height; i++)
            {
                this.MainView.RowDefinitions.Add(new RowDefinition());
            }
            
            // Add Columns
            for (int i = 0; i < Width; i++)
            {
                this.MainView.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Add Pegs
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (board[i,j] > 0)
                    {
                        Button btn = new Peg();
                        Grid.SetRow(btn, i);
                        Grid.SetColumn(btn, j);
                        btn.Content = board[i, j];
                        this.MainView.Children.Add(btn);

                        btn.Click += PegClick;
                        btn.Background = board[i,j] == 1 ? Brushes.Blue : Brushes.DarkGray;
                    }
                }
            }
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("New Game Clicked");

            int[,] GameBoard = GameBoards[0];

            DrawBoard(GameBoard);
        }

        void PegClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int x = Grid.GetRow(btn);
            int y = Grid.GetColumn(btn);
            
        }
    }
}