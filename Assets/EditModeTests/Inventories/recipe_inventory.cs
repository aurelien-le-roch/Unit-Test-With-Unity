using NSubstitute;
using NUnit.Framework;

namespace InventoryTest
{
    public class recipe_inventory
    {
        [Test]
        public void when_Add_method_is_call_new_recipeDefinition_is_added_to_Recipes()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            recipeInventory.Add(recipeDefinition,1);
            Assert.AreEqual(recipeDefinition,recipeInventory.Recipes[0].Definition);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_Recipes_count_stay_the_same()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            recipeInventory.Add(recipeDefinition,1);
            recipeInventory.Add(recipeDefinition,1);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_OnRecipeAdded_get_raise()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForInventories>();
            recipeInventory.OnRecipeChange += sub.HandleOnRecipeChange;
            
            recipeInventory.Add(recipeDefinition,1);
            sub.ClearReceivedCalls();
            recipeInventory.Add(recipeDefinition,1);
            
            sub.Received().HandleOnRecipeChange(recipeInventory.Recipes);
        }
        
        [Test]
        public void when_Add_method_is_call_with_new_recipeDefinition_OnRecipeAdded_get_raise()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForInventories>();
            recipeInventory.OnRecipeChange += sub.HandleOnRecipeChange;
            
            recipeInventory.Add(recipeDefinition,1);
            
            sub.Received().HandleOnRecipeChange(recipeInventory.Recipes);
        }
    }
}