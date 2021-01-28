﻿using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using Object = UnityEngine.Object;

namespace InventoryTest
{
    public class resource_inventory_list
    {
        private ResourceInventoryList _resourceInventoryList;
        private ResourceDefinition _resourceDefinition1;
        private ResourceDefinition _resourceDefinition2;
        private ResourceDefinition _resourceDefinition3;
        
        [SetUp]
        public void Init()
        {
            _resourceInventoryList = GetInventory();
            _resourceDefinition1= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource1.asset");
            _resourceDefinition2= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource2.asset");
            _resourceDefinition3= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource3.asset");
        }
        
        [Test]
        public void inventory_list_count_equal_0_after_creation()
        {
            Assert.AreEqual(0,_resourceInventoryList.ResourcesList.Count);
        }
        
        [Test]
        public void inventory_list_count_increase_by_one_after_Add_method_call()
        {
            _resourceInventoryList.Add(_resourceDefinition1,2);
            Assert.AreEqual(1,_resourceInventoryList.ResourcesList.Count);
            _resourceInventoryList.Add(_resourceDefinition1,3);
            Assert.AreEqual(1,_resourceInventoryList.ResourcesList.Count);
            _resourceInventoryList.Add(_resourceDefinition2,1);
            Assert.AreEqual(2,_resourceInventoryList.ResourcesList.Count);
            _resourceInventoryList.Add(_resourceDefinition3,2);
            Assert.AreEqual(3,_resourceInventoryList.ResourcesList.Count);
        }

        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_once()
        {
            var dummySubscriber = Substitute.For<IDummySubscriber>();
            _resourceInventoryList.OnResourceAdded += dummySubscriber.HandleOnResourceAdded;
            
            _resourceInventoryList.Add(_resourceDefinition1,1);
            dummySubscriber.Received().HandleOnResourceAdded(_resourceInventoryList.ResourcesList);
        }
        
        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_multiple_time_with_same_resource()
        {
            var dummySubscriber = Substitute.For<IDummySubscriber>();
            _resourceInventoryList.OnResourceAdded += dummySubscriber.HandleOnResourceAdded;
            
            _resourceInventoryList.Add(_resourceDefinition1,1);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventoryList.Add(_resourceDefinition1,3);
            dummySubscriber.Received().HandleOnResourceAdded(_resourceInventoryList.ResourcesList);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventoryList.Add(_resourceDefinition1,2);
            dummySubscriber.Received().HandleOnResourceAdded(_resourceInventoryList.ResourcesList);
        }
        
        [Test]
        public void OnResourceAdded_event_get_raise_after_Add_method_is_call_multiple_time_with_different_resource()
        {
            var dummySubscriber = Substitute.For<IDummySubscriber>();
            _resourceInventoryList.OnResourceAdded += dummySubscriber.HandleOnResourceAdded;
            
            _resourceInventoryList.Add(_resourceDefinition1,1);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventoryList.Add(_resourceDefinition2,2);
            dummySubscriber.Received().HandleOnResourceAdded(_resourceInventoryList.ResourcesList);
            dummySubscriber.ClearReceivedCalls();
            _resourceInventoryList.Add(_resourceDefinition3,4);
            dummySubscriber.Received().HandleOnResourceAdded(_resourceInventoryList.ResourcesList);
        }
        
        [Test]
        public void GetResourceAmount_return_correct_amount_when_call_with_already_added_resource()
        {
            _resourceInventoryList.Add(_resourceDefinition1,1);

            var resourceInList = _resourceInventoryList.ResourcesList[0].Amount;
            
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition1),resourceInList);
            _resourceInventoryList.Add(_resourceDefinition1,4);
            resourceInList = _resourceInventoryList.ResourcesList[0].Amount;
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition1),resourceInList);
        }
        
        [Test]
        public void GetResourceAmount_return_correct_amount_when_call_with_different_added_resource()
        {
            _resourceInventoryList.Add(_resourceDefinition1,1);
            _resourceInventoryList.Add(_resourceDefinition2,6);

            var resource1InList = _resourceInventoryList.ResourcesList[0].Amount;
            var resource2InList = _resourceInventoryList.ResourcesList[1].Amount;
            
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition1),resource1InList);
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition2),resource2InList);
            _resourceInventoryList.Add(_resourceDefinition2,4);
            resource2InList = _resourceInventoryList.ResourcesList[1].Amount;
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition1),resource1InList);
            Assert.AreEqual(_resourceInventoryList.GetResourceAmount(_resourceDefinition2),resource2InList);
        }
        
        [Test]
        public void GetResourceAmount_return_0__when_call_with_a_resource_that_not_in_inventory()
        {
            _resourceInventoryList.Add(_resourceDefinition1,1);
            Assert.AreEqual(0,_resourceInventoryList.GetResourceAmount(_resourceDefinition2));
        }
        
        [Test]
        public void resource_with_the_same_definition_are_added_with_the_correct_amount_()
        {
            _resourceInventoryList.Add(_resourceDefinition1,1);
            Assert.AreEqual(1,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
            _resourceInventoryList.Add(_resourceDefinition1,2);
            Assert.AreEqual(3,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
            _resourceInventoryList.Add(_resourceDefinition1,4);
            Assert.AreEqual(7,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
        }
        
        [Test]
        public void resource_with_different_definition_are_added_with_the_correct_amount_()
        {
            _resourceInventoryList.Add(_resourceDefinition1,1);
            Assert.AreEqual(1,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
            Assert.AreEqual(0,_resourceInventoryList.GetResourceAmount(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventoryList.GetResourceAmount(_resourceDefinition3));
            _resourceInventoryList.Add(_resourceDefinition2,2);
            Assert.AreEqual(1,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
            Assert.AreEqual(2,_resourceInventoryList.GetResourceAmount(_resourceDefinition2));
            Assert.AreEqual(0,_resourceInventoryList.GetResourceAmount(_resourceDefinition3));
            _resourceInventoryList.Add(_resourceDefinition3,4);
            Assert.AreEqual(1,_resourceInventoryList.GetResourceAmount(_resourceDefinition1));
            Assert.AreEqual(2,_resourceInventoryList.GetResourceAmount(_resourceDefinition2));
            Assert.AreEqual(4,_resourceInventoryList.GetResourceAmount(_resourceDefinition3));
        }
        private ResourceInventoryList GetInventory()
        {
            return new ResourceInventoryList();
        }
    }

    public class ui_resource_inventory_canvas
    {
        private UiResourceCanvas _uiResourceCanvas;
        private IResourceInventory _resourceInventory;
        
        private ResourceDefinition _resourceDefinition1;
        private ResourceDefinition _resourceDefinition2;
        private ResourceDefinition _resourceDefinition3;

        [SetUp]
        public void Init()
        {
            _uiResourceCanvas=GetUiResourceCanvas();
            _resourceInventory= Substitute.For<IResourceInventory>();
            _uiResourceCanvas.Bind(_resourceInventory);
            
            _resourceDefinition1= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource1.asset");
            _resourceDefinition2= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource2.asset");
            _resourceDefinition3= AssetDatabase.LoadAssetAtPath<ResourceDefinition>("Assets/Scripts/ScriptableObject/unit_test_resource3.asset");
        }
        
        [Test]
        public void slots_get_clear_after_binding()
        {
            foreach (var slot in _uiResourceCanvas.Slots)
            {
                Assert.AreEqual(null,slot.Sprite);
                Assert.AreEqual(string.Empty,slot.TextValue);
            }
        }

        [Test]
        public void there_are_40_slots()
        {
            Assert.AreEqual(40,_uiResourceCanvas.Slots.Length);
        }

        [Test]
        public void slot_get_refresh_when_OnAddResource_event_is_raise()
        {
            //Arrange
            List<ResourceDefinitionWithAmount> test = new List<ResourceDefinitionWithAmount>
            {
                new ResourceDefinitionWithAmount(_resourceDefinition1,1),
                new ResourceDefinitionWithAmount(_resourceDefinition2,2),
                new ResourceDefinitionWithAmount(_resourceDefinition3,3),
            };
            //Act
            _resourceInventory.OnResourceAdded += Raise.Event<Action<List<ResourceDefinitionWithAmount>>>(test);

            //Assert
            for (int i = 0; i < _uiResourceCanvas.Slots.Length; i++)
            {
                if (i == 0)
                {
                    Assert.AreEqual(_resourceDefinition1.Sprite,_uiResourceCanvas.Slots[i].Sprite);
                    Assert.AreEqual(1.ToString(),_uiResourceCanvas.Slots[i].TextValue);
                }else if (i == 1)
                {
                    Assert.AreEqual(_resourceDefinition2.Sprite,_uiResourceCanvas.Slots[i].Sprite);
                    Assert.AreEqual(2.ToString(),_uiResourceCanvas.Slots[i].TextValue);
                }else if (i == 2)
                {
                    Assert.AreEqual(_resourceDefinition3.Sprite,_uiResourceCanvas.Slots[i].Sprite);
                    Assert.AreEqual(3.ToString(),_uiResourceCanvas.Slots[i].TextValue); 
                }
                else
                {
                    Assert.AreEqual(null,_uiResourceCanvas.Slots[i].Sprite);
                    Assert.AreEqual(string.Empty,_uiResourceCanvas.Slots[i].TextValue); 
                }
            }
        }

        private UiResourceCanvas GetUiResourceCanvas()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UiResourceCanvas>("Assets/Prefabs/Ui/Ui Resource Canvas.prefab");
            return Object.Instantiate(prefab);
        }
    }

    public interface IDummySubscriber
    {
        void HandleOnResourceAdded(List<ResourceDefinitionWithAmount> resources);
    }
}