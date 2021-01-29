using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace RecipeDefinitionTest
{
    public class recipe_definition : MonoBehaviour
    {
        [Test]
        public void AddToInventories_method_call_is_calling_RecipeInventory_Add_method()
        {
            var recipeDefinition = GetRecipeDefinition1();
            var sub = Substitute.For<IHaveInventories>();
            
            recipeDefinition.AddToInventories(sub);
            
            sub.RecipeInventory.Received().Add(recipeDefinition);
        }

        [Test]
        public void get_correct_amount_from_GetCraftableAmount_method_call_from_single_resource_recipe()
        {
            var recipeDefinition1 = GetRecipeDefinition1();
            var resourceNeeded1 = recipeDefinition1.ResourcesNeeded[0].ResourceDefinition;
            var resourceNeededAmount1 = recipeDefinition1.ResourcesNeeded[0].Amount;
            
            var subResourceInventory = Substitute.For<IResourceInventory>();
            subResourceInventory.GetResourceAmount(resourceNeeded1).Returns(resourceNeededAmount1*3);
            
            Assert.AreEqual(3,recipeDefinition1.GetCraftableAmount(subResourceInventory));
        }
        
        [Test]
        public void get_correct_amount_from_GetCraftableAmount_method_call_from_2_resources_recipe()
        {
            var recipeDefinition2 = GetRecipeDefinition2();
            var resourceNeeded1 = recipeDefinition2.ResourcesNeeded[0].ResourceDefinition;
            var resourceNeededAmount1 = recipeDefinition2.ResourcesNeeded[0].Amount;

            
            var resourceNeeded2 = recipeDefinition2.ResourcesNeeded[1].ResourceDefinition;
            var resourceNeededAmount2 = recipeDefinition2.ResourcesNeeded[1].Amount;
            
            var subResourceInventory = Substitute.For<IResourceInventory>();
            subResourceInventory.GetResourceAmount(resourceNeeded1).Returns(resourceNeededAmount1*3);
            subResourceInventory.GetResourceAmount(resourceNeeded2).Returns(resourceNeededAmount2);
            
            Assert.AreEqual(1,recipeDefinition2.GetCraftableAmount(subResourceInventory));
        }
        
        private RecipeDefinition GetRecipeDefinition1()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>
                ("Assets/Scripts/ScriptableObject/unit_test_recipe1.asset");
        }
        
        private RecipeDefinition GetRecipeDefinition2()
        {
            return AssetDatabase.LoadAssetAtPath<RecipeDefinition>
                ("Assets/Scripts/ScriptableObject/unit_test_recipe2.asset");
        }
    }
}

