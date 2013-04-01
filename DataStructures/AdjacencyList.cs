using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.DataStructures
{
    struct WeightedArc
    {
        public int Vertex;
        public double Cost;
        public WeightedArc(int v, double c) { Vertex = v; Cost = c; }
    }

    class AdjacencyList
    {
        int _VertexCount = 0;
        List<WeightedArc>[] _Graph;

        public AdjacencyList(int VertexCount)
        {
            _VertexCount = VertexCount;
            _Graph = new List<WeightedArc>[VertexCount];
            for (int i = 0; i < VertexCount; ++i)
                _Graph[i] = new List<WeightedArc>();
        }

        public void addEdge(int from, int to, double cost)
        {
            Assert.InRange(from, 0, _VertexCount - 1);
            _Graph[from].Add(new WeightedArc(to, cost));
        }

        public int getVertexCount()
        {
            return _VertexCount;
        }

        public List<WeightedArc> getEdges(int Vertex)
        {
            Assert.InRange(Vertex, 0, _VertexCount - 1);
            return _Graph[Vertex];
        }
    }
}
