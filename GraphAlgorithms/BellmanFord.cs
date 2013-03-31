using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DynammicComponents;
using AlgorithmsLibrary.GraphAlgorithms;

namespace AlgorithmsLibrary
{
    class BellmanFord
    {
        const int INF = 2000000000;
        static public int SolveShortestPath(Edge[] EdgeList, int[] EdgeCost, int nodes, int source, int dest)
        {
            int E = EdgeList.Length;
            int r1,r2,w,v,cost;
            int[,] Bellman = new int[2, nodes]; //using row decomposition
            
            //Init Step
            r1 = r2 = w = v = cost = 0;
            for (int i = 0; i < nodes; i++)
                Bellman[0, i] = Bellman[1, i] = INF;
            Bellman[0, source] = Bellman[1, source] = 0;
            
            //Algorithm Step
            for (int i = 1; i < nodes; i++)
            {
                r2 = i%2; r1 = (i-1)%2;
                for (int j = 0; j < EdgeList.Length; j++)
                {
                    w = EdgeList[j].a;
                    v = EdgeList[j].b;
                    cost = EdgeCost[j];
                    Bellman[r2, v] = Math.Min(Bellman[r1, v], Bellman[r1, w] + cost); 
                }
            }

            //Shortest Path Cost
            int shortestPathCost = Bellman[r2, dest];

            //Detect Negative-Cost Cycle
            for (int i = nodes; i <= nodes; i++) //One-More Cycle
            {
                r2 = i % 2; r1 = (i - 1) % 2;
                for (int j = 0; j < EdgeList.Length; j++)
                {
                    w = EdgeList[j].a;
                    v = EdgeList[j].b;
                    cost = EdgeCost[j];
                    if (Bellman[r1, v] > Bellman[r1, w] + cost)
                        return -INF;
                }
            }

            return shortestPathCost;
        }
    }
}
