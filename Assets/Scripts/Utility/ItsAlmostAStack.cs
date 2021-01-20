using System.Collections.Generic;

public class ItsAlmostAStack<T>
{
    private List<T> items = new List<T>();
    public int Count => items.Count;

    public void Push(T item)
    {
        items.Add(item);
    }

    public void Pop()
    {
        T temp = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
    }

    public T Peek()
    {
        return items.Count > 0 ? items[items.Count - 1] : default(T);
    }

    public void Remove(int itemAtPosition)
    {
        items.RemoveAt(itemAtPosition);
    }

    public void Remove(T item)
    {
        items.Remove(item);
    }

    public bool Contains(T item)
    {
        return items.Contains(item);
    }
}