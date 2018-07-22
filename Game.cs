using log4net.Config;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe
{

    class Game
    {
        static NeatEvolutionAlgorithm<NeatGenome> ea;
        const string CHAMPION_FILE = "tictactoe_champion.xml";

        public static int Play(Player p1, Player p2, bool training)
        {
            int[,] board = { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            int hasWon = -1;
            while (hasWon == -1)
            {
                if (!training)
                    Thread.Sleep(Program.delay);
                Player currPlayer = p1;
                if (p2.turn)
                    currPlayer = p2;
                Tuple<int,int> action = currPlayer.PerformTurn(board);
                board[action.Item1, action.Item2] = currPlayer.id;
                if (p1.turn)
                {
                    p1.turn = false;
                    p2.turn = true;
                }
                else
                {
                    p1.turn = true;
                    p2.turn = false;
                }
                if (!training)
                    Program.gui.form.Invoke((MethodInvoker)(() => Program.gui.Update(board)));
                hasWon = HasWon(board);
                        
            }
            if (!training)
                Program.gui.form.Invoke((MethodInvoker)(() => Program.gui.ShowWinner(hasWon)));
            return hasWon;
        }

        public static int HasWon(int[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2] && board[i, 0] != 0)
                    return board[i,0];
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] == board[1, i] && board[0, i] == board[2, i] && board[0, i] != 0)
                    return board[0, i];
            }
            if (board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2] && board[0, 0] != 0)
                return board[0, 0];
            if (board[0, 2] == board[1, 1] && board[0, 2] == board[2, 0] && board[0, 2] != 0)
                return board[0, 2];
            if (GetFreeCells(board).Count == 0)
                return 0;
            return -1;
        }

        public static List<Tuple<int, int>> GetFreeCells(int[,] board)
        {
            List<Tuple<int, int>> cells = new List<Tuple<int, int>>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                        cells.Add(Tuple.Create(i, j));
                }
            }

            return cells;
        }

        public static void Train()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.properties"));
            TicTacToeExperiment experiment = new TicTacToeExperiment();
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("../../tictactoe.config.xml");
            experiment.Initialize("TicTacToe", xmlConfig.DocumentElement);
            ea = experiment.CreateEvolutionAlgorithm();
            ea.UpdateEvent += new EventHandler(EAUpdateEvent);
            ea.StartContinue();
            Console.ReadLine();
        }

        static void EAUpdateEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}",ea.CurrentGeneration, ea.Statistics._maxFitness));
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { ea.CurrentChampGenome }, false);
            doc.Save(CHAMPION_FILE);
        }

    }
}
