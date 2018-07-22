using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    abstract class Player
    {

        public int id;
        public bool turn;

        public Player(int id, bool turn)
        {
            this.id = id;
            this.turn = turn;
        }

        abstract public Tuple<int,int> PerformTurn(int[,] state);

    }
}
