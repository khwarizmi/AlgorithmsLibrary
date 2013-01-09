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
           string filePath = @"C:\Users\Feras\Desktop\g3.txt";
           StreamReader sr = new StreamReader (@filePath);
           string[] data = sr.ReadLine().Split(' ');
           int n = int.Parse(data[0]);
           int e = int.Parse(data[1]);
           Edge[] edges = new Edge[e];
           int[] costs = new int[e];

           int i = 0;
           while (!sr.EndOfStream)
           {
               data = sr.ReadLine().Split(' ');
               edges[i].a = int.Parse(data[1]) - 1;
               edges[i].b = int.Parse(data[0]) - 1;
               costs[i] = int.Parse(data[2]);
               i++;
           }

           FloydWarshall fWarshall = new FloydWarshall(n);
           int res = fWarshall.SolveShortestPath(edges, costs);
           if (res == -FloydWarshall.INF)
           {
               Console.WriteLine("Negative Cost Cycles");
           }
           else
           {
               int smallest = FloydWarshall.INF;
               for(i=0; i < n; i++)
                   for(int j=0; j< n; j++)
                       smallest = Math.Min(smallest, fWarshall.QueryPath(i,j));
               Console.WriteLine(smallest);
           }


        }

    }
}
