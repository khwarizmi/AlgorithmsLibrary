using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DataStructures;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class Dijkstra
    {
        int source;
        int vertexCount;
        int[] parent;
        double[] vertexCost;
        AdjacencyList adjList;
        
        public Dijkstra(AdjacencyList Graph, int S)
        {
            source = S;
            vertexCount = Graph.getVertexCount();
            adjList = Graph;
            parent = new int[vertexCount];
            vertexCost = new double[vertexCount];

            for (int i = 0; i < vertexCount; ++i)
            { 
                parent[i] = -1; 
                vertexCost[i] = double.MaxValue; 
            }

            parent[source] = -1;
            vertexCost[source] = 0;
        }

        public void Run()
        {
            IndexedPriorityQueue<double, int> pQ = new IndexedPriorityQueue<double, int>();
            for(int i = 0; i < vertexCount; ++i)
                pQ.Insert(i, double.MaxValue);

            pQ.Decrease_Key(source, 0);
            while (pQ.Count > 0)
            {
                KeyValuePair<double, int> kv = pQ.Extract_Minimum();
                vertexCost[kv.Value] = kv.Key;
                List<WeightedArc> edges = adjList.getEdges(kv.Value);
                foreach (WeightedArc e in edges)
                {
                    if (vertexCost[e.Vertex] > vertexCost[kv.Value] + e.Cost)
                    {
                        parent[e.Vertex] = kv.Value;
                        pQ.Decrease_Key(e.Vertex, vertexCost[kv.Value] + e.Cost);
                    }
                }
            }
        }

        public double DistanceTo(int Vertex)
        {
            return vertexCost[Vertex];
        }

        public bool HasPathTo(int Vertex)
        {
            return vertexCost[Vertex] != int.MaxValue;
        }

        public List<int> PathTo(int Vertex)
        {
            List<int> path = new List<int>();

            while (parent[Vertex] != -1)
            {
                path.Add(Vertex);
                Vertex = parent[Vertex];
            }

            path.Reverse();
            return path;
        }
    }
}
