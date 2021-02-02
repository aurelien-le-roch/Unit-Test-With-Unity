using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

namespace InventoryTest
{
    public class resource_inventory
    { 
        private ResourceInventory _resourceInventory;
        private ResourceDefinition _resourceDefinition1;
        private ResourceDefinition _resourceDefinition2;
        private ResourceDefinition _resourceDefinition3;
        
        [SetUp]
        public void Init()
        {
            _resourceInventory = GetInventory();
            _resourceDefinition1= Helpers.GetResourceDefinition1();
            _resourceDefinition2= Helpers.GetResourceDefinition2();
            _resourceDefinition3= Helpers.GetResourceDefinition3();
        }
        
        
        [Test]
        public void inventory_list_count_equal_0_after_creation()
        {
            Assert.AreEqual(0,_resourceInventory.ResourcesList.Count);
        }
        
        
        [Test]
        public void inventory_list_count_increase_by_one_after_Add_method_call()
        {
            _resourceInventory.Add(_resourceDefinition1,2);
            Assert.AreEqual(1,_resourceInventory.ResourcesList.Count);
            _resourceInventory.Add(_resourceDefinition1,3);
            Assert.AreEqual(1,_resourceInventory.ResourcesList.Count);
            _resourceInventory.Add(_resourceDefinition2,1);
            Assert.AreEqual(2,_resourceInventory.ResourcesList.Count);
            _resourceInventory.Add(_resourceDefinition3,2);
            Assert.AreEqual(3,_resourceInventory.ResourcesList.Count);
        }

        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_once()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForInventories>();
            _resourceInventory.OnResourceChange += dummySubscriber.HandleOnResourceChange;
            
            _resourceInventory.Add(_resourceDefinition1,1);
            dummySubscriber.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
        }
        
        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_multiple_time_with_same_resource()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForInventories>();
            _resourceInventory.OnResourceChange += dummySubscriber.HandleOnResourceChange;
            
            _resourceInventory.Add(_resourceDefinition1,1);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventory.Add(_resourceDefinition1,3);
            dummySubscriber.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventory.Add(_resourceDefinition1,2);
            dummySubscriber.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
        }
        
        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_multiple_time_with_different_resource()
        {
            var dummySubscriber = Substitute.For<IDummySubscriberForInventories>();
            _resourceInventory.OnResourceChange += dummySubscriber.HandleOnResourceChange;
            
            _resourceInventory.Add(_resourceDefinition1,1);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventory.Add(_resourceDefinition2,2);
            dummySubscriber.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventory.Add(_resourceDefinition3,4);
            dummySubscriber.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
        }
        
        [Test]
        public void resource_with_the_same_definition_are_added_with_the_correct_amount_()
        {
            _resourceInventory.Add(_resourceDefinition1,1);
            Assert.AreEqual(1,_resourceInventory.GetAmountOf(_resourceDefinition1));
            _resourceInventory.Add(_resourceDefinition1,2);
            Assert.AreEqual(3,_resourceInventory.GetAmountOf(_resourceDefinition1));
            _resourceInventory.Add(_resourceDefinition1,4);
            Assert.AreEqual(7,_resourceInventory.GetAmountOf(_resourceDefinition1));
        }
        
        [Test]
        public void resource_with_different_definition_are_added_with_the_correct_amount_()
        {
            _resourceInventory.Add(_resourceDefinition1,1);
            Assert.AreEqual(1,_resourceInventory.GetAmountOf(_resourceDefinition1));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition3));
            _resourceInventory.Add(_resourceDefinition2,2);
            Assert.AreEqual(1,_resourceInventory.GetAmountOf(_resourceDefinition1));
            Assert.AreEqual(2,_resourceInventory.GetAmountOf(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition3));
            _resourceInventory.Add(_resourceDefinition3,4);
            Assert.AreEqual(1,_resourceInventory.GetAmountOf(_resourceDefinition1));
            Assert.AreEqual(2,_resourceInventory.GetAmountOf(_resourceDefinition2));
            Assert.AreEqual(4,_resourceInventory.GetAmountOf(_resourceDefinition3));
        }

        [TestCase(-2)]
        [TestCase(-6)]
        public void Add_negative_amount_add_positive_amount(int amountAdded)
        {
            _resourceInventory.Add(_resourceDefinition1,amountAdded);
            Assert.AreEqual(Mathf.Abs(amountAdded),_resourceInventory.GetAmountOf(_resourceDefinition1));
        }

        [Test]
        public void Remove_a_positive_value_reduce_amount()
        {
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.Add(_resourceDefinition2,8);
            
            _resourceInventory.Remove(_resourceDefinition1, 3);
            _resourceInventory.Remove(_resourceDefinition2, 10);
            _resourceInventory.Remove(_resourceDefinition3, 5);
            
            Assert.AreEqual(2,_resourceInventory.GetAmountOf(_resourceDefinition1));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition3));
        }
        
        [Test]
        public void Remove_a_negative_value_reduce_amount()
        {
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.Add(_resourceDefinition2,8);
            
            _resourceInventory.Remove(_resourceDefinition1, -3);
            _resourceInventory.Remove(_resourceDefinition2, -10);
            _resourceInventory.Remove(_resourceDefinition3, -5);
            
            Assert.AreEqual(2,_resourceInventory.GetAmountOf(_resourceDefinition1));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition3));
        }

        [Test]
        public void remove_all_resource_from_list_when_amount_hit_0()
        {
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.Add(_resourceDefinition2,8);
            _resourceInventory.Add(_resourceDefinition3,12);
            
            _resourceInventory.Remove(_resourceDefinition1, 99);
            _resourceInventory.Remove(_resourceDefinition3, 99);
            _resourceInventory.Remove(_resourceDefinition2, 99);
            
            Assert.AreEqual(0,_resourceInventory.ResourcesList.Count);
        }

        [Test]
        public void Remove_raise_OnResourceChange_event()
        {
            var dummy = Substitute.For<IDummySubscriberForInventories>();
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.OnResourceChange += dummy.HandleOnResourceChange;
            _resourceInventory.Remove(_resourceDefinition1, 3);
            dummy.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
        }

        [Test]
        public void RemoveAll_remove_all_resources()
        {
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.Add(_resourceDefinition2,8);
            _resourceInventory.Add(_resourceDefinition3,12);
            
            _resourceInventory.RemoveAll();
            Assert.AreEqual(0,_resourceInventory.ResourcesList.Count);
        }
        
        [Test]
        public void RemoveAll_raise_OnResourceChange_event()
        {
            _resourceInventory.Add(_resourceDefinition1,5);
            _resourceInventory.Add(_resourceDefinition2,8);
            _resourceInventory.Add(_resourceDefinition3,12);
            var dummy = Substitute.For<IDummySubscriberForInventories>();
            _resourceInventory.OnResourceChange += dummy.HandleOnResourceChange;
            
            _resourceInventory.RemoveAll();
            
            dummy.Received().HandleOnResourceChange(_resourceInventory.ResourcesList);
        }
        
        [Test]
        public void GetResourceAmount_return_correct_amount_when_call_with_already_added_resource()
        {
            _resourceInventory.Add(_resourceDefinition1,1);

            var resourceInList = _resourceInventory.ResourcesList[0].Amount;
            
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition1),resourceInList);
            _resourceInventory.Add(_resourceDefinition1,4);
            resourceInList = _resourceInventory.ResourcesList[0].Amount;
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition1),resourceInList);
        }
        
        [Test]
        public void GetResourceAmount_return_correct_amount_when_call_with_different_added_resource()
        {
            _resourceInventory.Add(_resourceDefinition1,1);
            _resourceInventory.Add(_resourceDefinition2,6);

            var resource1InList = _resourceInventory.ResourcesList[0].Amount;
            var resource2InList = _resourceInventory.ResourcesList[1].Amount;
            
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition1),resource1InList);
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition2),resource2InList);
            _resourceInventory.Add(_resourceDefinition2,4);
            resource2InList = _resourceInventory.ResourcesList[1].Amount;
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition1),resource1InList);
            Assert.AreEqual(_resourceInventory.GetAmountOf(_resourceDefinition2),resource2InList);
        }
        
        [Test]
        public void GetResourceAmount_return_0__when_call_with_a_resource_that_not_in_inventory()
        {
            _resourceInventory.Add(_resourceDefinition1,1);
            Assert.AreEqual(0,_resourceInventory.GetAmountOf(_resourceDefinition2));
        }
        
        private ResourceInventory GetInventory()
        {
            return new ResourceInventory();
        }
    }

    public interface IDummySubscriberForInventories
    {
        void HandleOnResourceChange(List<ResourceDefinitionWithAmount> resources);
        void HandleOnRecipeChange(List<RecipeDefinitionWithAmount> recipes);
    }
}