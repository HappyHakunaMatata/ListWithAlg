﻿using System;
using System.Collections;
using System.Collections.Generic;
using Enumerator;

class ConcreteAggregate<T> : IEnumerable<T> //DFS
{

    private ListWithAlgorithm<T> _Container;
    
    public ConcreteAggregate(ListWithAlgorithm<T> list)
    {
        _Container = list;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ConcreteEnumerator<T> GetEnumerator()
    {
        return new ConcreteEnumerator<T>(_Container);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}