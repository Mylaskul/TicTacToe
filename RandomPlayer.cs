using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class RandomPlayer : Player
    {
        public RandomPlayer(int id, bool turn) : base(id, turn) {}

        public override Tuple<int,int> PerformTurn(int[,] state)
        {
            Random rng = new Random();
            int ranX = rng.Next(3);
            int ranY = rng.Next(3);
            while (state[ranX, ranY] != 0)
            {
                ranX = rng.Next(3);
                ranY = rng.Next(3);
            }
            state[ranX, ranY] = id;
            return Tuple.Create(ranX, ranY);
        }


        public override string ToString()
        {
            return "Random Player";
        }

    }
}
