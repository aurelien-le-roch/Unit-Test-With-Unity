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
            recipeInventory.Add(recipeDefinition);
            Assert.AreEqual(recipeDefinition,recipeInventory.Recipes[0]);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_Recipes_count_stay_the_same()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            recipeInventory.Add(recipeDefinition);
            recipeInventory.Add(recipeDefinition);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_OnRecipeAdded_dont_get_raise()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForInventories>();
            recipeInventory.OnRecipeAdded += sub.HandleOnRecipeAdded;
            
            recipeInventory.Add(recipeDefinition);
            sub.ClearReceivedCalls();
            recipeInventory.Add(recipeDefinition);
            
            sub.DidNotReceive().HandleOnRecipeAdded(recipeInventory.Recipes);
        }
        
        [Test]
        public void when_Add_method_is_call_with_new_recipeDefinition_OnRecipeAdded_get_raise()
        {
            var recipeInventory = new RecipeInventory();
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForInventories>();
            recipeInventory.OnRecipeAdded += sub.HandleOnRecipeAdded;
            
            recipeInventory.Add(recipeDefinition);
            
            sub.Received().HandleOnRecipeAdded(recipeInventory.Recipes);
        }
    }
}