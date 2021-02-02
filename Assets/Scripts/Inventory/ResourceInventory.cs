using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : IResourceInventory
{
    private List<ResourceDefinitionWithAmount> _resourcesList = new List<ResourceDefinitionWithAmount>();
    public event Action<List<ResourceDefinitionWithAmount>> OnResourceChange;
    public List<ResourceDefinitionWithAmount> ResourcesList => _resourcesList;

    public void Add(ResourceDefinition resourceDefinition, int amount)
    {
        amount = Mathf.Abs(amount);
        bool resourceAdded = false;
        foreach (var resource in _resourcesList)
        {
            if (resource.Definition == resourceDefinition) 
            {
                resource.IncreaseAmount(amount);
                resourceAdded = true;
            }
        }

        if (resourceAdded == false)
        {
            _resourcesList.Add(new ResourceDefinitionWithAmount(resourceDefinition, amount));
        }

        OnResourceChange?.Invoke(_resourcesList);
    }
    
    public void RemoveAll()
    {
        for (int i = _resourcesList.Count; i-- > 0;)
        {
            Remove(_resourcesList[i].Definition,_resourcesList[i].Amount);
        }
    }

    public void Remove(ResourceDefinition resourceDefinition, int amount)
    {
        amount = Mathf.Abs(amount);
        for (int i = _resourcesList.Count;i-->0;)
        {
            if (_resourcesList[i].Definition != resourceDefinition)
                continue;
            
            _resourcesList[i].ReduceAmount(amount);
            
            if (_resourcesList[i].Amount > 0)
                continue;
            
            _resourcesList.Remove(_resourcesList[i]);
        }
        OnResourceChange?.Invoke(_resourcesList);
    }
    
    public int GetAmountOf(ResourceDefinition definition)
    {
        foreach (var resourceInInventory in _resourcesList)
        {
            if (resourceInInventory.Definition == definition)
                return resourceInInventory.Amount;
        }

        return 0;
    }
}

public interface IRecipeInventory
{
    void Add(RecipeDefinition newRecipe,int amount);
    event Action<List<RecipeDefinitionWithAmount>> OnRecipeChange;
    bool Contain(RecipeDefinition recipeDefinition);
    int GetAmountOf(RecipeDefinition recipeDefinition);
    void Remove(RecipeDefinition recipeDefinition, int amount);
}