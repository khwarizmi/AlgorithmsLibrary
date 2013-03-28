using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AlgorithmsLibrary.GraphAlgorithms;

namespace AlgorithmsLibrary
{
    class SAT_Problem
    {
        public static void solve()
        {
            for (int f = 1; f <= 6; f++)
            {
                Tarjan scc = new Tarjan();
                StreamReader sr = new StreamReader(@"C:\Users\Feras\Desktop\sat" + f + ".txt");
                List<List<int>> adj = new List<List<int>>();
                List<List<int>> adjTranspose = new List<List<int>>();
                int n = int.Parse(sr.ReadLine());
                int[] seen = new int[n];

                for (int i = 0; i < 2 * n; i++)
                {
                    adj.Add(new List<int>());
                    adjTranspose.Add(new List<int>());
                }

                int a, b, _a, _b;
                string[] str;
                while (!sr.EndOfStream)
                {
                    str = sr.ReadLine().Split(' ');
                    a = int.Parse(str[0]);
                    b = int.Parse(str[1]);
                    seen[Math.Abs(a) - 1]++;
                    seen[Math.Abs(b) - 1]++;
                }

                sr.Close();
                sr = new StreamReader(@"C:\Users\Feras\Desktop\sat" + f + ".txt");
                
                sr.ReadLine();
                while(!sr.EndOfStream)
                {
                    str = sr.ReadLine().Split(' ');
                    a = int.Parse(str[0]);
                    b = int.Parse(str[1]);

                    if (seen[Math.Abs(a) - 1] == 1 || seen[Math.Abs(b) - 1] == 1)
                        continue;

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
                    if(b != a && _a != _b)
                        adj[_b].Add(a);
                    /*
                    // Add Edge Transpose
                    adjTranspose[b].Add(_a);
                    if (b != a && _a != _b)
                        adjTranspose[a].Add(_b);
                     * */
                }

                //scc.RunKosaraju(adj, adjTranspose, n);
                scc.RunTarjan(adj, 2 * n);

                bool satisfiable = true;
                for (int i = 0; i < n && satisfiable; i++)
                {
                    satisfiable = !scc.SameComponent(i, i + n);
                    if (!satisfiable)
                        Console.WriteLine(i);
                }

                Console.WriteLine(satisfiable);
                //scc.PrintComponents();
                sr.Close();
            }
        }
    }
}
