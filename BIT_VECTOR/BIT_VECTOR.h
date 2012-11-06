#pragma once
#include <vector>
#include <assert.h>

typedef std::vector<unsigned int> BIT;
#define BIT_SIZE 32

class BIT_VECTOR
{
private:
	BIT list;
	int _size, _capacity;

private:
	void increaseCapacity();

public:
	//Constructor
	BIT_VECTOR(void);
	void set(int index);
	void set(int index, bool value);
	bool get(int index);
	int size();
	~BIT_VECTOR(void);
};

