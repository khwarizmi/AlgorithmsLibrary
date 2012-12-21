#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>
#include <set>
#include <queue>
#include <stdio.h>
#include <string.h>
#include <math.h>
#include <map>

using namespace std;

const int INF = 2000000000;

#define fileMST  "edges.txt"

const int n= 501;
int matrix[n][n];
bool intree[n];
int costtree[n];

int mst(int start)
{
    int nodes = 1;
    int nextNode = start;
	long long sum= 0;

    //init-start node
    costtree[nextNode] = 0;

	set<int> vis;
    while(nodes < n)
    {
		intree[nextNode]= true;
        //relax over all edges
        for(int i = 1; i < n; i++)
            if(matrix[nextNode][i] != INF && !intree[i])
                costtree[i] = min(costtree[i], matrix[nextNode][i]);

        //fetch-smallest element
        for(int i = 1; i < n; i++)
            if((!intree[i] && costtree[i] < costtree[nextNode]) || intree[nextNode])
                nextNode = i;
		
        if(intree[nextNode])
            break;

		//inc fetched count
        nodes++;
    }

    for(int i = 1; i < n; i++)
		if(intree[i])
			sum += costtree[i];

    return sum;
}

void solvemst() {

    ifstream fin (fileMST);

	if(!fin)
		cout<<"File Not Open!"<<endl;

	for(int i=0; i<n; i++)
	{
		intree[i]= false;
		costtree[i]= INF;
		for(int j=0; j<n; j++)
			matrix[i][j]= INF;
	}

    long long cost = 0;
    int nodes, edges, i, j, edgeCost;
	fin>>nodes>>edges;
	while(edges > 0)
	{
		fin>>i>>j>>edgeCost;
		matrix[i][j]= min(matrix[i][j], edgeCost); 
		matrix[j][i]= matrix[i][j];
		if(i == 1 && j == 2)
			cout<<matrix[1][2]<<" "<<endl;
		edges--;
	}

	int startNode= 1;
    cost = mst(startNode);
    cout<<cost<<endl;
}

int main()
{
	solvemst();
}





