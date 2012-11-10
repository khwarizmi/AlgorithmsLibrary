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
	
}








