using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AlgorithmsLibrary.DynammicComponents;
using AlgorithmsLibrary.GraphAlgorithms;
using AlgorithmsLibrary.IO;

namespace AlgorithmsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string str = @"tiny.txt";
            /*
             * 13 22
                0 2
                0 6
                1 0
                2 3
                2 4
                3 2
                3 4
                4 5
                4 6
                4 11
                5 0
                5 3
                6 8
                6 7
                8 6
                9 6
                9 7
                9 12
                10 9
                11 9
                12 10
                12 11
             * */
            List<int>[] G = IOOperations.ReadGraph(str);
            /* reverse Graph */
            List<int>[] GT = GraphHelper.ReverseGraph(G);
            Kosaraju ks = new Kosaraju();
            //ks.Run(G, GT, G.Length);
            ks.Kosaraju_BFSRun(G, GT, G.Length);
            ks.PrintComponents();
        }
    }
}
