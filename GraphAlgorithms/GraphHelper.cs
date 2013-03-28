using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.GraphAlgorithms
{
    class GraphHelper
    {
        static public List<int>[] ReverseGraph(List<int>[] G)
        {
            List<int>[] GT = new List<int>[G.Length];
            for (int i = 0; i < G.Length; ++i)
                GT[i] = new List<int>();

            for (int from = 0; from < G.Length; ++from)
                for (int to = 0; to < G[from].Count; ++to)
                    GT[G[from][to]].Add(from);

            return GT;
        }
    }
}
