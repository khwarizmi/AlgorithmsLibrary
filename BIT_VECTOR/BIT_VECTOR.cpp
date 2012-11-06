#include "BIT_VECTOR.h"


BIT_VECTOR::BIT_VECTOR(void) : _size(10*32), _capacity(10 * 32)
{
	for(int i = 0; i < size()/BIT_SIZE; i++)
		list.push_back(0);
}

BIT_VECTOR::~BIT_VECTOR(void)
{
}

void BIT_VECTOR::set(int index)
{
	set(index, true);
}

void BIT_VECTOR::set(int index, bool value)
{
	int list_index = index / 32;
	int bit_index = index % 32;
	assert(bit_index <= size());

	if(value)
		list[list_index] = list[list_index] | (1 << bit_index);
	else
		list[list_index] = list[list_index] & ~(1 << bit_index);
}

bool BIT_VECTOR::get(int index)
{
	assert(index <= size());
	int list_index = index / 32;
	int bit_index = index % 32;
	return list[list_index] & (1<<bit_index);
}

int BIT_VECTOR::size()
{
	return _size;
}