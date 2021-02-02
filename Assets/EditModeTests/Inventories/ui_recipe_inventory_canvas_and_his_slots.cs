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
        private List<RecipeDefinitionWithAmount> _recipeDefinitions;
        private RecipeDefinition _recipe1;
        private RecipeDefinition _recipe2;

        [SetUp]
        public void Init()
        {
            _uiRecipesCanvas=GetUiRecipesCanvas();
            _subRecipeInventory = Substitute.For<IRecipeInventory>();
            _subCraftController = Substitute.For<ICraftController>();
            _recipeDefinitions = new List<RecipeDefinitionWithAmount>
            {
                new RecipeDefinitionWithAmount(_recipe1, 1),
                new RecipeDefinitionWithAmount(_recipe2, 3)
            };

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
            _subRecipeInventory.OnRecipeChange += Raise.Event<Action<List<RecipeDefinitionWithAmount>>>(_recipeDefinitions);
            for (int i = 0; i < _uiRecipesCanvas.Slots.Length; i++)
            {
                if(i<_recipeDefinitions.Count)
                    Assert.IsTrue(_uiRecipesCanvas.Slots[i].gameObject.activeSelf);
                else
                    Assert.IsFalse(_uiRecipesCanvas.Slots[i].gameObject.activeSelf);
            }
        }
        
        [Test]
        public void slots_get_refresh_when_OnRecipeAdded_is_raise_from_IRecipeInventory()
        {
            _subRecipeInventory.OnRecipeChange += Raise.Event<Action<List<RecipeDefinitionWithAmount>>>(_recipeDefinitions);
            for (int i = 0; i < _uiRecipesCanvas.Slots.Length; i++)
            {
                var slot = _uiRecipesCanvas.Slots[i];
                if (i < _recipeDefinitions.Count)
                {
                    Assert.AreEqual(_recipeDefinitions[i].Definition,slot.RecipeDefinition);
                    Assert.AreEqual(_recipeDefinitions[i].Definition.Sprite,slot.Sprite);
                }
                else
                {
                    Assert.IsNull(slot.RecipeDefinition);
                    Assert.IsNull(slot.Sprite);
                    Assert.AreEqual(string.Empty,slot.TextForAmount);
                }
            }
        }

        [Test]
        public void
            slot_craftable_amount_get_refresh_when_OnRecipeCraftableAmountChange_is_raise_from_ICraftController()
        {
            _uiRecipesCanvas.Slots[0].Refresh(_recipe1);
            _uiRecipesCanvas.Slots[1].Refresh(_recipe2);
            var recipesWithCraftableAmount = new Dictionary<RecipeDefinition, int> {{_recipe1, 2}, {_recipe2, 1}};
            
            _subCraftController.OnRecipeCraftableAmountChange +=
                Raise.Event<Action<Dictionary<RecipeDefinition, int>>>(recipesWithCraftableAmount);
            
            for (int i = 0; i < _uiRecipesCanvas.Slots.Length; i++)
            {
                var slot = _uiRecipesCanvas.Slots[i];
                if (i == 0)
                {
                    Assert.AreEqual("x2",slot.TextForAmount);
                }else if (i == 1)
                {
                    Assert.AreEqual("x1",slot.TextForAmount);
                }
                else
                {
                    Assert.AreEqual("x0",slot.TextForAmount);
                }
            }
        }
        
        private UiRecipesCanvas GetUiRecipesCanvas()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UiRecipesCanvas>("Assets/Prefabs/Ui/Ui Craft Canvas.prefab");
            return Object.Instantiate(prefab);
        }
    }
}