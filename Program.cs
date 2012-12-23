using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

            //Console.WriteLine(qu.Count());

            //C:\Users\Feras\Desktop\edges.txt
            StreamReader sr = new StreamReader(@"C:\Users\Feras\Desktop\clustering1.txt");
            string x= sr.ReadLine();
            string[] nv = x.Split(' ');
            int vertices= int.Parse(nv[0]);
            List<Edge> EdgeList= new List<Edge> ();
            List<int> EdgeCost = new List<int>();
            while (!sr.EndOfStream)
            {
                Edge ed;
                x = sr.ReadLine();
                nv = x.Split(' ');
                ed.a = int.Parse(nv[0]) - 1;
                ed.b = int.Parse(nv[1]) - 1;
                EdgeList.Add(ed);
                EdgeCost.Add(int.Parse(nv[2]));
            }

            Console.WriteLine(SolveCluster(EdgeList.ToArray(), EdgeCost.ToArray(), vertices));
        }

        //106
        static public int SolveCluster(Edge[] EdgeList, int[] EdgeCost, int nodes)
        {
            int mstCost = 0;
            QuickUnion Qu = new QuickUnion(nodes);
            Array.Sort(EdgeCost, EdgeList);
            int i = 0;
            for (; i < EdgeCost.Length && Qu.Count() > 4; i++)
            {
                if (!Qu.Find(EdgeList[i].a, EdgeList[i].b))
                {
                    mstCost += EdgeCost[i];
                    Qu.Union(EdgeList[i].a, EdgeList[i].b);
                }
            }

            for (; i < EdgeCost.Length; i++)
            {
                if (!Qu.Find(EdgeList[i].a, EdgeList[i].b))
                {
                    Console.WriteLine(EdgeCost[i]);
                    break;
                }
            }

            return mstCost;
        }
    }
}
