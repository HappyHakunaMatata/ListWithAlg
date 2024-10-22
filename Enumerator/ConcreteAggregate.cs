using System.Collections;
using Enumerator;

class ConcreteAggregate<T> : IEnumerable<T>
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

    public IEnumerable<(T value, int depth)> GetElementsWithDepth()
    {
        var iterator = new ConcreteEnumerator<T>(_Container);
        while (iterator.MoveNext())
        {
            yield return iterator.CurrentWithDeep();
        }
    }

}
