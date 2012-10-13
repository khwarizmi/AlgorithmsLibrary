using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
    /* A class that implements the circular Queue Datastructure */
    
    class CQueue<T>
    {
        int head, tail, capacity, count;
        T[] circularQueue;

        public CQueue(int QueueCapacity)
        {
            head = tail = count = 0;
            capacity = QueueCapacity;
            circularQueue = new T[capacity];
        }

        public T Top()
        {
            if (count != 0)
                return circularQueue[head];
            else
                throw new Exception("Empty Queue");
        }

        public T Pop()
        {
            T topItem = Top();
            head = (head + 1) % capacity;
            count--;
            return topItem;
        }

        public void Push(T item)
        {
            if (count == capacity)
                throw new Exception("Full Size Queue");
            else
            {
                circularQueue[tail] = item;
                tail = (tail + 1) % capacity;
                count++;
            }
        }

    }
}
