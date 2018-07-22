using SharpNeat.Phenomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TicTacToe
{
    class NEATPlayer : Player
    {

        public IBlackBox ai;

        public NEATPlayer(int id, bool turn, IBlackBox ai) : base(id, turn)
        {
            this.ai = ai;
        }

        public override Tuple<int,int> PerformTurn(int[,] state)
        {
            ai.ResetState();
            ISignalArray input = ai.InputSignalArray;
            for (int i = 0; i < 9; i++)
            {
                input[i] = state[i / 3, i % 3];
            }

            ai.Activate();

            Tuple<int, int> action = null;
            double max = double.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state[i, j] != 0)
                        continue;

                    double score = ai.OutputSignalArray[i * 3 + j];

                    if (action == null || max < score)
                    {
                        action = Tuple.Create(i, j);
                        max = score;
                    }
                }
            }

            return action;
        }


        public override string ToString()
        {
            return "NEAT Player";
        }

    }
}
