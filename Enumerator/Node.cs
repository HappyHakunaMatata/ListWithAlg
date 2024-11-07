using System.Reflection;
using Enumerator;

internal sealed class Node<T>
{

    public Node(T data)
    {
        Container = new ListWithAlgorithm<T>();
        this.data = data;
    }

    public ListWithAlgorithm<T> Container { get; set; }
    public T data { get; set; }
    public Node<T> Next { get; set; }
    
}
