using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class Tarjan
    {
        const int WHITE = 0;
        const int BLACK = 1;
        const int GRAY = 2;
        List<List<int>> AdjList;
        List<List<int>> SccComponenets;
        Stack<int> visitedNodes;
        int[] vertex_num, vertex_low, visited;
        int vertexNumber;

        public Tarjan()
        {
            AdjList = null;
            visited = vertex_num = vertex_low = null;
            visitedNodes = null;
            SccComponenets = new List<List<int>>();
            visitedNodes = new Stack<int>();
        }

        public int RunTarjan(List<List<int>> AdjacencyList, int nodesCount)
        {
            AdjList = AdjacencyList; 
            SccComponenets.Clear(); // clear previous components
            visitedNodes.Clear();
            visited = new int[nodesCount]; 
            vertex_num = new int [nodesCount];
            vertex_low = new int[nodesCount];
            vertexNumber = 0;

            for (int i = 0; i < AdjList.Count; i++)
            {
                if (visited[i] == WHITE)
                    tarjanScc(i);
            }
            return SccComponenets.Count;
        }

        public void PrintComponents()
        {
            if (SccComponenets == null)
                return;

            foreach (List<int> group in SccComponenets)
            {
                Console.WriteLine("\n------------------------");
                foreach (int vertex in group)
                    Console.Write(vertex + ", ");
            }
        }

        void tarjanScc(int v)
        {
            int u = 0;
            vertex_num[v] = vertex_low[v] = vertexNumber++;
            visited[v] = GRAY;
            visitedNodes.Push(v);
            for (int i = 0; i < AdjList[v].Count; i++)
            {
                u = AdjList[v][i];

                if (visited[u] == WHITE) // node isn't visited yet
                  tarjanScc(u);

                if(visited[u] != BLACK)
                    vertex_low[v] = Math.Min(vertex_low[v], vertex_low[u]);
            }

            if (vertex_num[v] == vertex_low[v])
            {
                SccComponenets.Add(new List<int>());
                int index = SccComponenets.Count - 1;
                while (true)
                {
                    u = visitedNodes.Peek();
                    visitedNodes.Pop();
                    visited[u] = BLACK;
                    SccComponenets[index].Add(u);
                    
                    if (u == v)
                        break;
                }
            }
        }

        public bool SameComponent(int v1, int v2)
        {
            return (vertex_low != null) && v1 < vertex_low.Length && v2 < vertex_low.Length && (vertex_low[v1] == vertex_low[v2]);
        }

        public int MaxComponent()
        {
            if (SccComponenets == null)
                return 0;

            int max = 0;
            foreach (List<int> group in SccComponenets)
                max = Math.Max(max, group.Count);

            return max;
        }
    }
}
