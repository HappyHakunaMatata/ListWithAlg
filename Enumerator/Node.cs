using System.Reflection;
using Enumerator;

public class Node<T>: IDisposable
{

    public Node(T data)
    {
        Container = new ListWithAlgorithm<T>();
        this.data = data;

        if (data is IDisposable disposable)
        {
            baseDisposeMethod = disposable;
        }
    }

    public ListWithAlgorithm<T> Container { get; set; }
    public T data { get; set; }
    public Node<T> Next { get; set; }
    private IDisposable baseDisposeMethod = null;




    private bool disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }
        if (disposing)
        {
            baseDisposeMethod?.Dispose();
            if (Container is not null && !Container.IsEmpty)
            {
                Container?.Dispose();
            }
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
