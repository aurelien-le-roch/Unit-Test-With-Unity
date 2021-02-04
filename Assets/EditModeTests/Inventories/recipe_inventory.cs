using NSubstitute;
using NUnit.Framework;

namespace InventoryTest
{
    public class recipe_inventory
    {
        private RecipeInventory recipeInventory;
        [SetUp]
        public void Init()
        {
            var subIHaveInventories = Substitute.For<IHaveInventories>();
            recipeInventory=new RecipeInventory(subIHaveInventories);
        }
        
        [Test]
        public void when_Add_method_is_call_new_recipeDefinition_is_added_to_Recipes()
        {
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            recipeInventory.Add(recipeDefinition,1);
            Assert.AreEqual(recipeDefinition,recipeInventory.Recipes[0].Definition);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_Recipes_count_stay_the_same()
        {
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            recipeInventory.Add(recipeDefinition,1);
            recipeInventory.Add(recipeDefinition,1);
            Assert.AreEqual(1,recipeInventory.Recipes.Count);
        }
        
        [Test]
        public void when_Add_method_is_call_with_already_added_recipeDefinition_OnRecipeAmountChange_get_raise()
        {
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForRecipeInventory>();
            recipeInventory.OnRecipeAmountChange += sub.HandleOnRecipeAmountChange;
            
            recipeInventory.Add(recipeDefinition,1);
            sub.ClearReceivedCalls();
            recipeInventory.Add(recipeDefinition,1);
            
            sub.Received().HandleOnRecipeAmountChange(recipeDefinition,2,0);
        }
        
        [Test]
        public void when_Add_method_is_call_with_new_recipeDefinition_OnRecipeAdded_get_raise()
        {
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IDummySubscriberForRecipeInventory>();
            recipeInventory.OnNewRecipeAdded += sub.HandleOnNewRecipeAdded;
            
            recipeInventory.Add(recipeDefinition,1);
            
            sub.Received().HandleOnNewRecipeAdded(recipeDefinition,1,0);
        }
        
        public interface IDummySubscriberForRecipeInventory
        {
            void HandleOnRecipeAmountChange(RecipeDefinition recipeDefinition,int amount,int craftableAmount);
            void HandleOnNewRecipeAdded(RecipeDefinition recipeDefinition,int amount,int craftableAmount);
        }
    }
}