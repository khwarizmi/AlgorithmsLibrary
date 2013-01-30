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

            /*
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
            */

            for (int f = 1; f <= 6; f++)
            {
                StreamReader sr = new StreamReader(@"C:\Users\Feras\Desktop\sat" + f + ".txt");
                int n = int.Parse(sr.ReadLine());
                for (int i = 0; i < 2 * n; i++)
                    adj.Add(new List<int>());

                int a, b, _a, _b;
                string[] str;
                for (int i = 0; i < n; i++)
                {
                    str = sr.ReadLine().Split(' ');
                    a = int.Parse(str[0]);
                    b = int.Parse(str[1]);
                    if (a < 0)
                    {
                         a = (Math.Abs(a) - 1) + n;
                        _a = a - n;
                    }
                    else
                    {
                         a = a - 1;
                        _a = a + n;
                    }

                    if (b < 0)
                    {
                         b = (Math.Abs(b) - 1) + n;
                        _b = b - n;
                    }
                    else
                    {
                        b = b - 1;
                       _b = b + n;
                    }

                    adj[_a].Add(b);
                    adj[_b].Add(a);
                }

                scc.Run(adj, n * 2);

                bool satisfiable = true;
                for(int i = 0; i < n && satisfiable; i++)
                    if (scc.SameComponent(i, i + n))
                        satisfiable = false;

                Console.WriteLine(satisfiable);
                sr.Close();
            }
        }
    }
}
