using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class MinMaxPlayer : Player
    {

        public MinMaxPlayer(int id, bool turn) : base(id, turn) {}

        public override Tuple<int,int> PerformTurn(int[,] state)
        {
            return GetBestAction(state);
        }


        public Tuple<int,int> GetBestAction(int[,] state)
        {
            Tuple<int,int> bestAction = null;
            int best = int.MinValue;
            List<Tuple<int, int>> cells = Game.GetFreeCells(state);
            foreach (Tuple<int, int> c in cells)
            {
                int[,] newState = (int[,])state.Clone();
                newState[c.Item1, c.Item2] = id;
                int score = SimulateMin(newState);
                if (bestAction == null || score > best)
                {
                    best = score;
                    bestAction = c;
                }
            }

            return bestAction;
        }


        public int SimulateMax(int[,] state)
        {
            int winner = Game.HasWon(state);
            List<Tuple<int, int>> cells = Game.GetFreeCells(state);
            if (winner == 0)
                return 0;
            else if (winner == -1)
            {
                int best = int.MinValue;
                foreach (Tuple<int, int> c in cells)
                {
                    int[,] newState = (int[,])state.Clone();
                    newState[c.Item1, c.Item2] = id;
                    int score = SimulateMin(newState);
                    if (score > best)
                        best = score;
                }

                return best;
            }
            
            else if (winner == id)
                return 10;
            else
                return -10;
        }

        public int SimulateMin(int[,] state)
        {
            int winner = Game.HasWon(state);
            List<Tuple<int, int>> cells = Game.GetFreeCells(state);
            if (winner == 0)
                return 0;
            else if (winner == -1)
            {
                int best = int.MaxValue;
                foreach (Tuple<int, int> c in cells)
                {
                    int[,] newState = (int[,])state.Clone();
                    if (id == 1)
                        newState[c.Item1, c.Item2] = 2;
                    else 
                        newState[c.Item1, c.Item2] = 1;
                    int score = SimulateMax(newState);
                    if (score < best)
                        best = score;
                }

                return best;
            }
            else if (winner == id)
                return 10;
            else
                return -10;
        }

        public override string ToString()
        {
            return "Min-Max Player";
        }

    }
}
