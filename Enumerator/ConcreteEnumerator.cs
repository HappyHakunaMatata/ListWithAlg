using System.Collections;
using System.Reflection;
using Enumerator;

internal class ConcreteEnumerator<T> : IEnumerator<T>
{
    public ListWithAlgorithm<T> _Container;
    private int position = -1;
    

    public ConcreteEnumerator(ListWithAlgorithm<T> list)
    {
        _Container = list;
    }

   

    public void Dispose() { }

    public bool MoveNext()
    {
        position++;
        return (position < _Container.Count);
    }

    public void Reset()
    {
        position = -1;
    }

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