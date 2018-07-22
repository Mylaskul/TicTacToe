using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{

    class HeuristicAIPlayer : Player
    {

        public HeuristicAIPlayer(int id, bool turn) : base(id, turn){ }

        public override Tuple<int,int> PerformTurn(int[,] state)
        {
            List<Tuple<int, int>> cells = Game.GetFreeCells(state);
            SortedDictionary<int, List<Tuple<int,int>>> scores = new SortedDictionary<int, List<Tuple<int,int>>>();

            foreach (Tuple<int, int> c in cells)
            {
                int[,] newState = (int[,])state.Clone();
                newState[c.Item1, c.Item2] = id;
                int score = ScoreBoard(newState);
                if (scores.ContainsKey(score))
                    scores[score].Add(c);
                else
                {
                    List<Tuple<int,int>> newList = new List<Tuple<int,int>>
                    {
                        c
                    };
                    scores.Add(score, newList);
                }
            }

            Random rng = new Random();
            int maxScore = scores.Last().Key;
            Tuple<int,int> action = scores[maxScore][rng.Next(scores[maxScore].Count)];

            return action;
        }


        public int[,] MakeMove(int[,] board, Tuple<int, int> cell)
        {
            
            return board;
        }

        public int ScoreBoard(int[,] board)
        {
            int score = 0;

            for (int i = 0; i < 3; i++)
            {
                int countPlayer = 0;
                int countEnemy = 0;

                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == id)
                    {
                        score += 100;
                        countPlayer++;
                    }
                    else if (board[i, j] != 0)
                    {
                        score -= 100;
                        countEnemy++;
                    }
                }

                score = UpdateScore(score, countPlayer, countEnemy);
            }

            for (int j = 0; j < 3; j++)
            {

                int countPlayer = 0;
                int countEnemy = 0;

                for (int i = 0; j < 3; j++)
                {
                    if (board[i, j] == id)
                        countPlayer++;
                    else if (board[i, j] != 0)
                        countEnemy++;
                }

                score = UpdateScore(score, countPlayer, countEnemy);
            }

            int cP = 0;
            int cE = 0;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, i] == id)
                    cP++;
                else if (board[i, i] != 0)
                    cE++;
            }

            score = UpdateScore(score, cP, cE);

            int k = 2;
            cP = 0;
            cE = 0;

            for (int i = 0; i < 3; i++)
            {
                if (board[i, k] == id)
                    cP++;
                else if (board[i, k] != 0)
                    cE++;
                k--;
            }

            score = UpdateScore(score, cP, cE);

            return score;
        }

        public int UpdateScore(int score, int countPlayer, int countEnemy)
        {
            if (countPlayer == 3)
                score += 10000;
            else if (countPlayer == 2 && countEnemy == 0)
                score += 400;
            else if (countPlayer == 0 && countEnemy == 2)
                score -= 3000;
            else if (countEnemy == 3)
                score -= 10000;

            return score;
        }

        public override string ToString()
        {
            return "Heuristic AI Player";
        }


    }
}
