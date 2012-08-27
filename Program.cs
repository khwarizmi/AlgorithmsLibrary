using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgorithmsLibrary.DynammicComponents;

namespace AlgorithmsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //0-9 9-2 0-8 0-7 0-1 6-0 
            DynammicComponents.QuickFind qf = new DynammicComponents.QuickFind(10);
            qf.Union(0, 9);
            qf.Union(9, 2);
            qf.Union(0, 8);
            qf.Union(0, 7);
            qf.Union(0, 1);
            qf.Union(6, 0);

            //  9-5 7-2 6-1 1-4 1-8 0-3 7-3 4-9 8-0 
            DynammicComponents.QuickUnion qu = new DynammicComponents.QuickUnion(10);
            qu.Union(9, 5);
            qu.Union(7, 2);
            qu.Union(6, 1);
            qu.Union(1, 4);
            qu.Union(1, 8);
            qu.Union(0, 3);
            qu.Union(7, 3);
            qu.Union(4, 9);
            qu.Union(8, 0);

        }
    }
}
