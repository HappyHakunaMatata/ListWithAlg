﻿using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Enumerator;
public class ListWithAlgorithm<T> : IEnumerable<T>
{
    private Node<T> tail { get; set; } = null;
    private Node<T> head { get; set; } = null;

    private GetElementByIndexDelegate<T> getElementByIndexDelegate;
    private SetElementByIndexDelegate<T> setElementByIndexDelegate;
    int count;


    public T _data;


    public ListWithAlgorithm()
    {
        getElementByIndexDelegate = GetElementByIndexDFS;
        setElementByIndexDelegate = SetElementByIndexDFS;
    }


    public void Add(T data)
    {
        Node<T> node = new Node<T>(data);
        if (head == null)
        {
            head = node;
        }
        else
        {
            tail.Next = node;
        }
        tail = node;
        count++;
    }

    public ListWithAlgorithm<T> GetEmbeddedList(int index)
    {
        if (index < 0 || head is null)
        {
            throw new IndexOutOfRangeException("Index cannot be negative");
        }

        Node<T> current = head;
        int count = 0;
        while (count != index)
        {
            if (current.Next is null)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
            current = current.Next;
            count += 1;
        }
        
        return current.Container;
    }

    public bool Remove(T data)
    {
        Node<T> current = head;
        Node<T> previous = null;
        while (current is not null && current.data is not null)
        {
            if (current.data.Equals(data))
            {
                if (previous is not null)
                {
                    previous.Next = current.Next;
                    if (current.Next is null)
                    {
                        tail = previous;
                    }
                }
                else
                {
                    head = head?.Next;
                    if (head is null)
                    {
                        tail = null;
                    }
                }
                count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public int Count
    {
        get
        {
            var count = this.count;
            Node<T> current = head;
            while (current is not null)
            {
                count += current.Container.Count;
                current = current.Next;
            }
            return count;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return count == 0;
        }
    }

    public T GetElementByIndex(int index)
    {
        return getElementByIndexDelegate.Invoke(index);
    }

    public T GetElementByIndexBFS(int index)
    {
        int i = 0;
        var current = head;
        Queue<Node<T>> previous = new();
        while (current is not null)
        {
            if (i == index)
            {
                return current.data;
            }
            i += 1;
            if (current.Container is not null && current.Container.head is not null)
            {
                previous.Enqueue(current.Container.head);
            }
            if (current.Next is not null)
            {
                current = current.Next;
            }
            else if (previous.Count != 0)
            {
                current = previous.Dequeue();
            }
            else
            {
                break;
            }
        }
        throw new IndexOutOfRangeException("Index out of range");
    }

    

    public T GetElementByIndexDFS(int index)
    {
        int i = 0;
        var current = head;
        Stack<Node<T>> previous = new();
        while (current is not null)
        {
            if (i == index)
            {
                return current.data;
            }
            i += 1;
            if (current.Container is not null && current.Container.head is not null)
            {
                if (current.Next is not null)
                {
                    previous.Push(current.Next);
                }
                current = current.Container.head;
            }
            else if (current.Next is not null)
            {
                current = current.Next;
            }
            else if (previous.Count != 0)
            {
                current = previous.Pop();
            }
            else
            {
                break;
            }
        }
        throw new IndexOutOfRangeException("Index out of range");
    }

    public bool SetElementByIndex(int index, T Data)
    {
        return setElementByIndexDelegate.Invoke(index, Data);
    }

    public bool SetElementByIndexDFS(int index, T Data)
    {
        int i = 0;
        var current = head;
        Stack<Node<T>> previous = new();
        while (current is not null)
        {
            if (i == index)
            {
                current.data = Data;
                return true;
            }
            i += 1;
            if (current.Container is not null && current.Container.head is not null)
            {
                if (current.Next is not null)
                {
                    previous.Push(current.Next);
                }
                current = current.Container.head;
            }
            else if (current.Next is not null)
            {
                current = current.Next;
            }
            else if (previous.Count != 0)
            {
                current = previous.Pop();
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    public bool SetElementByIndexBFS(int index, T Data)
    {
        int i = 0;
        var current = head;
        Queue<Node<T>> previous = new();
        while (current is not null)
        {
            if (i == index)
            {
                current.data = Data;
                return true;
            }
            i += 1;
            if (current.Container is not null && current.Container.head is not null)
            {
                previous.Enqueue(current.Container.head);
            }
            if (current.Next is not null)
            {
                current = current.Next;
            }
            else if (previous.Count != 0)
            {
                current = previous.Dequeue();
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    public int GetDeepByElement(T data)
    {
        int depth = 0;
        Node<T> current = head;
        Stack<(Node<T> node, int depth)> previous = new();
        while (current is not null)
        {
            if (current.data.Equals(data))
            {
                return depth;
            }

            if (current.Container is not null && current.Container.head is not null)
            {
                if (current.Next is not null)
                {
                    previous.Push((current.Next, depth));
                }
                current = current.Container.head;
                depth++;
            }
            else if (current.Next is not null)
            {
                current = current.Next;
            }
            else if (previous.Count > 0)
            {
                var previousNode = previous.Pop();
                current = previousNode.node;
                depth = previousNode.depth;
            }
            else
            {
                break;
            }
        }
        return -1;
    }

    public T this[int index] {
        get
        {
            return this.GetElementByIndex(index);
        }
        set
        {
            SetElementByIndex(index, value);
        }
    }

    public IEnumerable<(T value, int depth)> GetElementsWithDepth()
    {
        ConcreteAggregate<T> aggregate = new ConcreteAggregate<T>(this);
        return aggregate.GetElementsWithDepth();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        ConcreteAggregate<T> Aggregate = new ConcreteAggregate<T>(this);
        return ((IEnumerable<T>)Aggregate).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        ConcreteAggregate<T> Aggregate = new ConcreteAggregate<T>(this);
        return ((IEnumerable)Aggregate).GetEnumerator();
    }


    public void UseBFS()
    {
        getElementByIndexDelegate = GetElementByIndexBFS;
        setElementByIndexDelegate = SetElementByIndexBFS;
    }

    public void UseDFS()
    {
        getElementByIndexDelegate = GetElementByIndexDFS;
        setElementByIndexDelegate = SetElementByIndexDFS;
    }

    public int Deep
    {
        get
        {
            int max = 0;
            var current = head;
            Stack<Node<T>> previous = new();
            int depth = 0;
            while (current is not null)
            {
                if (current.Container is not null && current.Container.head is not null)
                {
                    if (current.Next is not null)
                    {
                        previous.Push(current.Next);
                    }
                    depth += 1;
                    current = current.Container.head;
                }
                else if (current.Next is not null)
                {
                    current = current.Next;
                }
                else if (previous.Count != 0)
                {
                    depth -= 1;
                    current = previous.Pop();
                }
                else
                {
                    break;
                }
                if (depth > max)
                {
                    max = depth;
                }
            }
            return max;
        }
    }
}

