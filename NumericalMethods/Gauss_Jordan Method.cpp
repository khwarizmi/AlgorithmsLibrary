/*
 *      linear.cpp
 *      
 */


#include <iostream>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>

using namespace std;

double* solve(double** a,double* b,int n);
void print(double** a,int r,int c);

int main(int argc, char** argv)
{

	int n=3;
    double** a=new double*[n];
	a[0]=new double[n];
	a[1]=new double[n];
	a[2]=new double[n];
	
	a[0][0]=1;
 	a[0][1]=2;
 	a[0][2]=3;
 	
	a[1][0]=-3;
 	a[1][1]=1;
 	a[1][2]=5;
 	
	a[2][0]=2;
 	a[2][1]=4;
 	a[2][2]=-1;
 	
	double* b = new double[n];
	b[0]=3;
	b[1]=-2;
	b[2]=-1;
	
	double* x=new double[n];
	
	x=solve(a,b,n);
	
	for(int i=0;i<n;i++)
	  cout<<"X"<<i+1<<" : "<<x[i]<<endl;
	 
	 
	 //print(a,3,3); 
	 //swap(a[2],a[1]);
	 //print(a,3,3);
	 
	return 0;
}


double* solve(double** a,double* b,int n)
{
	//a is Supposed to Be Square Matrix.
	//Add matrix b to the End of a First
	//and Extend A Size.
	

	for(int p=0;p<n;p++)
	{		
		
		int index_LargestPivot=p;
		int index_Temp=-1;
		/* Choose Biggest Pivot to Get
		 * Numerical Stability */
		/*print(a,n,n);*/
		#pragma omp parallel private(index_Temp)
		{
			index_Temp=p;
			
		#pragma omp for
		for(int k=p+1;k<n;k++)
		  if(abs(a[k][p]) > abs(a[index_Temp][p]))
		  {
			  index_Temp=k;
			  //swap(a[k],a[p]);
			  //swap(b[k],b[p]);
		  }

		  #pragma omp critical 
		  {
			  if(abs(a[index_Temp][p])>abs(a[index_LargestPivot][p]))
			   index_LargestPivot=index_Temp;	  
		  }
		}
		/* Swap Best Positions */
		swap(a[index_LargestPivot],a[p]);
		swap(b[index_LargestPivot],b[p]);
		  
		  /* 'Debug Part'
			  cout<<"result"<<endl;
		 print(a,n,n);
		 cout<<"------------"<<endl;
		*/
		
		#pragma omp for
		for(int k=0;k<n;k++)
		{
		
		if(k==p) continue;
		
		for(int i=0;i<n;i++)
		{
			if(p==i)continue;
			
		/* calculate it to decrease propagation of error*/
		a[i][k]=a[i][k]-(a[i][p]*a[p][k])/a[p][p];
		}
		
		a[p][k]=a[p][k]/a[p][p];//divide pivot row with pivot variable a[p][p]
		
		}
		
		#pragma omp for
		for(int i=0;i<n;i++)//Calculate B Matrix and set Pivot Column to '0'
		{
			if(p==i) continue;
			
			b[i]=b[i]-(a[i][p]*b[p])/a[p][p];
		    a[i][p]=0;	
	    }
		
		b[p]=b[p]/a[p][p];//divide current b matrix with priovt value
		a[p][p]=1; //Set Current Pivot to '1'
	}
	
	
	//print(a,n,n);
	 
	return b;
}

void print(double** a,int r,int c)
{
    //Print Result A Matrix
	for(int i=0;i<r;i++)
	{
		for(int j=0;j<c;j++)
		 cout<<a[i][j]<<" ";
		cout<<endl;
	}
}
