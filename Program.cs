using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {

        public static GUI gui;
        public static int delay = 500;


        public static int Main(string[] args)
        {
            gui = new GUI();
            gui.form.ShowDialog();

            return 0;
        }


    }
}
