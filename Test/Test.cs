using Enumerator;

namespace Test;

public class Test
{

    [Fact]
    public void InitTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        Assert.True(0 == list.Count);
    }

    [Fact]
    public void AddTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        Assert.True(list.Count == 1);
        list.Add(1);
        list.Add(1);
        Assert.True(list.Count == 3);
    }

    [Fact]
    public void RemoveTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(1);
        var removed = list.Remove(1);
        Assert.True(removed);
        removed = list.Remove(2);
        Assert.True(!removed);
        Assert.True(list.Count == 1);
    }

    [Fact]
    public void RemoveFromEmptyTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        var removed = list.Remove(2);
        Assert.True(!removed);
    }

    [Fact]
    public void GetEmbedListTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        Assert.Throws<IndexOutOfRangeException>(() => list.GetEmbeddedList(2));
        list.Add(1);
        var embedded = list.GetEmbeddedList(0);
        embedded.Add(2);
        Assert.Equal(2, list.Count);
        var removed = list.Remove(1);
        Assert.True(removed);
        Assert.Throws<IndexOutOfRangeException>(() => list.GetEmbeddedList(0));
    }

    [Fact]
    public void CountTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.GetEmbeddedList(1).Add(5);
        list.GetEmbeddedList(1).Add(6);
        list.GetEmbeddedList(2).Add(7);
        Assert.Equal(7, list.Count);
    }

    [Fact]
    public void IsEmptyTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        Assert.True(list.IsEmpty);
        list.Add(1);
        Assert.True(!list.IsEmpty);
        var removed = list.Remove(1);
        Assert.True(removed);
        Assert.True(list.IsEmpty);
    }


    [Fact]
    public void GetElementByIndexDFSTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.GetEmbeddedList(0).Add(5);
        list.GetEmbeddedList(0).Add(6);
        list.GetEmbeddedList(2).Add(7);
        list.GetEmbeddedList(2).GetEmbeddedList(0).Add(70);
        Assert.Equal(1, list.GetElementByIndex(0));
        Assert.Equal(5, list.GetElementByIndex(1));
        Assert.Equal(6, list.GetElementByIndex(2));
        Assert.Equal(2, list.GetElementByIndex(3));
        Assert.Equal(3, list.GetElementByIndex(4));
        Assert.Equal(7, list.GetElementByIndex(5));
        Assert.Equal(70, list.GetElementByIndex(6));
        Assert.Equal(4, list.GetElementByIndex(7));
        int[] ints = new int[8];
        int i = 0;
        foreach (var l in list)
        {
            ints[i] = l;
            i += 1;
        }
        Assert.True(ints is [1,5,6,2,3,7,70,4]);
    }

    [Fact]
    public void SetElementByIndexDFSTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.GetEmbeddedList(0).Add(3);
        list.SetElementByIndex(0,10);
        list.SetElementByIndex(1, 8);
        list.SetElementByIndex(2, 4);
        Assert.Equal(10, list.GetElementByIndex(0));
        Assert.Equal(8, list.GetElementByIndex(1));
        Assert.Equal(4, list.GetElementByIndex(2));
        Assert.Throws<IndexOutOfRangeException>(() => list.SetElementByIndex(3, 5));
    }


    [Fact]
    public void IndexerTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.GetEmbeddedList(0).Add(3);
        Assert.Equal(list[0], list.GetElementByIndex(0));
        Assert.Equal(list[1], list.GetElementByIndex(1));
        Assert.Equal(list[2], list.GetElementByIndex(2));
        list[0] = 20;
        Assert.Equal(list[0], list.GetElementByIndex(0));
        list.SetElementByIndex(1, 10);
        Assert.Equal(list[1], list.GetElementByIndex(1));
        Assert.Throws<IndexOutOfRangeException>(() => list[3]);
    }

    [Fact]
    public void UseAlgTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.GetEmbeddedList(0).Add(5);
        list.GetEmbeddedList(0).Add(6);
        list.GetEmbeddedList(2).Add(7);
        list.UseBFS();
        Assert.Equal(3, list.GetElementByIndex(2));
        Assert.Equal(6, list.GetElementByIndex(5));
        list.UseDFS();
        Assert.Equal(6, list.GetElementByIndex(2));
        Assert.Equal(7, list.GetElementByIndex(5));
    }

    [Fact]
    public void BFSTest()
    {
        ListWithAlgorithm<int> list = new ListWithAlgorithm<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        list.GetEmbeddedList(0).Add(5); 
        list.GetEmbeddedList(0).Add(6);
        list.GetEmbeddedList(0).GetEmbeddedList(0).Add(60);
        list.GetEmbeddedList(2).Add(7);
        list.UseBFS();
        int[] ints = new int[8];
        int i = 0;
        foreach (var l in list)
        {
            ints[i] = l;
            i += 1;
        }
        Assert.True(ints is [1, 2, 3, 4, 5, 6, 7, 60]);
    }
}
