using System;
using System.Collections;
using System.Collections.Generic;
namespace Enumerator;
public class ListWithAlgorithm<T> : IEnumerable<T>
{
    private Node<T> tail { get; set; } = null;
    private Node<T> head { get; set; } = null;
    int count;


    public T _data;


    public ListWithAlgorithm() { }

    public ListWithAlgorithm(T data)
    {
        _data = data;
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
                break;
            }
        }
        throw new IndexOutOfRangeException("Index out of range");
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
}

