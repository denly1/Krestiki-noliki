using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Button[] buttons;
        private bool playerTurn;
        private bool gameOver;
        private Random random;

        public MainWindow()
        {
            InitializeComponent();

            buttons = new Button[] { button0, button1, button2, button3, button4, button5, button6, button7, button8 };
            playerTurn = true;
            gameOver = false;
            random = new Random();

            ResetGame();
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }

            playerTurn = true;
            gameOver = false;
            statusLabel.Content = "Ходят крестики";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (!gameOver && button.Content.ToString() == "")
            {
                if (playerTurn)
                {
                    button.Content = "X";
                    statusLabel.Content = "Ходят крестики";
                }
                else
                {
                    button.Content = "O";
                    statusLabel.Content = "Ходят нолики";
                }

                button.IsEnabled = false;

                if (CheckForWin())
                {
                    if (playerTurn)
                    {
                        statusLabel.Content = "Крестики выиграли!";
                    }
                    else
                    {
                        statusLabel.Content = "Нолики выиграли!";
                    }

                    gameOver = true;
                }
                else if (CheckForDraw())
                {
                    statusLabel.Content = "Ничья!";
                    gameOver = true;
                }

                playerTurn = !playerTurn;

                if (!gameOver && !playerTurn)
                {
                    RobotMove();
                }
            }
        }

        private bool CheckForWin()
        {
            string[] board = buttons.Select(b => b.Content.ToString()).ToArray();

            // Check rows
            for (int i = 0; i <= 6; i += 3)
            {
                if (board[i] != "" && board[i] == board[i + 1] && board[i + 1] == board[i + 2])
                {
                    return true;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (board[i] != "" && board[i] == board[i + 3] && board[i + 3] == board[i + 6])
                {
                    return true;
                }
            }

            if ((board[0] != "" && board[0] == board[4] && board[4] == board[8]) ||
                (board[2] != "" && board[2] == board[4] && board[4] == board[6]))
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            return !buttons.Any(b => b.Content.ToString() == "");
        }

        private void RobotMove()
        {
            List<Button> emptyButtons = buttons.Where(b => b.Content.ToString() == "").ToList();

            if (emptyButtons.Count > 0)
            {
                int randomIndex = random.Next(emptyButtons.Count);
                Button randomButton = emptyButtons[randomIndex];
                randomButton.Content = "O";
                randomButton.IsEnabled = false;

                if (CheckForWin())
                {
                    statusLabel.Content = "Нолики выиграли!";
                    gameOver = true;
                }
                else if (CheckForDraw())
                {
                    statusLabel.Content = "Ничья!";
                    gameOver = true;
                }

                playerTurn = !playerTurn;
                statusLabel.Content = "Ход крестиков";
            }
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}