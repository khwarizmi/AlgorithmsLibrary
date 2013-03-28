using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class Kosaraju
    {
        bool[] visited;
        int[] componentId;
        List<List<int>> SccComponenets;
        List<int>[] AdjList;
        Stack<int> fpStack; //First Pass Stack

        public Kosaraju()
        {

        }

        private void FirstPassDFS(int node)
        {
            visited[node] = true;
            for (int i = 0; i < AdjList[node].Count; ++i)
            {
                int to = AdjList[node][i];
                if (!visited[to])
                    FirstPassDFS(to);
            }
            fpStack.Push(node);
        }

        private void SecondPassDFS(int node, int componentCount)
        {
            visited[node] = true;
            componentId[node] = componentCount;
            for (int i = 0; i < AdjList[node].Count; ++i)
            {
                int to = AdjList[node][i];
                if (!visited[to])
                    SecondPassDFS(to, componentCount);
            }
        }

        public int Run(List<int>[] G, List<int>[] GT, int nodesCount)
        {
            fpStack = new Stack<int>();
            visited = new bool[nodesCount];
            componentId = new int[nodesCount];
            SccComponenets = new List<List<int>>();

            //Run First Pass: Reverse PostOrder DFS on Graph Transpose
            AdjList = GT;
            for (int i = 0; i < AdjList.Length; ++i)
            {
                if (!visited[i])
                    FirstPassDFS(i);
            }

            //Run Second Pass: Mark reachable nodes as Same Component
            int componentCount = 0;
            AdjList = G;
            for (int i = 0; i < nodesCount; ++i) 
                visited[i] = false;
 
            while(fpStack.Count > 0)
            {
                int node = fpStack.Pop();
                if (!visited[node])
                {
                    SecondPassDFS(node, componentCount);
                    componentCount++;
                }
            }

            //Create Scc
            for(int i  = 0; i < componentCount; ++i)
                SccComponenets.Add(new List<int>());
            for (int node = 0; node < nodesCount; ++node)
                SccComponenets[componentId[node]].Add(node);

            return SccComponenets.Count;
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

        public int MaxComponent()
        {
            if (SccComponenets == null)
                return 0;

            int max = 0;
            foreach (List<int> group in SccComponenets)
                max = Math.Max(max, group.Count);

            return max;
        }

        public void PrintComponents()
        {
            if (SccComponenets == null)
                return;

            foreach (List<int> group in SccComponenets)
            {
                foreach (int vertex in group)
                    Console.Write(vertex + ", ");
                Console.WriteLine("\n------------------------");
            }
        }

    }
}
