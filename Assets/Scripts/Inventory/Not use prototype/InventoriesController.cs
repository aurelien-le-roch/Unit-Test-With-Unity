using System.Collections.Generic;

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