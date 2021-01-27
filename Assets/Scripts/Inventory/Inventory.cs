using System.Collections.Generic;

public class Inventory
{
    private List<IItem> _items=new List<IItem>();
    public bool IsEmpty => NumberOfItems==0;
    public int NumberOfItems => _items.Count;

    public void AddItem(IItem item)
    {
        if(_items.Contains(item))
            return;
        _items.Add(item);
    }
}

public class Item
{
}

public interface IItem
{
}


