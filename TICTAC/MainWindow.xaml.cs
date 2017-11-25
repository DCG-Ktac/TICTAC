using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TICTAC
{
    /// <summary>
    /// TICTAC
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean currentPlayer;
        byte player0Score = 0; 
        byte player1Score = 0;

        Button[] buttons;
        
        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9 };
            UpdateScore();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Button temp = sender as Button;
            if (temp.Content.ToString() == "")
                temp.Content = (currentPlayer == false ? "O" : "X");
            else
                return;

            CheckWin();
        }

        private void CheckWin()
        {
            //Check for all win conditions
            if (Line(btn1.Content.ToString(), btn2.Content.ToString(), btn3.Content.ToString()) != "" ||
               Line(btn4.Content.ToString(), btn5.Content.ToString(), btn6.Content.ToString()) != "" ||
               Line(btn7.Content.ToString(), btn8.Content.ToString(), btn9.Content.ToString()) != "" ||
               Line(btn1.Content.ToString(), btn4.Content.ToString(), btn7.Content.ToString()) != "" ||
               Line(btn2.Content.ToString(), btn5.Content.ToString(), btn8.Content.ToString()) != "" ||
               Line(btn3.Content.ToString(), btn6.Content.ToString(), btn9.Content.ToString()) != "" ||
               Line(btn1.Content.ToString(), btn5.Content.ToString(), btn9.Content.ToString()) != "" ||
               Line(btn3.Content.ToString(), btn5.Content.ToString(), btn7.Content.ToString()) != "")
            {
                //If there was a winner: increment the score of the current player
                if (currentPlayer)
                    player1Score++;
                else
                    player0Score++;

                //and update the labels
                UpdateScore();

               
                
                //Then show a box
                MessageBox.Show((currentPlayer ? "Player 2 " : "Player 1 ") + "has won");

                //then clear the board
                BoardClear();

                //then set the current player to 0. 
                currentPlayer = false;
            }
            //If no-one has won yet then just swap players
            else
            {
                //but make sure there's still empty spaces left
                CheckForDraw();
                
                currentPlayer = !currentPlayer;
            }     
        }

        string Line(string a, string b, string c)
        {
            if (a == b && b == c && a != "")
                return a;
            else
                return "";
        }

        void UpdateScore() {
            lblPlayer0Score.Content = "Player 1: " + player0Score;
            lblPlayer1Score.Content = "Player 2: " + player1Score;
        }

        void CheckForDraw() {
            foreach (Button btn in buttons)
            {
                if (btn.Content.ToString() == "")
                    return;
            }

            //If all spaces are full then reset:
            MessageBox.Show("Draw lol");
            BoardClear();
            currentPlayer = true; //so that when the rest of CheckWin() runs it'll swap this back to false ready for a new game.
        }

        void BoardClear() {
            foreach (Button btn in buttons)
                btn.Content = "";
        }

        void Reset(object sender, RoutedEventArgs e)
        {
            BoardClear();
            currentPlayer = false;
        }
    }
}
