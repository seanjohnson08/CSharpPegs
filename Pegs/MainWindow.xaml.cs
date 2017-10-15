﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Pegs
{
    enum PegState { None, Peg, NoPeg };

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

        private int[,] gameState = null;

        private void DrawBoard(int[,] board)
        {
            // Clear board
            this.MainView.RowDefinitions.Clear();
            this.MainView.ColumnDefinitions.Clear();
            this.MainView.Children.Clear();

            // Get Properties
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
                    switch (board[i,j])
                    {
                        case 0:
                            break;
                        case 1:
                            Peg peg = new Peg();
                            Grid.SetRow(peg, i);
                            Grid.SetColumn(peg, j);
                            this.MainView.Children.Add(peg);
                            break;
                        case 2:
                            PegDropZone dropZone = new PegDropZone();
                            Grid.SetRow(dropZone, i);
                            Grid.SetColumn(dropZone, j);
                            this.MainView.Children.Add(dropZone);
                            dropZone.PegSwapped += DropZone_PegSwapped;
                            break;
                    }
                }
            }
        }

        private bool IsValidMove(IntPoint from, IntPoint to)
        {
            return (
                    // Move horizontally
                    Math.Abs(from.X - to.X) == 2 && from.Y == to.Y ||
                    // Move vertically
                    Math.Abs(from.Y - to.Y) == 2 && from.X == to.X
                ) &&
                    // Check to make sure there's a peg in between
                    (PegState)gameState[(from.X + to.X) / 2, (from.Y + to.Y) / 2] == PegState.Peg;
        }

        private void DropZone_PegSwapped(object sender, IntPoint from, IntPoint to)
        {
            if (!IsValidMove(from, to))
            {
                return;
            }

            // Empty where the peg is leaving from
            gameState[from.X, from.Y] = (int)PegState.NoPeg;

            // Remove the peg that was jumped
            gameState[(from.X + to.X) / 2, (from.Y + to.Y) / 2] = (int)PegState.NoPeg;

            // Add the peg to its final destination
            gameState[to.X, to.Y] = (int)PegState.Peg;

            // Redraw the game board
            DrawBoard(gameState);
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("New Game Clicked");

            int[,] GameBoard = GameBoards[0];
            gameState = (int[,])GameBoard.Clone();
            DrawBoard(gameState);
        }
    }
}