using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class TSP
    {
        static public void Run(string filePath)
        {
            const double INF = int.MaxValue;
            int n = 6;
            //string filePath = @"C:\Users\Feras\Desktop\tsp.txt";
            StreamReader sr = new StreamReader(@filePath);

            //
            n = int.Parse(sr.ReadLine());
            int MAX = 1 << n;
            int nSet = 0;
            int p = 2704157;
            int newIndex = 0;
            int oldIndex = 0;
            double[,] nodes = new double[n, 2];
            double[,] Graph = new double[n, n];
            double[, ,] state = new double[p, n, 2]; // [Set, LastVertex, vertexCount_Index] 
            int[] indices = new int[1 << n];
            int[] indexCount = new int[n + 1];
            int[] setSize = new int[1 << n];

            //Read input and Create Graph
            for (int i = 0; i < n; i++)
            {
                string[] xy = sr.ReadLine().Split(' ');
                nodes[i, 0] = double.Parse(xy[0]);
                nodes[i, 1] = double.Parse(xy[1]);
            }

            //Convert to Graph Matrix
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Graph[i, j] = (nodes[i, 0] - nodes[j, 0]) * (nodes[i, 0] - nodes[j, 0]);
                    Graph[i, j] += (nodes[i, 1] - nodes[j, 1]) * (nodes[i, 1] - nodes[j, 1]);
                    Graph[i, j] = Math.Sqrt(Graph[i, j]);
                }

            //Create Index Mapping
            int sz = 0;
            for (int i = 1; i < (1 << n); i += 2)
            {
                sz = 0;
                for (int j = 0; j < n; j++)
                    if (((1 << j) & i) != 0)
                        sz++;

                indices[i] = indexCount[sz]++;
                setSize[i] = sz;
            }

            //TSP Algorithm
            //Base Case
            for (int i = 0; i < p; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < 2; k++)
                        state[i, j, k] = INF;

            // [Set, LastVertex, vertexCount_Index] 
            state[indices[1], 0, 1] = 0;
            for (int i = 1; i < n; i++)
                state[indices[(1 | (1 << i))], i, 0] = Graph[0, i];

            //Steps
            for (int m = 2; m < n; m++)
            {
                //Re-Init Old Values to use new values in Math.Min
                for (int i = 0; i < p; i++)
                    for (int j = 0; j < n; j++)
                        state[i, j, (m + 1) % 2] = INF;

                for (int set = 1; set < MAX; set += 2)
                {
                    //Check Size of set isn't equal to m
                    if (setSize[set] != m)
                        continue;

                    for (int k = 1; k < n; k++)
                        if (((1 << k) & set) != 0)
                            for (int vertex = 1; vertex < n; vertex++)
                                if (((1 << vertex) & set) == 0)
                                {
                                    nSet = set | (1 << vertex);
                                    newIndex = indices[nSet];
                                    oldIndex = indices[set];

                                    // [Set, LastVertex, vertexCount] 
                                    state[newIndex, vertex, (m + 1) % 2] = Math.Min(state[newIndex, vertex, (m + 1) % 2], state[oldIndex, k, m % 2] + Graph[k, vertex]);
                                }
                }
            }
            //Get minimum length to vertex '1'
            int setIndex = indices[(1 << n) - 1];
            double minLength = double.MaxValue;
            for (int i = 1; i < n; i++)
                minLength = Math.Min(minLength, state[setIndex, i, n % 2] + Graph[i, 0]);

            Console.WriteLine("Result: " + minLength);
        }
    }
}
