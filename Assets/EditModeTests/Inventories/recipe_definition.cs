using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace InventoryTest
{
    public class recipe_definition : MonoBehaviour
    {
        [Test]
        public void AddToInventories_method_call_is_calling_RecipeInventory_Add_method()
        {
            var recipeDefinition = Helpers.GetRecipeDefinition1ResourceResult();
            var sub = Substitute.For<IHaveInventories>();

            recipeDefinition.AddToInventory(sub,1);

            sub.RecipeInventory.Received().Add(recipeDefinition,1);
        }

        [Test]
        public void get_correct_amount_from_GetCraftableAmount()
        {
            var recipeDefinition1 = Helpers.GetRecipeDefinition1ResourceResult();
            var objectNeededForRecipe1 = Helpers.GetResourceDefinition1();
            var objectNeededForRecipe2 = Helpers.GetRecipeDefinition1ResourceResult();

            var subIHaveInventories = Substitute.For<IHaveInventories>();
            subIHaveInventories.ResourceInventory.GetAmountOf(objectNeededForRecipe1).Returns(12);
            subIHaveInventories.RecipeInventory.GetAmountOf(objectNeededForRecipe2).Returns(2);

            Assert.AreEqual(2,recipeDefinition1.GetCraftableAmount(subIHaveInventories));
        }


        [Test]
        public void GetRecipeResult_return_ICanBeAddedToInventories_if_setup_correctly()
        {
            var recipeWithResourceResult = Helpers.GetRecipeDefinition1ResourceResult();
            var recipeWithRecipeResult = Helpers.GetRecipeDefinition2RecipeResult();

            Assert.IsInstanceOf<ICanBeAddedToInventories>(recipeWithResourceResult.GetRecipeResult());
            Assert.IsInstanceOf<ICanBeAddedToInventories>(recipeWithRecipeResult.GetRecipeResult());
        }

        [Test]
        public void Sprite_dont_return_null_when_sprite_is_setup()
        {
            var recipe1 = Helpers.GetRecipeDefinition1ResourceResult();
            Assert.IsNotNull(recipe1.Sprite);
        }

        [Test]
        public void GetRecipeResult_return_null_if_not_setup_correctly()
        {
            var recipeWithWrongResult = Helpers.GetRecipeDefinition3WrongResult();

            Assert.IsNull(recipeWithWrongResult.GetRecipeResult());
        }
    }
}