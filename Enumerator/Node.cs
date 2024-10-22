using Enumerator;

public class Node<T>
{

    public Node(T data)
    {
        Container = new ListWithAlgorithm<T>(data);
        this.data = data;
    }

    public ListWithAlgorithm<T> Container { get; set; }
    public T data { get; set; }
    public Node<T> Next { get; set; }
    

    public void Add(T data)
    {
        if (Container is not null)
        {
            Container.Add(data);
        }
    }

    public void Remove(T data)
    {
        if (Container is not null)
        {
            Container.Remove(data);
        }
    }
}
