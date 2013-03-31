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
        int _nodes;
        int[] _EdgeCost;
        Edge[] _EdgeList;
        List<Edge> _MstEdges;
        public Kruskal(Edge[] EdgeList, int[] EdgeCost, int nodes)
        {
            _nodes = nodes;
            _EdgeCost = EdgeCost;
            _EdgeList = EdgeList;
        }

        public int SolveMSt()
        {
            int mstCost = 0;
            _MstEdges = new List<Edge>();
            QuickUnion Qu = new QuickUnion(_nodes + 1);
            Array.Sort(_EdgeCost, _EdgeList);
            for (int i = 0; i < _EdgeCost.Length; i++)
            {
                if (!Qu.Find(_EdgeList[i].a, _EdgeList[i].b))
                {
                    mstCost += _EdgeCost[i];
                    _MstEdges.Add(_EdgeList[i]);
                    Qu.Union(_EdgeList[i].a, _EdgeList[i].b);
                }
            }
            
            return mstCost;
        }

        public List<Edge> Edges()
        {
            return _MstEdges;
        }
    }
}
