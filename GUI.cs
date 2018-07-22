using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class GUI
    {

        public Label[] cells;
        public Label winner;
        public Button start;
        public Button train;
        public Button reset;
        public ComboBox p1;
        public ComboBox p2;
        public Form form;

        public GUI()
        {
            
            form = new Form
            {
                Text = "Tic Tac Toe"
            };
            form.SetDesktopBounds(800, 600, 900, 639);

            cells = new Label[9];
            for (int i = 0; i < 9; i++)
            {
                Label cell = new Label
                {
                    Text = "",
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Aquamarine,
                    BorderStyle = BorderStyle.FixedSingle,
                    Bounds = new Rectangle((i * 200) % 600, (i / 3) * 200, 200, 200),
                    Font = new Font("Arial", 30)
                };
                form.Controls.Add(cell);
                cells[i] = cell;
            }

            start = new Button() {
                Bounds = new Rectangle(620, 20, 120, 50),
                BackColor = Color.LightGray,
                Text = "Start"
            };
            start.Click += new EventHandler(Click_Start);
            form.Controls.Add(start);

            train = new Button()
            {
                Bounds = new Rectangle(740, 20, 120, 50),
                BackColor = Color.LightGray,
                Text = "Train"
            };
            train.Click += new EventHandler(Click_Train);
            form.Controls.Add(train);

            winner = new Label() {
                Bounds = new Rectangle(620, 490, 240, 100),
                BackColor = Color.LightGray,
                Text = "Winner: ",
                Font = new Font("Arial", 24, FontStyle.Bold)
            };
            form.Controls.Add(winner);

            p1 = new ComboBox
            {
                Bounds = new Rectangle(620, 260, 120, 50),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            p1.Items.Add("Random Player");
            p1.Items.Add("Heuristic Player");
            p1.Items.Add("Min-Max Player");
            p1.Items.Add("Human Player");
            //p1.Items.Add("NEAT Player");
            p1.SelectedIndex = 0;
            form.Controls.Add(p1);

            p2 = new ComboBox
            {
                Bounds = new Rectangle(620, 310, 120, 50),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            p2.Items.Add("Random Player");
            p2.Items.Add("Heuristic Player");
            p2.Items.Add("Min-Max Player");
            p2.Items.Add("Human Player");
            //p2.Items.Add("NEAT Player");
            p2.SelectedIndex = 0;
            form.Controls.Add(p2);


        }

        private void Click_Start(object sender, EventArgs e)
        {
            String box1 = (String)p1.SelectedItem;
            String box2 = (String)p2.SelectedItem;
            Player player1 = null;
            Player player2 = null;

            switch (box1)
            {
                case "Random Player":
                    player1 = new RandomPlayer(1, true);
                    break;
                case "Heuristic Player":
                    player1 = new HeuristicAIPlayer(1, true);
                    break;
                case "Min-Max Player":
                    player1 = new MinMaxPlayer(1, true);
                    break;
                case "Human Player":
                    player1 = new HumanPlayer(1, true);
                    break;
            }

            switch (box2)
            {
                case "Random Player":
                    player2 = new RandomPlayer(2, false);
                    break;
                case "Heuristic Player":
                    player2 = new HeuristicAIPlayer(2, false);
                    break;
                case "Min-Max Player":
                    player2 = new MinMaxPlayer(2, false);
                    break;
                case "Human Player":
                    player2 = new HumanPlayer(2, false);
                    break;
            }

            Clear();

            Task.Factory.StartNew(() => Game.Play(player1, player2, false));
        }

        private void Click_Train(object sender, EventArgs e)
        {
             Task.Factory.StartNew(Game.Train);
        }

        private void Clear()
        {
            for (int i = 0; i < 9; i++)
            {
                cells[i].Text = "";
            }
            winner.Text = "Winner: ";
        }

        public void Update(int[,] board)
        {
            int counter = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                        cells[counter].Text = "";
                    else if (board[i, j] == 1)
                        cells[counter].Text = "X";
                    else if (board[i, j] == 2)
                        cells[counter].Text = "O";
                    counter++;
                }
            }
        }

        public void ShowWinner(int result)
        {
            if (result == 0)
                winner.Text = "Winner: Draw";
            else 
                winner.Text = "Winner: Player " + result;
            
        }

    }
}
