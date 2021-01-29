using NSubstitute;
using NUnit.Framework;

namespace InventoryTest
{
    public class inventories_controller_test
    {
        private Inventory _inventoryForEquipment;
        private Inventory _inventoryForConsumable;
        private Inventory _inventoryForResource;
        private InventoriesController _inventoriesController;
        private IItem _item;
        
        [SetUp]
        public void Init()
        {
            _inventoryForEquipment = new Inventory();
            _inventoryForConsumable = new Inventory();
            _inventoryForResource = new Inventory();
            _inventoriesController = new InventoriesController
                (_inventoryForEquipment,_inventoryForConsumable,_inventoryForResource);
            _item = Substitute.For<IItem>();
        }
        [Test]
        public void ItemType_Equipment_get_add_to_the_correct_inventory()
        {
            
            _item.ItemType.Returns(ItemType.Equipment);
            _inventoriesController.AddItem(_item);
            Assert.AreEqual(1,_inventoryForEquipment.NumberOfItems);
            Assert.AreEqual(0,_inventoryForConsumable.NumberOfItems);
            Assert.AreEqual(0,_inventoryForResource.NumberOfItems);
        }
        
        [Test]
        public void ItemType_Consumable_get_add_to_the_correct_inventory()
        {
            _item.ItemType.Returns(ItemType.Consumable);
            _inventoriesController.AddItem(_item);
            Assert.AreEqual(0,_inventoryForEquipment.NumberOfItems);
            Assert.AreEqual(1,_inventoryForConsumable.NumberOfItems);
            Assert.AreEqual(0,_inventoryForResource.NumberOfItems);
        }
        
        [Test]
        public void ItemType_Resource_get_add_to_the_correct_inventory()
        {
            _item.ItemType.Returns(ItemType.Resource);
            _inventoriesController.AddItem(_item);
            Assert.AreEqual(0,_inventoryForEquipment.NumberOfItems);
            Assert.AreEqual(0,_inventoryForConsumable.NumberOfItems);
            Assert.AreEqual(1,_inventoryForResource.NumberOfItems);
        }
    }
}