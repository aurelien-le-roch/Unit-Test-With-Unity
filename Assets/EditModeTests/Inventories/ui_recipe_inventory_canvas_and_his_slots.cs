using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using Object = UnityEngine.Object;

namespace InventoryTest
{
    public class ui_recipe_inventory_canvas_and_his_slots
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

        
        [Test]
        public void slots_get_enable_when_OnRecipeAdded_is_raise()
        {
            _subRecipeInventory.OnNewRecipeAdded += Raise.Event<Action<RecipeDefinition,int,int>>(_recipe1,1,1);
            _subRecipeInventory.OnNewRecipeAdded += Raise.Event<Action<RecipeDefinition,int,int>>(_recipe2,1,1);
            
            Assert.IsTrue(_uiRecipesCanvas.Slots[0].gameObject.activeSelf);
            Assert.IsTrue(_uiRecipesCanvas.Slots[1].gameObject.activeSelf);
            Assert.IsFalse(_uiRecipesCanvas.Slots[2].gameObject.activeSelf);
            Assert.IsFalse(_uiRecipesCanvas.Slots[3].gameObject.activeSelf);
            Assert.IsFalse(_uiRecipesCanvas.Slots[4].gameObject.activeSelf);
        }
        
        [Test]
        public void slots_get_setup_when_OnRecipeAdded_is_raise()
        {
            _subRecipeInventory.OnNewRecipeAdded += Raise.Event<Action<RecipeDefinition,int,int>>(_recipe1,3,4);

            var slot = _uiRecipesCanvas.Slots[0];
            Assert.AreEqual(_recipe1,slot.RecipeDefinition);
            Assert.AreEqual("3",slot.AmountText);
            Assert.AreEqual("x4",slot.CraftableAmountText);
            Assert.AreEqual(_recipe1.Sprite,slot.Sprite);
        }
        
        [Test]
        public void slots_get_refresh_when_OnRecipeAmountChange_is_raise_from_IRecipeInventory()
        {
            _subRecipeInventory.OnNewRecipeAdded += Raise.Event<Action<RecipeDefinition,int,int>>(_recipe1,1,1);
            _subRecipeInventory.OnRecipeAmountChange += Raise.Event<Action<RecipeDefinition,int,int>>(_recipe1,2,3);

            var recipeDefinition = _uiRecipesCanvas.Slots[0].RecipeDefinition;
            string amount = _uiRecipesCanvas.Slots[0].AmountText;
            string craftableAmount = _uiRecipesCanvas.Slots[0].CraftableAmountText;
            var sprite = _uiRecipesCanvas.Slots[0].Sprite;

            Assert.AreEqual(_recipe1,recipeDefinition);
            Assert.AreEqual("2",amount);
            Assert.AreEqual("x3",craftableAmount);
            Assert.AreEqual(_recipe1.Sprite,sprite);
        }

        
        private UiRecipesCanvas GetUiRecipesCanvas()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UiRecipesCanvas>("Assets/Prefabs/Ui/Ui Craft Canvas.prefab");
            return Object.Instantiate(prefab);
        }
    }
}