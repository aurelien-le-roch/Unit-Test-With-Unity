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

public class ResourceInventory
{
    private Dictionary<ResourceDefinition,int> _resources = new Dictionary<ResourceDefinition, int>();

    public void Add(ResourceDefinition resourceDefinition,int amount)
    {
        if (_resources.ContainsKey(resourceDefinition))
        {
            _resources[resourceDefinition]+=amount;
        }
        else
        {
            _resources.Add(resourceDefinition,amount);
        }
    }
}
public class InventoriesController
{
    private Dictionary<ItemType, Inventory> _inventories;
    
    public InventoriesController(Inventory forEquipment,Inventory forConsumable,Inventory forResource)
    {
        _inventories = new Dictionary<ItemType, Inventory>
        {
            {ItemType.Equipment, forEquipment},
            {ItemType.Consumable, forConsumable},
            {ItemType.Resource, forResource}
        };
    }
    public InventoriesController()
    {
        _inventories = new Dictionary<ItemType, Inventory>
        {
            {ItemType.Equipment, new Inventory()},
            {ItemType.Consumable, new Inventory()},
            {ItemType.Resource, new Inventory()}
        };
    }
    public bool CanAddItem(IItem item)
    {
        return _inventories[item.ItemType].CanAddItem(item);
    }

    public void AddItem(IItem item)
    {
        _inventories[item.ItemType].AddItem(item);
    }

    public int GetNumberOfItemByType(ItemType itemType)
    {
        return _inventories[itemType].NumberOfItems;
    }
}

public class Item : IItem
{
    public ItemType ItemType { get; }
}

public interface IItem
{
    ItemType ItemType { get; }
}

public enum ItemType
{
    Equipment,
    Consumable,
    Resource,
}


