using System;
using System.Collections.Generic;

public interface IRecipeInventory
{
    void Add(RecipeDefinition newRecipe,int amount);
    //event Action<List<RecipeDefinitionWithAmount>> OnRecipeChange;
    bool Contain(RecipeDefinition recipeDefinition);
    int GetAmountOf(RecipeDefinition recipeDefinition);
    void Remove(RecipeDefinition recipeDefinition, int amount);
    
    event Action<RecipeDefinition, int,int> OnNewRecipeAdded;
    event Action<RecipeDefinition, int,int> OnRecipeAmountChange;
    event Action<RecipeDefinition> OnRecipeRemoved;
    List<RecipeDefinitionWithAmount> Recipes { get; }
}

