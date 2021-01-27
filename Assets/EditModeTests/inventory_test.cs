using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

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
        
        

        private Inventory GetInventory()
        {
            return new Inventory();
        }
    }
}

