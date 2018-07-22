using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToe
{
    class HumanPlayer : Player
    {

        public bool waiting;
        Tuple<int, int> action;
        EventWaitHandle clickHandle;

        public HumanPlayer(int id, bool turn) : base(id, turn)
        {
            this.waiting = false;
            this.action = null;
            this.clickHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            for (int i = 0; i < 9; i++)
            {
                Program.gui.cells[i].Click += new EventHandler(Click_Cell);
            }
        }

        public override Tuple<int,int> PerformTurn(int[,] state)
        {
            waiting = true;
            clickHandle.WaitOne();
            return action;
        }

        private void Click_Cell(object sender, EventArgs e)
        {
            Label cell = (Label)sender;
            if (waiting && cell.Text == "")
            {
                for (int i = 0; i < 9; i++)
                {
                    if (cell == Program.gui.cells[i])
                    {
                        action = Tuple.Create(i / 3, i % 3);
                        break;
                    }
                }
                waiting = false;
                clickHandle.Set();
            }
        }


        public override string ToString()
        {
            return "Human Player";
        }
    }
}
