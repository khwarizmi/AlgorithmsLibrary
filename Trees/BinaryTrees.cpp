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
#include <assert.h>
#include <time.h>

#define MAX_RANDOM 100

using namespace std;

struct LinkedNode {
	int data;
	LinkedNode* next;
};

struct node{
	int data;
	node* left;
	node* right;
};

/* Linked List Helper Function */
void addLinkedNode(LinkedNode** headRef, int data)
{
	LinkedNode* newHead = new LinkedNode;
	newHead->data = data;
	newHead->next = NULL;

	if(headRef == NULL || *headRef == NULL)
	{
		*headRef = newHead;
	}
	else
	{
		LinkedNode* emptyNode= *headRef;
		while(emptyNode->next != NULL)
			emptyNode = emptyNode->next;
		emptyNode->next = newHead;
	}
}

void removeLinkedNode(LinkedNode** headRef)
{
	if(headRef == NULL || *headRef == NULL)
		return;

	// Only one item
	if((*headRef)->next == NULL)
	{
		delete *headRef;
		*headRef = NULL;
		return;
	}

	LinkedNode* nextPtr = *headRef;
	while(nextPtr->next->next != NULL)
		nextPtr = nextPtr->next;

	// free nextPtr node
	delete nextPtr->next;
	// set next node in nextPtr to Null
	nextPtr->next = NULL;
}

void printLinkedList(LinkedNode* headPtr)
{
	LinkedNode* list = headPtr;
	while(list != NULL)
	{
		cout<<list->data<<" ";
		list = list->next;
	}
	cout<<endl;
}

void freeLinkedList(LinkedNode** headRef)
{
	LinkedNode* tempNode = NULL;
	LinkedNode* headPtr = *headRef;

	while(headPtr != NULL)
	{
		tempNode = headPtr->next;
		delete headPtr;
		headPtr = tempNode;
	}

	*headRef = NULL;
}
/* End of LinkedList Helper Functions */

bool isLeaf(node* treeNode)
{
	return (treeNode->left == NULL && treeNode->right == NULL);
}

void printPreOrder(node* tree)
{
	if(tree == NULL)
		return;

	cout<<tree->data<<" ";
	printPreOrder(tree->left);
	printPreOrder(tree->right);
}

void printInOrder(node* tree)
{
	if(tree == NULL)
		return;

	printInOrder(tree->left);
	cout<<tree->data<<" ";
	printInOrder(tree->right);
}

void printPostOrder(node* tree)
{
	if(tree == NULL)
		return;

	printPostOrder(tree->left);
	printPostOrder(tree->right);
	cout<<tree->data<<" ";
}

node* newNode(int data)
{
	node* nNode = new node;
	nNode->data = data;
	nNode->left = nNode->right = NULL;
	return nNode;
}

int size(node* tree)
{
	if(tree == NULL)
		return 0;
	
	return size(tree->left) + size(tree->right);
}

void nThInOrder_Helper(node* tree, int& k, int* retValue)
{
	if(tree == NULL || k <= 0)
		return;

	nThInOrder_Helper(tree->left, k, retValue);

	if(k == 1)
	{
		*retValue = tree->data;
		k--;
		return;
	}
	else
		k--;
	
	nThInOrder_Helper(tree->right, k, retValue);
}

// 1-Based InOrder Index
int nThInOrder(node* tree, int k)
{
	int nodeValue = INT_MIN;
	int* retValue = &nodeValue;
	nThInOrder_Helper(tree, k, retValue);
	return *retValue;
}

bool lookup(node* tree, int target)
{
	if(tree == NULL)
		return false;

	if(tree->data == target)
		return true;

	if(tree->data >= target)
		return lookup(tree->left, target);
	else
		return lookup(tree->right, target);
}

node* insert(node* tree, int data)
{
	if(tree == NULL)
		return newNode(data);
	
	if(tree->data > data)
		tree->left = insert(tree->left, data);
	else
		tree->right = insert(tree->right, data);
	
	return tree;
}

node* BuildTree(int n)
{
	node* tree = NULL;
	for(int i = 0; i < n; i++)
		tree = insert(tree, i);
	return tree;
}

node* BuildRandomTree(int n)
{
	cout<<"Generating Random Tree ...";
	int tempData;
	node* tree = NULL;
	srand(time(NULL));
	for(int i = 0; i < n; i++)
	{
		tempData = rand()%MAX_RANDOM;
		cout<<" "<<tempData<<" ";
		tree = insert(tree, tempData);
	}
	cout<<"End  of Generation "<<endl;
	return tree;
}

int maxDepth(node* tree)
{
	if(tree == NULL)
		return 0;

	return 1 + max(maxDepth(tree->left), maxDepth(tree->right));
}

int minValue(node* tree)
{
	if(tree == NULL)
		return INT_MAX;

	return (tree->left == NULL) ? tree->data : minValue(tree->left);
}

int hasPathSum(node* node, int sum)
{
	if(node == NULL)
		return (sum == 0);

	return hasPathSum(node->left, sum - node->data) || hasPathSum(node->right, sum - node->data);
}

void printPathsHelper(node* tree, LinkedNode* headRef)
{
	if(tree == NULL)
		return;

	addLinkedNode(&headRef, tree->data);
	if(isLeaf(tree))
		printLinkedList(headRef);
	else
	{
		printPathsHelper(tree->left, headRef);
		printPathsHelper(tree->right, headRef);
	}
	removeLinkedNode(&headRef);
}

void printPaths(node* tree)
{
	if(tree == NULL)
		return;

	LinkedNode* headRef = NULL;
	printPathsHelper(tree, headRef);
}

bool sameTree(node* a, node* b)
{
	if(a == NULL && b == NULL)
		return true;

	if(a == NULL && b != NULL || a != NULL && b == NULL || a->data != b->data)
		return false;

	return sameTree(a->left, b->left) & sameTree(a->right, b->right);
}

void mirror(node* tree)
{
	if(tree == NULL)
		return;

	node* temp = tree->left;
	tree->left = tree->right;
	tree->right = temp;
	mirror(tree->left);
	mirror(tree->right);
}

void duplicateTree(node* tree)
{
	if(tree == NULL)
		return;

	node* dupNode = newNode(tree->data);
	dupNode->left = tree->left;
	tree->left = dupNode;
	// left sub-tree
	duplicateTree(dupNode->left);
	// right sub-tree
	duplicateTree(tree->right);
}

int recCountTree(int key)
{
	if(key <= 1)
		return 1;

	int left = 0, right = 0, sum = 0;
	key--;
	for(int leftNodes = 0; leftNodes<= key; leftNodes++)
	{
		left = recCountTree(leftNodes);
		right = recCountTree(key - leftNodes);
		sum += left*right;
	}

	return sum;
}

int countTrees(int numKeys)
{
	if(numKeys <= 0)
		return 0;

	return recCountTree(numKeys);
}

// PS: NOT MY solution
int countTreesSolution(int numKeys)
{
	assert(numKeys >= 0);
	if(numKeys <= 1)
		return 1;

	else {
			// there will be one value at the root, with whatever remains
			// on the left and right each forming their own subtrees.
			// Iterate through all the values that could be the root...
			int sum = 0;
			int left, right, root;
			for (root=1; root<=numKeys; root++) {
				left = countTreesSolution(root - 1);
				right = countTreesSolution(numKeys - root);
				// number of possible trees with this root == left*right
				sum += left*right;
			}
		return(sum);
	}
}

bool isBST(node* tree)
{
	if(tree == NULL)
		return true;

	bool left = (tree->left == NULL || tree->left->data < tree->data) & isBST(tree->left);
	bool right = (tree->right == NULL || tree->right->data >= tree->data) & isBST(tree->right);

	return left & right;
}


node* treeListProblem(node* nodePtr)
{
	if(nodePtr == NULL)
		return NULL;
	else
		if(nodePtr->left == NULL && nodePtr->right == NULL)
		{
			nodePtr->left = nodePtr;
			nodePtr->right = nodePtr;
			return nodePtr;
		}
	else
		if(nodePtr->left != NULL && nodePtr->right == NULL)
		{
			node* listTail = treeListProblem(nodePtr->left);
			node* listHead = listTail->right;

			nodePtr->right = listHead;
			nodePtr->left = listTail;
			
			listTail->right = nodePtr;
			listHead->left = nodePtr;

			return nodePtr;
		}
	else
		if(nodePtr->left == NULL && nodePtr->right != NULL)
		{
			node* listTail = treeListProblem(nodePtr->right);
			node* listHead = listTail->right;

			nodePtr->right = listHead;
			nodePtr->left = listTail;
			
			listTail->right = nodePtr;
			listHead->left = nodePtr;

			return listTail;
		}
	else
		{
			node* leftSubTree = treeListProblem(nodePtr->left);
			node* rightSubTree = treeListProblem(nodePtr->right);

			// left-sub-tree
			node* leftTail = leftSubTree;
			node* leftHead = leftTail->right;

			// right-sub-tree
			node* rightTail = rightSubTree;
			node* rightHead = rightTail->right;
			
			leftTail->right = nodePtr;
			leftHead->left = rightTail;

			rightTail->right = leftHead;
			rightHead->left = nodePtr;

			nodePtr->right = rightHead;
			nodePtr->left = leftTail;

			return rightTail;
		}

	return NULL;
}


int main() {

	node* tree = BuildRandomTree(5);
	printInOrder(tree);
	cout<<"Testing inOrder "<<endl;
	for(int i = 0; i < 5; i++)
		cout<<nThInOrder(tree, i+1)<<endl;
	cout<<endl;

	cout<<"Pre Order Traversal : "<<endl;
	printPreOrder(tree);
	cout<<endl;

	cout<<"Testing PrintPaths Operation: "<<endl;
	printPaths(tree);
	cout<<endl;

	cout<<"Testing PrintPaths Operation: "<<endl;
	duplicateTree(tree);
	printInOrder(tree);
	cout<<endl;

	cout<<"Testing COunt trees: "<<endl;
	cout<<"Possible Keys: "<<countTrees(4)<<endl;

	for(int i = 1; i <= 10; i++)
		if(countTreesSolution(i) != countTrees(i))
			cout<<i<<" "<<countTrees(i)<<" "<<countTreesSolution(i)<<endl;

	tree = BuildRandomTree(5);
	printInOrder(tree);
	cout<<"Testing inOrder "<<endl;
	
	cout<<"Testing Tree-List Recurssion "<<endl;
	node* endList = treeListProblem(tree)->right;
	node* nextPTr = endList;
	do{
		cout<<nextPTr->data<<" ";
		nextPTr=nextPTr->right;
	}while(nextPTr != endList);

	cout<<endl<<"Finish";

}








