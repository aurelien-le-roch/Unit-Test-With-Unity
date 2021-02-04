using System;
using System.Collections.Generic;
using UnityEngine;

public class RecipeInventory : IRecipeInventory
{
    private List<RecipeDefinitionWithAmount> _recipes = new List<RecipeDefinitionWithAmount>();
    //public event Action<List<RecipeDefinitionWithAmount> > OnRecipeChange;
    
    public List<RecipeDefinitionWithAmount> Recipes => _recipes;

    public event Action<RecipeDefinition, int,int> OnNewRecipeAdded;
    public event Action<RecipeDefinition, int,int> OnRecipeAmountChange;
    public event Action<RecipeDefinition> OnRecipeRemoved;

    private IHaveInventories _haveInventories;

    public RecipeInventory(IHaveInventories haveInventories)
    {
        _haveInventories = haveInventories;
        _haveInventories.ResourceInventory.OnResourceAmountChange += RefreshRecipesCraftableAmount;
    }

    private void RefreshRecipesCraftableAmount(ResourceDefinition resourceDefinition,int amount)
    {
        foreach (var recipe in _recipes)
        {
            var recipeDefinition = recipe.Definition;
            
            if(recipeDefinition.NeedThatObjetForCraft(resourceDefinition)==false)
                continue;
            OnRecipeAmountChange?.Invoke(recipe.Definition,recipe.Amount,recipe.Definition.GetCraftableAmount(_haveInventories));
        }
    }
    public void Add(RecipeDefinition newRecipe,int amount)
    {
        amount = Mathf.Abs(amount);
        bool added = false;
        foreach (var recipe in _recipes)
        {
            if (recipe.Definition == newRecipe) 
            {
                recipe.IncreaseAmount(amount);
                OnRecipeAmountChange?.Invoke(recipe.Definition,recipe.Amount,recipe.Definition.GetCraftableAmount(_haveInventories));
                added = true;
            }
        }

        if (added == false)
        {
            var newAddedRecipe = new RecipeDefinitionWithAmount(newRecipe, amount); 
            _recipes.Add(newAddedRecipe);
            OnNewRecipeAdded?.Invoke(newAddedRecipe.Definition,newAddedRecipe.Amount,newAddedRecipe.Definition.GetCraftableAmount(_haveInventories));
        }

        //OnRecipeChange?.Invoke(_recipes);
    }
    
    public bool Contain(RecipeDefinition recipeDefinition)
    {
        foreach (var recipeDefinitionWithAmount in _recipes)
        {
            if (recipeDefinition == recipeDefinitionWithAmount.Definition)
                return true;
        }

        return false;
    }

    public int GetAmountOf(RecipeDefinition recipeDefinition)
    {
        foreach (var recipe in _recipes)
        {
            if (recipe.Definition == recipeDefinition)
                return recipe.Amount;
        }

        return 0;
    }

    public void Remove(RecipeDefinition recipeDefinition, int amount)
    {
        amount = Mathf.Abs(amount);
        for (int i = _recipes.Count;i-->0;)
        {
            if (_recipes[i].Definition != recipeDefinition)
                continue;
            
            _recipes[i].ReduceAmount(amount);
            
            if (_recipes[i].Amount > 0)
                continue;
            
            OnRecipeRemoved?.Invoke(_recipes[i].Definition);
            _recipes.Remove(_recipes[i]);
            
        }
        //OnRecipeChange?.Invoke(_recipes);
    }
}

public class RecipeDefinitionWithAmount 
{
    public RecipeDefinitionWithAmount(RecipeDefinition definition,int amount)
    {
        Definition = definition;
        Amount = amount;
    }
    
    public RecipeDefinition Definition { get; }
    public int Amount { get;  private set; }
    
    public void IncreaseAmount(int value)
    {
        Amount += value;
    }

    public void ReduceAmount(int value)
    {
        Amount -= value;
    }
}