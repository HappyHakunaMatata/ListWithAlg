using System;
using System.Collections.Generic;
using Enumerator;

public static class Program
{
    static void Main()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.GetEmbeddedList(1).Add(5);
        list.GetEmbeddedList(1).Add(6);
        list.GetEmbeddedList(2).Add(7);
        foreach (var i in list.GetElementsWithDepth())
        {
            Console.WriteLine($"value: {i.value} depth: {i.depth}");
        }

    }
}
