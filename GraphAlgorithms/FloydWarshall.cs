using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DynammicComponents;

namespace AlgorithmsLibrary
{
    class FloydWarshall
    {
        const int INF = 2000000000;
        int nodes;
        int[,] Table;
        bool negativeCycle;

        public FloydWarshall(int vertices)
        {
            negativeCycle = false;
            nodes = vertices;
            Table = new int[vertices, vertices];
        }

        public int QueryPath(int source, int dest)
        {
            return Table[source, dest];
        }

        public bool isNegativeCycle()
        {
            return negativeCycle;
        }

        public int SolveShortestPath(Edge[] EdgeList, int[] EdgeCost)
        {
            //Create Graph
            for (int i = 0; i < nodes; i++)
                for (int j = 0; j < nodes; j++)
                    Table[i, j] = INF;
            
            int from, to;
            for (int i = 0; i < EdgeList.Length; i++)
            {
                from = EdgeList[i].a;
                to = EdgeList[i].b;
                Table[from, to] = EdgeCost[i];
            }

            //Algorithm Steps
            for (int k = 0; k < nodes; k++)
                for (int i = 0; i < nodes; i++)
                    for (int j = 0; j < nodes; j++)
                        Table[i, j] = Math.Min(Table[i, j], Table[i, k] + Table[k, j]);

            //Detect Negative Cost-Cycle
            for (int i = 0; i < nodes; i++)
                if (Table[i, i] < 0)
                {
                    negativeCycle = true;
                    return -INF;
                }

            return 0;
        }
    }
}
