﻿using NSubstitute;
using NUnit.Framework;

namespace InventoryTest
{
    public class inventory_test
    {
        private Inventory _inventory;
        [SetUp]
        public void Init()
        {
            _inventory = GetInventory();
        }
        
        [Test]
        public void Inventory_IsEmpty_flag_is_true_at_creation()
        {
            Assert.IsTrue(_inventory.IsEmpty);
        }
        
        [Test]
        public void IsEmpty_flag_is_false_after_add_item_to_inventory()
        {
            var item = Substitute.For<IItem>();
            _inventory.AddItem(item);
            Assert.IsFalse(_inventory.IsEmpty);
        }

        [Test]
        public void NumberOfItems_equal_zero_at_Inventory_creation()
        {
            Assert.AreEqual(0,_inventory.NumberOfItems);
        }
        
        [Test]
        public void NumberOfItems_increase_by_1_each_time_we_add_1_item_to_the_Inventory()
        {
            var item1 = Substitute.For<IItem>();
            var item2 = Substitute.For<IItem>();
            var item3 = Substitute.For<IItem>();

            _inventory.AddItem(item1);
            Assert.AreEqual(1,_inventory.NumberOfItems);
            _inventory.AddItem(item2);
            Assert.AreEqual(2,_inventory.NumberOfItems);
            _inventory.AddItem(item3);
            Assert.AreEqual(3,_inventory.NumberOfItems);
        }

        [Test]
        public void NumberOfItems_dont_increase_if_we_add_the_same_item()
        {
            var item1 = Substitute.For<IItem>();
            _inventory.AddItem(item1);
            var numberOfItemsBefore = _inventory.NumberOfItems;
            _inventory.AddItem(item1);
            Assert.AreEqual(numberOfItemsBefore,_inventory.NumberOfItems);
        }
        
        [Test]
        public void NumberOfItems_dont_increase_if_we_add_the_null_item()
        {
            IItem item = null;
            _inventory.AddItem(item);
            Assert.AreEqual(0,_inventory.NumberOfItems);
        }

        [Test]
        public void CanAddItem_return_false_when_item_is_already_in_Inventory()
        {
            var item1 = Substitute.For<IItem>();
            _inventory.AddItem(item1);
            Assert.IsFalse(_inventory.CanAddItem(item1));
        }
        
        [Test]
        public void CanAddItem_return_true_when_add_a_item_for_the_first_time()
        {
            var item1 = Substitute.For<IItem>();
            Assert.IsTrue(_inventory.CanAddItem(item1));
        }
        [Test]
        public void CanAddItem_return_false_when_add_a_null_item()
        {
            IItem item=null;
            Assert.IsFalse(_inventory.CanAddItem(item));
        }

        private Inventory GetInventory()
        {
            return new Inventory();
        }
    }

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