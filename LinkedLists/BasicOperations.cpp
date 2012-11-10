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

using namespace std;

struct node{
	int data;
	node* next;
};

int length(node* listHead)
{
	int sz;
	for(sz = 0; listHead != NULL; sz++, listHead = listHead->next);
	return sz;
}

node* BuildList(int n)
{
	assert(n >= 1);
	node *listHead = NULL, *listPointer = NULL;
	for(int i = 1; i <= n; i++)
	{
		if(listHead == NULL)
		{
			listHead = new node;
			listPointer = listHead;
		}
		else
		{
			listPointer->next = new node;
			listPointer = listPointer->next;
		}
		listPointer->data = i;
	}
	listPointer->next = NULL;

	return listHead;
}

node* BuildListReverse(int n)
{
	assert(n >= 1);
	node *listHead = NULL, *listPointer = NULL;
	for(int i = n; i >= 1; i--)
	{
		if(listHead == NULL)
		{
			listHead = new node;
			listPointer = listHead;
		}
		else
		{
			listPointer->next = new node;
			listPointer = listPointer->next;
		}
		listPointer->data = i;
	}
	listPointer->next = NULL;

	return listHead;
}

void printList(node* listHead)
{
	while(listHead != NULL)
	{
		printf("%i->", (*listHead).data);
		listHead = listHead->next;
	}
	printf("\n");
}

void push(node** headRef, int newData)
{
	if(*headRef == NULL)
	{
		*headRef = new node;
		(*headRef)->next = NULL;
		(*headRef)->data = newData;
	}
	else
	{
		node* newHead = new node;
		newHead->next = *headRef;
		newHead->data = newData;
		*headRef = newHead;
	}
}

void pushTail(node** headRef, int newData)
{
	if(*headRef == NULL)
	{
		*headRef = new node;
		(*headRef)->data = newData;
	}
	else
	{
		node* emptyNode = *headRef;
		while(emptyNode->next != NULL)
			emptyNode = emptyNode->next;
		emptyNode->next = new node;
		emptyNode = emptyNode->next;
		emptyNode->data = newData;
		emptyNode->next = NULL;
	}
}

node* pushWithReference()
{
	node* headRef = NULL;
	node** lastPtrRef = &headRef;

	for(int i = 0; i < 6; i++)
	{
		push(lastPtrRef, i);
		lastPtrRef = &((*lastPtrRef)->next);
	}

	return headRef;
}

int count(node* headRef, int searchFor)
{
	int found = 0;
	while(headRef != NULL)
	{
		if(headRef->data == searchFor) found++;
		headRef = headRef->next;
	}
	return found;
}

int GetNth(node* headRef, int index)
{
	int count = length(headRef);
	assert(index >= 0 && index < count);

	while(index-- > 0)
		headRef = headRef->next;
	return headRef->data;
}

void DeleteList(node** headRef)
{
	node* nextPtr = *headRef;
	while(*headRef != NULL)
	{
		nextPtr = (*headRef)->next;
		delete *headRef;
		*headRef = NULL;
		headRef = &nextPtr;
	}
}

int pop(node** headRef)
{
	assert(*headRef != NULL);
	int result = (*headRef)->data;
	node* newHead = (*headRef)->next;
	delete *headRef;
	*headRef = newHead;
	return result;
}

void InsertNth(node** headRef, int index, int newData)
{
	int listLength = length(*headRef);
	assert(index >=0 && index <= listLength);

	if(index == 0)
	{
		push(headRef, newData);
	}
	else
	{
		node** headPtr = headRef;
		while(index-- > 0)
			headPtr = &((*headPtr)->next);
		push(headPtr, newData);
	}	
}

void sortedInsert(node **headRef, node *item)
{
	node** headPtr = headRef;
	node** prevPtr = NULL;

	while(*headPtr != NULL && (*headPtr)->data < item->data)
	{
		prevPtr = headPtr;
		headPtr = &((*headPtr)->next);
	}

	if(prevPtr == NULL)
	{
		item->next = *headRef;
		*headRef = item;
	}
	else
	{
		item->next = *headPtr;
		(*prevPtr)->next = item;
	}
}

void Append(node** a, node**b)
{
	if(*b == NULL)
		return;
	if(*a == NULL)
	{
		*a = *b;
		*b = NULL;
		return;
	}

	node** aPtr = a;
	while((*aPtr)->next != NULL)
		aPtr = &((*aPtr)->next);
	
	(*aPtr)->next = *b;
	*b = NULL;
}

void FrontBackSplit(node* head, node** first, node** second)
{
	if(head == NULL)
	{
		*first = *second = NULL;
		return;
	}

	int count = length(head);
	if(count == 1)
	{
		*first = head;
		*second = NULL;
		return;
	}

	node* splitNode = head;
	for(int i = 1; i < ceil(count/2.0); i++)
		splitNode= splitNode->next;

	*first = head;
	*second = splitNode->next;
	splitNode->next = NULL;
}

void RemoveDuplicates(node* headPtr)
{
	if(headPtr == NULL)
		return;

	while(headPtr->next != NULL)
	{
		if(headPtr->data == headPtr->next->data)
			headPtr->next = headPtr->next->next;
		else
			headPtr = headPtr->next;
	}
}

void MoveNode(node** sourceRef, node** destRef)
{
	node* move = *destRef;
	if(move == NULL)
		return;

	if(*sourceRef == NULL)
	{
		*destRef = (*destRef)->next;
		*sourceRef = move;
		(*sourceRef)->next = NULL;
	}
	else
	{
		*destRef = (*destRef)->next;
		move->next = (*sourceRef);
		*sourceRef = move;
	}
}

void AlternatingSplit(node* source, node** aRef, node** bRef) 
{
	if(source == NULL)
		return;

	bool takeFirst= true;
	node* tempNode = NULL;
	node** firstList = aRef, **secondList = bRef;

	while(source != NULL)
	{
		tempNode = source->next;
		if(takeFirst)
		{
			if(*firstList == NULL){ *firstList = source;}
			else { (*firstList)->next = source; firstList = &((*firstList)->next);}
		}
		else
		{
			if(*secondList == NULL){ *secondList = source;}
			else { (*secondList)->next = source; secondList = &((*secondList)->next);}
		}
		source = tempNode;
		takeFirst = !takeFirst;
	}

	(*firstList)->next = NULL;
	(*secondList)->next = NULL;
}

node* ShuffleMerge(node* a, node* b)
{
	node* head = (a != NULL? a : b);
	node* nextPtr = head;
	if(head == NULL)
		return NULL;
	if(a == NULL)
		return b;
	if(b == NULL)
		return a;

	bool takeFirst = false;
	a = a->next;
	while(a != NULL || b != NULL)
	{
		if(takeFirst && a != NULL)
		{
			nextPtr->next = a;
			a = a->next;
		}
		else if(!takeFirst && b != NULL)
		{
			nextPtr->next = b;
			b = b->next;
		}

		takeFirst = !takeFirst;
		nextPtr = nextPtr->next;			
	}

	return head;
}

node* SortedMerge(node* a, node* b)
{
	if(a == NULL)
		return b;
	if(b == NULL)
		return a;

	node* head = NULL;
	node* nextPtr = NULL;
	if(a->data <= b->data)
	{
		head = a;
		a= a->next;
	}
	else
	{
		head = b;
		b = b->next;
	}

	nextPtr = head;
	while(a != NULL || b != NULL)
	{
		if(b == NULL || (a!= NULL && a->data <= b->data))
		{
			nextPtr->next = a;
			a = a->next;
		}
		else if(a == NULL || (b!=NULL && a->data > b->data))
		{
			nextPtr->next = b;
			b = b->next;
		}
		nextPtr = nextPtr->next;
	}

	return head;
}

void Reverse(node** headRef)
{
	if(headRef == NULL || *headRef == NULL)
		return;

	node *newHead=NULL, *tempHead=NULL, *oldHead = *headRef;

	while(oldHead != NULL)
	{
		tempHead = oldHead->next;
		oldHead->next = newHead;
		newHead = oldHead;
		oldHead = tempHead;
	}

	*headRef = newHead;
}

void ReverseRecursive(node** headRef)
{
	if(headRef == NULL || *headRef == NULL)
		return;

	if((*headRef)->next == NULL)
		return;

	node* current = *headRef;
	node* next = current->next;
	
	if(next == NULL) return;

	ReverseRecursive(&next);

	current->next->next = current;
	current->next = NULL;
	*headRef = next;
}

node* SortedIntersect(node* a, node* b)
{
	if(a == NULL || b == NULL)
		return NULL;

	node* iList = NULL;
	node* ptrList = NULL;

	while(a != NULL && b != NULL)
	{
		if(a->data == b->data)
		{
			if(iList == NULL)
			{
				iList = a;
				ptrList = iList;
				a =a->next;
				b = b->next;
			}
			else
			{
				ptrList->next = a;
				a = a->next;
				b = b->next;
				ptrList = ptrList->next; 
			}
		}
		else
			if(a->data > b->data)
				b = b->next;
			else
				a = a->next;
	}

	ptrList->next = NULL;
	return iList;
}

void MergeSort(node** headRef)
{
	if(headRef == NULL || *headRef == NULL)
		return;

	node *a, *b;
	FrontBackSplit(*headRef, &a, &b);

	if(b == NULL)
	{
		*headRef = a;
		return;
	}

	MergeSort(&a);
	MergeSort(&b);

	*headRef = SortedMerge(a,b);
}

void swap(int *a, int*b)
{
	int temp = *a;
	*a = *b;
	*b = temp;
}

int main() {

	printf("Test BuildList, PrintList: ");
	printList(BuildList(3));
	printf("Test Push: ");
	node* ListStart = NULL;
	push(&ListStart, 1);
	push(&ListStart, 2);
	push(&(ListStart->next), 3);
	pushTail(&ListStart, 4);
	printList(ListStart);
	
	printf("Test push with Reference : ");
	node* headRef = pushWithReference();
	printList(headRef);
	
	printf("Testing GetNth Element : ");
	printf("first:%i Third:%i Last:%i", GetNth(headRef, 0), GetNth(headRef, 2), GetNth(headRef, 5));
	
	printf("Testing Delete List : ");
	DeleteList(&headRef);
	printList(headRef);
	
	printf("Testing Pop Operation: ");
	node* popTestNode = BuildList(3);
	printf("%i\n", pop(&popTestNode));
	printList(popTestNode);

	printf("Testing Insert-Nth Operation: ");
	InsertNth(&popTestNode, 0, 0);
	printList(popTestNode);
	InsertNth(&popTestNode, length(popTestNode), length(popTestNode) + 1);
	printList(popTestNode);
	InsertNth(&popTestNode, 2, 10);
	printList(popTestNode);
	
	printf("Testing Insert-Nth Operation: ");
	node* newNode = new node;
	newNode->data = 40;
	sortedInsert(&popTestNode, newNode);
	printList(popTestNode);
	
	/*
	printf("Testing Append Operation: \n");
	node* emptyList = NULL;
	printList(ListStart);
	printList(popTestNode);
	Append(&emptyList, &ListStart);
	printList(emptyList);
	*/

	printf("Testing Split Operation: \n");
	node *first, *second;
	printList(popTestNode);
	FrontBackSplit(popTestNode, &first, &second);
	printList(first);
	printList(second);

	printf("Testing Remove Duplicates Operation Operation: \n");
	node* testDuplicatesList = BuildList(5);
	printList(testDuplicatesList);
	push(&(testDuplicatesList->next), 1);
	printList(testDuplicatesList);
	RemoveDuplicates(testDuplicatesList);
	printList(testDuplicatesList);

	printf("Testing Move Node Operation Operation: \n");
	node* testMoveNodeListFirst = BuildList(5);
	printList(testMoveNodeListFirst);
	node* testMoveNodeListSecond = BuildList(5);
	printList(testMoveNodeListSecond);
	MoveNode(&testMoveNodeListFirst, &testMoveNodeListSecond);
	printList(testMoveNodeListFirst);
	printList(testMoveNodeListSecond);
	
	printf("Testing Alternate Splitting Operation: \n");
	node *newFirstList = NULL, *newSecondList = NULL;
	printList(testMoveNodeListFirst);	
	AlternatingSplit(testMoveNodeListFirst, &newFirstList, &newSecondList);
	printList(newFirstList);
	printList(newSecondList);

	/*
	printf("Testing ShuffleMerge Operation: \n");
	printList(newFirstList);	
	printList(newSecondList);	
	printList(ShuffleMerge(newFirstList, newSecondList));
	*/

	printf("Testing SortedMerge Operation: \n");
	printList(newFirstList);	
	printList(newSecondList);	
	node* newll = SortedMerge(newFirstList, newSecondList);
	printList(newll);
	
	printf("Testing Reverse Operation: \n");
	printList(newll);
	Reverse(&newll);
	printList(newll);

	
	printf("Testing ReverseRecursive Operation: \n");
	printList(newll);
	ReverseRecursive(&newll);
	printList(newll);

	
	printf("Testing SortedIntersect Operation: \n");
	node* a = BuildList(3), *b = BuildList(6);
	printList(a);
	printList(b);
	printList(SortedIntersect(a, b));

	printf("Testing MergeSort Operation: \n");
    a = BuildListReverse(3);
	printList(a);
	MergeSort(&a);
	printList(a);
}








