using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary.NumericalMethods
{
    class LU
    {
        /* If L has 1's on it's diagonal, then it is called a Doolittle factorization.  
         * If U has 1's on its diagonal, then it is called a Crout factorization.  
         * When L= U transpose ->  it is called a Cholesky decomposition.  
         * In this module we will develop an algorithm that produces a Doolittle factorization.
         * */


        /*
         *AX = B. Then the matrix A can be factored as the
        product of a lower-triangular matrix L and an upper-triangular matrix U:
                                              A = LU.
        Furthermore, L can be constructed to have 1’s on its diagonal and U will have nonzero
        diagonal elements. After finding L and U, the solution X is computed in two steps:
            1. Solve LY = B for Y using forward substitution.
            2. Solve UX = Y for X using back substitution.
        */

        double[] LU_Solve(double[,] A, double[] B, int n)
        {
            int[] order = new int[n];/* Used to Construct 'P' Matrix */
            for (int i = 0; i < n; i++)
                order[i] = i;

            double[,] L = new double[n,n];

            int pos = -1;
            /* Calculate LU */
            for (int p = 0; p < n; p++) /* Loop over each row */
            {

                pos = p;

                for (int k = p + 1; k < n; k++)
                    if (Math.Abs(A[pos,p]) < Math.Abs(A[k,p])) /*Choose pivot with largest value due to Numerical Stability*/
                        pos = k;

                if (A[pos,p] == 0)/*Check if current Pivot == 0 => Matrix Singular */
                {
                    Console.WriteLine("Matrix is Singular");
                    return null;
                }

                if (pos != p)/* if Current pivot != the original row Pivot => Exhange Row */
                {
                    for (int swapIndex = 0; swapIndex < n; swapIndex++)
                       swap(ref A[p,swapIndex], ref A[pos,swapIndex]);
                    
                    swap(ref order[p], ref order[pos]); 
                }

                for (int i = p + 1; i < n; i++) /* loop over each row below the bottom */
                    for (int j = p + 1; j < n; j++) /* loop over Columns */
                        A[i,j] = A[i,j] - (A[p,j] * A[i,p]) / A[p,p];


                for (int k = p + 1; k < n; k++)/*Divide all numbers in the row of Pivot with the pivot Value */
                    A[k,p] = A[k,p] / A[p,p];

                /* Debug */
                //print(A,n,n);
                //cout<<"-----------------"<<endl;
            }

            /* Solve System P.L.U.X = P.b
             * P.L.y = P.b
             * Forward Subsitution to Calculate y
             * U.X = y
             * Backward Subsitution to Calculate X. 
             * */

            /* Forward Subsitution */
            double[] y = new double[n];
            double sum = 0;

            for (int i = 0; i < n; i++)
            {
                sum = 0;

                for (int j = 0; j < i; j++)
                    sum += (B[order[j]] * A[i,j]);

                y[i] = B[order[i]] - sum; /* U Matrix Diagonal Consists of one */
            }

            /* Backward Subsitution */

            double[] x = new double[n];

            for (int i = n - 1; i > -1; i--)
            {
                sum = 0;

                for (int j = n - 1; j > i; j--)
                    sum += (x[j] * A[i,j]);

                x[i] = (y[i] - sum) / A[i,i];
            }

            /*Debug
            print(A,n,n);
            cout<<"------***-----"<<endl;
            cout<<"Last Matrix Order "<<endl;
            for(int i=0;i<n;i++)
              cout<<order[i]<<endl;
            */

            return x;
        }

        private void swap(ref double x,ref double y)
        {
            double temp = x;
            x = y;
            y = temp;
        }

        private void swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        void Multiply(double[,] L, double[,] U, int r1, int c, int r2)
        {
            double[,] Res = new double[r1,r2];
            
            for (int i = 0; i < r1; i++)
                for (int j = 0; j < r2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < c; k++)
                        sum += L[i,k] * U[k,j];

                    Res[i,j] = sum;
                }

            print(Res, r1, r2);

        }

        void print(double[,] a, int r, int c)
        {
            //Print Result A Matrix
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                    Console.Write(a[i,j] + " ");
                Console.WriteLine();
            }
        }

    }
}
