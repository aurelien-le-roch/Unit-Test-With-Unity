using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private List<IItem> _items=new List<IItem>();
    public bool IsEmpty => NumberOfItems==0;
    public int NumberOfItems => _items.Count;

    public void AddItem(IItem item)
    {
        if(_items.Contains(item))
            return;
        if(item==null)
            return;
        _items.Add(item);
    }

    public bool CanAddItem(IItem item)
    {
        if (item == null)
            return false;

        return !_items.Contains(item);
    }
}