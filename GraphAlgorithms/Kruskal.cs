using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DynammicComponents;

namespace AlgorithmsLibrary
{
    public struct Edge
    {
        public int a, b;
        public Edge(int _a, int _b) { a= _a; b= _b; }
    }

    class Kruskal
    {
        static public int SolveMSt(Edge[] EdgeList, int [] EdgeCost, int nodes)
        {
            int mstCost = 0;
            QuickUnion Qu = new QuickUnion(nodes + 1);
            Array.Sort(EdgeCost, EdgeList);
            for (int i = 0; i < EdgeCost.Length; i++)
            {
                if (!Qu.Find(EdgeList[i].a, EdgeList[i].b))
                {
                    mstCost += EdgeCost[i];
                    Qu.Union(EdgeList[i].a, EdgeList[i].b);
                }
            }
            
            return mstCost;
        }
    }
}
