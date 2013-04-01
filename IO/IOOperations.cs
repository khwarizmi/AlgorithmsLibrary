using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AlgorithmsLibrary.DataStructures;

namespace AlgorithmsLibrary.IO
{
    class IOOperations
    {
        /*
         vertex_count edges_count
         0 1 
         1 2
         2 3
         */
        static public List<int>[] ReadGraph(string inputFile)
        {
            StreamReader sr = new StreamReader(inputFile);
            string[] header = sr.ReadLine().Split(' ');
            int vertex_count = int.Parse(header[0]);
            int edge_count = int.Parse(header[1]);
            List<int>[] G = new List<int>[vertex_count];
            for (int i = 0; i < vertex_count; ++i)
                G[i] = new List<int>();

            for(int i = 0; i < edge_count; ++i)
            {
                string[] line = sr.ReadLine().Split(' ');
                int from = int.Parse(line[0]);
                int to = int.Parse(line[1]);
                G[from].Add(to);
            }

            return G;
        }

        static public AdjacencyList ReadAdjacencyGraph(string inputFile)
        {
            StreamReader sr = new StreamReader(inputFile);
            int vertex_count = int.Parse(sr.ReadLine());
            int edge_count = int.Parse(sr.ReadLine());
            AdjacencyList adjList = new AdjacencyList(vertex_count);

            for (int i = 0; i < edge_count; ++i)
            {
                string[] line = sr.ReadLine().Split(' ');
                int from = int.Parse(line[0]);
                int to = int.Parse(line[1]);
                double cost = double.Parse(line[2]);
                adjList.addEdge(from, to, cost);
            }

            return adjList;
        }
    }
}
