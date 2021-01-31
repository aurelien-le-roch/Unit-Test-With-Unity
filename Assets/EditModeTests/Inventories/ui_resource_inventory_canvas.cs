﻿using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InventoryTest
{
    public class ui_resource_inventory_canvas
    {
        private UiResourcesCanvas _uiResourcesCanvas;
        private IResourceInventory _resourceInventory;
        
        private ResourceDefinition _resourceDefinition1;
        private ResourceDefinition _resourceDefinition2;
        private ResourceDefinition _resourceDefinition3;

        [SetUp]
        public void Init()
        {
            _uiResourcesCanvas=GetUiResourceCanvas();
            _resourceInventory= Substitute.For<IResourceInventory>();
            _uiResourcesCanvas.Bind(_resourceInventory);
            
            _resourceDefinition1= Helpers.GetResourceDefinition1();
            _resourceDefinition2= Helpers.GetResourceDefinition2();
            _resourceDefinition3= Helpers.GetResourceDefinition3();
        }
        
        [Test]
        public void slots_get_clear_after_binding()
        {
            foreach (var slot in _uiResourcesCanvas.Slots)
            {
                Assert.AreEqual(null,slot.Sprite);
                Assert.AreEqual(string.Empty,slot.TextValue);
            }
        }

        [Test]
        public void there_are_40_slots()
        {
            Assert.AreEqual(40,_uiResourcesCanvas.Slots.Length);
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
            _resourceInventory.OnResourceChange += Raise.Event<Action<List<ResourceDefinitionWithAmount>>>(test);

            //Assert
            for (int i = 0; i < _uiResourcesCanvas.Slots.Length; i++)
            {
                if (i == 0)
                {
                    Assert.AreEqual(_resourceDefinition1.Sprite,_uiResourcesCanvas.Slots[i].Sprite);
                    Assert.AreEqual(1.ToString(),_uiResourcesCanvas.Slots[i].TextValue);
                }else if (i == 1)
                {
                    Assert.AreEqual(_resourceDefinition2.Sprite,_uiResourcesCanvas.Slots[i].Sprite);
                    Assert.AreEqual(2.ToString(),_uiResourcesCanvas.Slots[i].TextValue);
                }else if (i == 2)
                {
                    Assert.AreEqual(_resourceDefinition3.Sprite,_uiResourcesCanvas.Slots[i].Sprite);
                    Assert.AreEqual(3.ToString(),_uiResourcesCanvas.Slots[i].TextValue); 
                }
                else
                {
                    Assert.AreEqual(null,_uiResourcesCanvas.Slots[i].Sprite);
                    Assert.AreEqual(string.Empty,_uiResourcesCanvas.Slots[i].TextValue); 
                }
            }
        }

        private UiResourcesCanvas GetUiResourceCanvas()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UiResourcesCanvas>("Assets/Prefabs/Ui/Ui Resource Canvas.prefab");
            return Object.Instantiate(prefab);
        }
    }

    public class ui_recipe_inventory_canvas
    {
        private UiRecipesCanvas _uiRecipesCanvas;
        private IRecipeInventory _subRecipeInventory;
        private ICraftController _subCraftController;
        private RecipeDefinition _recipe1;
        private RecipeDefinition _recipe2;

        [SetUp]
        public void Init()
        {
            _uiRecipesCanvas=GetUiRecipesCanvas();
            _subRecipeInventory = Substitute.For<IRecipeInventory>();
            _subCraftController = Substitute.For<ICraftController>();

            _uiRecipesCanvas.Bind(_subRecipeInventory,_subCraftController);
            
            _recipe1= Helpers.GetRecipeDefinition1ResourceResult();
            _recipe2= Helpers.GetRecipeDefinition2RecipeResult();
        }

        [Test]
        public void slots_get_disable_after_binding()
        {
            foreach (var slot in _uiRecipesCanvas.Slots)
            {
                Assert.IsFalse(slot.gameObject.activeSelf);
            }
        }

        [Test]
        public void there_are_5_slots()
        {
            Assert.AreEqual(5,_uiRecipesCanvas.Slots.Length);
        }
        
        private UiRecipesCanvas GetUiRecipesCanvas()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UiRecipesCanvas>("Assets/Prefabs/Ui/Ui Craft Canvas.prefab");
            return Object.Instantiate(prefab);
        }
    }
}