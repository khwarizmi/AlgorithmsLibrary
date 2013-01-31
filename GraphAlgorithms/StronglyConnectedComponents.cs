using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    //Using Tarjan SCC Algorithm
    class StronglyConnectedComponents
    {
        const int WHITE = 0;
        const int BLACK = 1;
        const int GRAY = 2;
        List<List<int>> AdjList;
        List<List<int>> SccComponenets;
        Stack<int> visitedNodes;
        int[] vertex_num, vertex_low, visited, vertex_visited;
        int vertexNumber;

        public StronglyConnectedComponents()
        {
            AdjList = null;
            visited = vertex_num = vertex_low = vertex_visited = null;
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

        /*
         * // More Updates and changed to Algorithm needed
         * // Runs Algorithm on Graph, Graph Transpose, Nodes Count
        public int RunKosaraju(List<List<int>> G, List<List<int>> GT, int nodesCount)
        {
            AdjList = G;
            SccComponenets.Clear(); // clear previous components
            visitedNodes.Clear();
            visited = new int[nodesCount]; 
            vertex_num = new int[nodesCount];
            vertex_visited = new int[nodesCount];
            vertex_low = new int[nodesCount];
            vertexNumber = 0;

            //Init array
            for (int i = 0; i < nodesCount; i++)
                vertex_low[i] = -(i + 1);

            // First Path of Algorithm
            for (int i = 0; i < AdjList.Count; i++)
                if (visited[i] == WHITE)
                    KosarajuForward(i);

            int v = 0;
            AdjList = GT;
            // Second Path on Graph Transpose
            for (int i = vertex_visited.Length - 1; i >= 0; i--)
            {
                v = vertex_visited[i];
                if(visited[v] == BLACK)
                    KosarajuBackward(v);
            }

            for(int i = 0; i < AdjList.Count; i++)
                Console.WriteLine(vertex_num[i]);
            return SccComponenets.Count;
        }
        */

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

        /* BFS Code needs to be Changed to DFS to Solve Problem Correct
        void KosarajuForward(int v)
        {
            int u = 0;
            Queue<int> nodeList = new Queue<int>();
            Stack<int> postOrder = new Stack<int>();
            nodeList.Enqueue(v);
            visited[v] = BLACK;

            while (nodeList.Count > 0)
            {
                v = nodeList.Dequeue();
                postOrder.Push(v);
                for (int i = 0; i < AdjList[v].Count; i++)
                {
                    u = AdjList[v][i];
                    if(visited[u] != BLACK)
                    {
                        nodeList.Enqueue(u);
                        visited[u] = BLACK;
                    }
                }
            }

            while (postOrder.Count > 0)
            {
                u = postOrder.Pop();
                vertex_num[u] = ++vertexNumber;
                vertex_visited[vertexNumber - 1] = u;
            }
        }

        void KosarajuBackward(int v)
        {
            int u = 0;
            int maxCost = vertex_num[v];
            Queue<int> nodeList = new Queue<int>();
            List<int> group = new List<int>();
            nodeList.Enqueue(v);
            visited[v] = GRAY;    

            while (nodeList.Count > 0)
            {
                v = nodeList.Dequeue();
                group.Add(v); // current group Componenets
                for (int i = 0; i < AdjList[v].Count; i++)
                {
                    u = AdjList[v][i];
                    if (maxCost > vertex_num[u] && visited[u] != GRAY)
                    {
                        nodeList.Enqueue(u);
                        visited[u] = GRAY;        
                    }
                }
            }

            int groupNumbers = SccComponenets.Count + 1;
            foreach (int vertex in group)
                vertex_low[vertex] = groupNumbers;

            SccComponenets.Add(group);
        }
        */

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
