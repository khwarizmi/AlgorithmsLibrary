using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.DynammicComponents
{
    class QuickFind
    {
        int[] id;
        public QuickFind(int N)
        {
            id = new int[N];
            for (int i = 0; i < N; i++)
                id[i] = i;
        }

        public bool Find(int pid, int qid)
        {
            return id[pid] == id[qid];
        }

        public void Union(int pid, int qid)
        {
            if (pid == qid)
                return;

            int p = id[pid];
            int q = id[qid];

            for (int i = 0; i < id.Length; i++)
                if (id[i] == p) id[i] = q;
        }
    }
}
