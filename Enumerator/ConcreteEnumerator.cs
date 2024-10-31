using System.Collections;
using System.Reflection;
using Enumerator;

class ConcreteEnumerator<T> : IEnumerator<T>, IDisposable
{
    public ListWithAlgorithm<T> _Container;
    private int position = -1;
    

    public ConcreteEnumerator(ListWithAlgorithm<T> list)
    {
        _Container = list;
    }

    private bool disposed = false;
    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }
        if (disposing)
        {
            _Container.Dispose();
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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