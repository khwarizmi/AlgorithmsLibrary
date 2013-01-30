using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AlgorithmsLibrary.DynammicComponents;
using AlgorithmsLibrary.GraphAlgorithms;

namespace AlgorithmsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            StronglyConnectedComponents scc = new StronglyConnectedComponents();
            List<List<int>> adj = new List<List<int>>();

            adj.Add(new List<int> { 1, 6 }); 
            adj.Add(new List<int> { 2 });
            adj.Add(new List<int> { 3 });
            adj.Add(new List<int> { 4 });
            adj.Add(new List<int> { 5 });
            adj.Add(new List<int> { 2 });
            adj.Add(new List<int> { 1, 7 });
            adj.Add(new List<int> ());

            Console.WriteLine(scc.Run(adj, 8));
            scc.PrintComponents();
        }
    }
}
