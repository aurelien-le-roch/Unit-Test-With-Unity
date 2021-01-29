using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceInventoryList : IResourceInventory
{
    private List<ResourceDefinitionWithAmount> _resourcesListList=new List<ResourceDefinitionWithAmount>();
    public event Action<List<ResourceDefinitionWithAmount> > OnResourceAdded;
    public List<ResourceDefinitionWithAmount> ResourcesList => _resourcesListList;

    public void Add(ResourceDefinition resourceDefinition, int amount)
    {
        bool resourceAdded=false;
        foreach (var resource in _resourcesListList)
        {
            if (resource.Definition == resourceDefinition)
            {
                resource.IncreaseAmount(amount);
                resourceAdded = true;
            }
        }

        if (resourceAdded == false)
        {
            _resourcesListList.Add(new ResourceDefinitionWithAmount(resourceDefinition,amount));
        }
        OnResourceAdded?.Invoke(_resourcesListList);
    }
    
    public int GetResourceAmount(ResourceDefinition definition)
    {
        foreach (var resourceInInventory in _resourcesListList)
        {
            if (resourceInInventory.Definition == definition)
                return resourceInInventory.Amount;
        }

        return 0;
    }
}

public class RecipeInventory : IRecipeInventory
{
    private List<RecipeDefinition> _recipes = new List<RecipeDefinition>();
    public event Action<List<RecipeDefinition> > OnRecipeAdded;
    
    public void Add(RecipeDefinition newRecipe)
    {
        if (_recipes.Contains(newRecipe)==false)
        {
            _recipes.Add(newRecipe);
            Debug.Log("recipe added");
        }
        OnRecipeAdded?.Invoke(_recipes);
    }
}

public interface IRecipeInventory
{
    void Add(RecipeDefinition newRecipe);
    event Action<List<RecipeDefinition> > OnRecipeAdded;
}

public class CraftController
{
    private readonly Player _player;
    private IResourceInventory ResourceInventory => _player.ResourceInventory;
    private IRecipeInventory RecipeInventory => _player.RecipeInventory;

    private Dictionary<RecipeDefinition, int> _recipeCraftableAmount=new Dictionary<RecipeDefinition, int>();

    public event Action<Dictionary<RecipeDefinition, int>> OnRecipeCraftableAmountChange;
    public CraftController(Player player)
    {
        _player = player;
        ResourceInventory.OnResourceAdded += RefreshRecipeCraftableAmount;
        RecipeInventory.OnRecipeAdded += RefreshRecipeCraftableAmount;
    }

    private void RefreshRecipeCraftableAmount(List<RecipeDefinition> recipes)
    {
        AddRecipeToDictionaryKey(recipes);
        RefreshRecipeCraftableAmount();
    }

    private void AddRecipeToDictionaryKey(List<RecipeDefinition> recipesOwn)
    {
        foreach (var recipeDefinitionOwn in recipesOwn)
        {
            if (_recipeCraftableAmount.ContainsKey(recipeDefinitionOwn) == false)
            {
                _recipeCraftableAmount.Add(recipeDefinitionOwn,0);
            }
        }
    }

    private void RefreshRecipeCraftableAmount(List<ResourceDefinitionWithAmount> resourcesOwn)
    {
        RefreshRecipeCraftableAmount();
    }

    private void RefreshRecipeCraftableAmount()
    {
        var amountChange = false;
        foreach (var key in _recipeCraftableAmount.Keys.ToList())
        {
            var newAmount = key.GetCraftableAmount(ResourceInventory);
            if (newAmount != _recipeCraftableAmount[key])
            {
                _recipeCraftableAmount[key] = newAmount;
                amountChange = true;
            }
        }

        if (amountChange)
        {
            OnRecipeCraftableAmountChange?.Invoke(_recipeCraftableAmount);
        }
    }
}