﻿using System.Collections;
using Enumerator;

class ConcreteEnumerator<T> : IEnumerator<T>
{
    public ListWithAlgorithm<T> _Container;

    int position = -1;

    public ConcreteEnumerator(ListWithAlgorithm<T> list)
    {
        _Container = list;
    }

    public bool MoveNext()
    {
        position++;
        return (position < _Container.Count);
    }

    public void Reset()
    {
        position = -1;
    }

    public void Dispose() { }

    T IEnumerator<T>.Current
    {
        get
        {
            return Current;
        }
    }

    public T Current
    {
        get
        {
            try
            {
                return _Container.GetElementByIndex(position);
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }

    object IEnumerator.Current => (object)Current;

    public int CurrentDeep
    {
        get
        {
            return _Container.GetDeepByElement(Current);
        }
    }

    public (T value, int depth) CurrentWithDeep()
    {
        return (Current, CurrentDeep);
    }
}