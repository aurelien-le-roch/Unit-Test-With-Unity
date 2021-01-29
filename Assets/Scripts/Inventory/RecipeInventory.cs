using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeInventory : IRecipeInventory
{
    private List<RecipeDefinition> _recipes = new List<RecipeDefinition>();
    public event Action<List<RecipeDefinition> > OnRecipeAdded;
    public List<RecipeDefinition> Recipes => _recipes;
    public void Add(RecipeDefinition newRecipe)
    {
        if (_recipes.Contains(newRecipe)) 
            return;
        
        _recipes.Add(newRecipe);
        OnRecipeAdded?.Invoke(_recipes);
    }
}