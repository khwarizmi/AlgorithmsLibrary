using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class Kosaraju
    {
        bool[] visited;
        bool[] enqueue;
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

        /* BFS Code still needs adjustments */
        public int Kosaraju_BFSRun(List<int>[] G, List<int>[] GT, int nodesCount)
        {
            int from, to, count = 0;
            visited = new bool[nodesCount];
            enqueue = new bool[nodesCount];
            componentId = new int[nodesCount];
            int[] level = new int[nodesCount]; 
            SccComponenets = new List<List<int>>();
            Queue<int> nodeList = new Queue<int>();
            Queue<int> nodeOrder = new Queue<int>();
            
            /* First Pass */
            AdjList = GT;
            for (int i = 0; i < AdjList.Length; ++i)
            {
                if(visited[i]) continue;
                level[i] = count++;
                nodeOrder.Enqueue(i);
                nodeList.Enqueue(i);
                while (nodeList.Count > 0)
                {
                    from = nodeList.Dequeue();
                    visited[from] = enqueue[from] = true;
                    for (int j = 0; j < AdjList[from].Count; j++)
                    {
                        to = AdjList[from][j];
                        if (!enqueue[to])
                        {
                            nodeList.Enqueue(to);
                            nodeOrder.Enqueue(to);
                            enqueue[to] = true;
                            level[to] = level[i];
                        }
                    }
                }
            }

            /* Second Pass */
            int componentCount = 0;
            AdjList = G;
            for (int i = 0; i < nodesCount; ++i)
                visited[i] = enqueue[i] = false;

            while(nodeOrder.Count > 0)
            {
                from = nodeOrder.Dequeue();
                if (visited[from]) continue;
                visited[from] = enqueue[from] = true;
                Queue<int> reachableNodes = new Queue<int>();
                reachableNodes.Enqueue(from);
                while (reachableNodes.Count > 0)
                {
                    int node = reachableNodes.Dequeue();
                    componentId[node] = componentCount;
                    visited[node] = true;
                    for (int i = 0; i < AdjList[node].Count; ++i)
                    {
                        to = AdjList[node][i];
                        if (!enqueue[to] && level[to] == level[from])
                        {
                            reachableNodes.Enqueue(to);
                            enqueue[to] = true;
                        }
                    }
                }

                componentCount++;
            }

            //Create Scc
            for (int i = 0; i < componentCount; ++i)
                SccComponenets.Add(new List<int>());
            for (int node = 0; node < nodesCount; ++node)
                SccComponenets[componentId[node]].Add(node);

            return SccComponenets.Count;

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
