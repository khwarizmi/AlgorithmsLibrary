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
        static HashSet<string> possibles = new HashSet<string>();
        static HashSet<string> hset = new HashSet<string>();
        static Dictionary<string, int> dict = new Dictionary<string, int>();
           
        static void permute(StringBuilder s, int changes)
        {
            string ky = s.ToString();
            if (changes == 2)
                possibles.Add(ky);
            else
            {
                possibles.Add(ky);
                for (int i = 0; i < s.Length; i++)
                {
                    char old = s[i];
                    if (s[i] == '0')
                    {
                        s[i] = '1';
                        permute(s, changes + 1);
                    }
                    else
                    {
                        s[i] = '0';
                        permute(s, changes + 1);
                    }
                    s[i] = old;
                }
            }
        }

        static void run(string file)
        {
            //clear hset
            hset.Clear();
            dict.Clear();
            possibles.Clear();

            List<string> lines = new List<string>();
            StreamReader sr = new StreamReader(file);
            string x = sr.ReadLine();
            int k = 0;
            while (!sr.EndOfStream)
            {
                string raw = sr.ReadLine();
                if (raw == "")
                    continue;

                string str = raw.Replace(" ", "");
                if (!dict.ContainsKey(str))
                {
                    dict.Add(str, k);
                    lines.Add(str);
                    k++;
                }
                hset.Add(str);
            }

            int nodes = dict.Keys.Count;
            QuickUnion Qu = new QuickUnion(nodes);
            for (int i=0; i < nodes; i++)
            {
                possibles.Clear();
                permute(new StringBuilder(lines[i]), 0);
                foreach (string s in possibles)
                {
                    if (hset.Contains(s) && s != lines[i])
                    {
                        if (!Qu.Find(dict[s], dict[lines[i]]))
                            Qu.Union(dict[s], dict[lines[i]]);
                    }
                }
            }

            Console.WriteLine(Qu.Count());
        }

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
