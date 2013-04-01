using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.DataStructures
{
    class IndexedPriorityQueue<TKey, TElement>
    {
        List<TKey> VertexKey=new List<TKey> ();
        List<TElement> VertexElement = new List<TElement>();
        List<int> PQIndex = new List<int>();
        List<int> ElementIndex = new List<int>();

        public IndexedPriorityQueue(TElement[] Elements, TKey[] Keys)
        {
            if (Elements.Length != Keys.Length) throw new Exception("Invalid Data Count");

            for (int i = 0; i < Keys.Length; i++)
            {
                PQIndex.Add(i);
                ElementIndex.Add(i);
                VertexKey.Add(Keys[i]);
                VertexElement.Add(Elements[i]);
            }

            for (int i = VertexKey.Count / 2; i >= 0; i--)
                MinHepify(i);
        }

        public IndexedPriorityQueue(List<TElement> Elements, List<TKey> Keys)
        {
            if(Elements.Count!=Keys.Count) throw new Exception("Invalid Data Count");

            for (int i = 0; i < Keys.Count; i++)
            {
                PQIndex.Add(i);
                ElementIndex.Add(i);
                VertexKey.Add(Keys[i]);
                VertexElement.Add(Elements[i]);
            }

            for(int i=VertexKey.Count/2;i>=0;i--) 
                MinHepify(i);
        }

        public KeyValuePair<TElement,TKey> Minimum()
        {
            if (VertexKey.Count < 1) 
                throw new Exception("Priority Queue is Empty !.");

            return (new KeyValuePair<TElement, TKey>(VertexElement[0], VertexKey[0]));
        }

        public KeyValuePair<TElement,TKey> Extract_Minimum()
        {
            if (VertexKey.Count < 1) 
                throw new Exception("Priority Queue is Empty !.");

            KeyValuePair<TElement, TKey> KvPair = new KeyValuePair<TElement, TKey>(VertexElement[0], VertexKey[0]);

            int _Last = VertexElement.Count - 1;
            VertexElement[0] = VertexElement[_Last];
            VertexKey[0] = VertexKey[_Last];
            ElementIndex[ PQIndex[0] ] = -1;

            VertexElement.RemoveAt(_Last);
            VertexKey.RemoveAt(_Last);

            MinHepify(0);

            return KvPair;
        }
       
        public void Decrease_Key(int Index, TKey Cost)
        {
            if (Index < 0 || Index > ElementIndex.Count)
                throw new Exception("Invalid index Range.");
            if (ElementIndex[Index] == -1) 
                throw new Exception("Element Removed.");
            
            int _Index = ElementIndex[Index];
            VertexKey[_Index] = Cost;
            while (_Index != 0 && Compare(VertexKey[Parent(_Index)],VertexKey[_Index]) > 0)
            {
                Swap(Parent(_Index), _Index);
                _Index = Parent(_Index);
            }
        }

        public void MinHepify(int i)
        {
            int L = 2 * i + 1;
            int R = 2 * i + 2;
            int Minimum = -1;

            if (L < VertexKey.Count && Compare(VertexKey[L], VertexKey[i]) < 0)
                Minimum = L;
            else
                Minimum = i;

            if (R < VertexKey.Count && Compare(VertexKey[R], VertexKey[Minimum]) < 0)
                Minimum = R;
            
            if (Minimum != i)
            {
                Swap(i, Minimum);
                MinHepify(Minimum);
            }
        }

        public int Count
        {
            get { return VertexKey.Count; }
        }

        public int Parent(int i)
        {
            if (i % 2 == 0)
                return (i - 2) / 2;
            else
                return (i - 1) / 2;
        }

        public void Swap(int i, int j)
        {
            TKey Temp = VertexKey[i];
            VertexKey[i] = VertexKey[j];
            VertexKey[j] = Temp;

            TElement TempElement = VertexElement[i];
            VertexElement[i] = VertexElement[j];
            VertexElement[j] = TempElement;

            int t1 = PQIndex[i];
            PQIndex[i] = PQIndex[j];
            PQIndex[j] = t1;

            ElementIndex[ PQIndex[i] ] = i;
            ElementIndex[ PQIndex[j] ] = j;
        }

        public List<TElement> Vertices
        {
            get { return VertexElement; }
        }
       
        public List<TKey> keys
        {
            get { return VertexKey; }
        }

        public int Compare(object x, object y)
        {
            if (x is int)
            {
                int X = (int)x;
                int Y = (int)y;

                if (X == Y) return 0;
                if (X > Y) return 2;
                else
                    return -2;
            }
            else
                if (x is string)
                {
                    string X = (string)x;
                    string Y = (string)y;

                    return X.CompareTo(Y);
                }
                else
                    throw new Exception("This Kind Not Implented int ICompare.");
        }

        public TKey this[TElement element]
        {
            get
            {
                for (int i = 0; i < VertexElement.Count; i++)
                    if (Compare(VertexElement[i], element) == 0)
                        return VertexKey[i];
                
                throw new Exception("Error");
            }

        }
    }
}
