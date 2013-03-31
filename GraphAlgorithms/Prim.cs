using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DataStructures;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class Prim
    {
        int _nodes;
        bool[] _VertexMarked;
        int[] _EdgeCost;
        Edge[] _EdgeList;
        List<Edge> _MstEdges;

        public Prim(Edge[] EdgeList, int[] EdgeCost, int nodes)
        {
            _nodes = nodes;
            _VertexMarked = new bool[nodes];
            _EdgeCost = EdgeCost;
            _EdgeList = EdgeList;
        }

        public int SolveMSt()
        {
            int _MstCost = 0;
            _MstEdges = new List<Edge>();
            PriorityQueue<int, Edge> pQ = new PriorityQueue<int, Edge>(_EdgeCost, _EdgeList);

            for (int i = 0; i < _nodes - 1; ++i)
            {
                KeyValuePair<int, Edge> minElement = pQ.Extract_Minimum();
                int cost = minElement.Key;
                Edge minEdge = minElement.Value;

                if (_VertexMarked[minEdge.a] && _VertexMarked[minEdge.b]) continue;

                _VertexMarked[minEdge.a] = _VertexMarked[minEdge.b] = true;
                _MstCost += cost;
                _MstEdges.Add(minEdge);
            }
            
            return _MstCost;
        }

        public List<Edge> Edges()
        {
            return _MstEdges;
        }
    }
}
