using System.Collections;
using System.Reflection;
using Enumerator;

class ConcreteEnumerator<T> : IEnumerator<T>
{
    public ListWithAlgorithm<T> _Container;
    int position = -1;
    private MethodInfo baseDiposeMethod = null;

    public ConcreteEnumerator(ListWithAlgorithm<T> list)
    {
        _Container = list;
    }

    private ConcreteEnumerator()
    {
        var type = typeof(T);
        var interfaces = type.GetInterfaces();
        if (interfaces.Contains(typeof(IDisposable)))
        {
            baseDiposeMethod = type.GetMethod(nameof(Dispose), BindingFlags.Public);
        }
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
            if (baseDiposeMethod is not null)
            {
                baseDiposeMethod.Invoke(_Container., null);
            }
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