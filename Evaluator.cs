using SharpNeat.Core;
using SharpNeat.Phenomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Evaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong evalCount;
        private bool stopConditionSatisfied;

        public ulong EvaluationCount
        {
            get { return evalCount; }
        }

        public bool StopConditionSatisfied
        {
            get { return stopConditionSatisfied; }
        }

        public FitnessInfo Evaluate(IBlackBox phenome)
        {
            double fitness = 0;
            int winner;
            Player optimalPlayer = new MinMaxPlayer(2, false);
            Player randomPlayer = new RandomPlayer(2, false);
            Player neatPlayer = new NEATPlayer(1, true, phenome);

            for (int i = 0; i < 50; i++)
            {
                winner = Game.Play(neatPlayer, randomPlayer, true);
                fitness += GetScore(winner, neatPlayer.id);
            }

            randomPlayer.id = 1;
            randomPlayer.turn = true;
            neatPlayer.id = 2;
            neatPlayer.turn = false;

            for (int i = 0; i < 50; i++)
            {
                winner = Game.Play(randomPlayer, neatPlayer, true);
                fitness += GetScore(winner, neatPlayer.id);
            }

            neatPlayer.id = 1;
            neatPlayer.turn = true;

            winner = Game.Play(neatPlayer, optimalPlayer, true);
            fitness += GetScore(winner, neatPlayer.id);

            optimalPlayer.id = 1;
            optimalPlayer.turn = true;
            neatPlayer.id = 2;
            neatPlayer.turn = false;

            winner = Game.Play(optimalPlayer, neatPlayer, true);
            fitness += GetScore(winner, neatPlayer.id);

            evalCount++;

            if (fitness >= 1002)
                stopConditionSatisfied = true;

            return new FitnessInfo(fitness, fitness);

        }

        public void Reset() {}

        public double GetScore(int winner, int id)
        {
            if (winner == 0)
                return 1;
            if (winner == id)
                return 10;
            else
                return 0;
        }
    }
}
