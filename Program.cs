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


            char[] arr = new char[] {'F', 'L', 'Q' ,'D' ,'U' ,'P' ,'E' ,'Y' ,'N' ,'R' };
            
            for(int i = 0;i < 4; i++)
            {
                int smallest = i;
                for (int k = i + 1; k < arr.Length; k++)
                    if (arr[smallest] > arr[k])
                        smallest = k;

                char temp = arr[smallest];
                arr[smallest] = arr[i];
                arr[i] = temp;
            }

            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");

            Console.WriteLine();
              
            arr = new char[] {  'F' ,'G' ,'J' ,'V' ,'Y' ,'I' ,'W' ,'A' ,'P' ,'E'  };
            int ex = 0;
            for (int i = 0; i < arr.Length; i++)
                for (int k = i; k > 0; k--)
                    if (k - 1 >= 0 && arr[k] < arr[k - 1])
                    {
                        char temp = arr[k];
                        arr[k] = arr[k - 1];
                        arr[k - 1] = temp;
                        ex++;
                        if (ex == 6)
                            goto end;
                    }
                    else break;
                
            end:
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + " ");


        }
    }
}
