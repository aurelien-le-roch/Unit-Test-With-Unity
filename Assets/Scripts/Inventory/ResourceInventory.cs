using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : IResourceInventory
{
    private List<ResourceDefinitionWithAmount> _resourcesList = new List<ResourceDefinitionWithAmount>();
    public event Action<List<ResourceDefinitionWithAmount>> OnResourceChange;
    public List<ResourceDefinitionWithAmount> ResourcesList => _resourcesList;

    private List<ResourceDefinition> _checkListForInventoryTransfer=new List<ResourceDefinition>();
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
    
    public void AddResources(List<ResourceDefinitionWithAmountStruct> resources)
    {
        foreach (var resourceWithAmount in resources)
        {
            Add(resourceWithAmount.ResourceDefinition,resourceWithAmount.Amount);
        }
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
    
    private void RemoveResources(List<ResourceDefinitionWithAmountStruct> resourcesToRemove)
    {
        foreach (var resourceWithAmount in resourcesToRemove)
        {
            Remove(resourceWithAmount.ResourceDefinition, resourceWithAmount.Amount);
        }
    }
    
    public int GetResourceAmount(ResourceDefinition definition)
    {
        foreach (var resourceInInventory in _resourcesList)
        {
            if (resourceInInventory.Definition == definition)
                return resourceInInventory.Amount;
        }

        return 0;
    }

    public bool SendResourceToOtherInventory(IResourceInventory otherInventory, List<ResourceDefinitionWithAmountStruct> resourcesToSend)
    {
        if (AllTheResourcesWithTheirAmountArePresentAndListIsSafe(resourcesToSend)==false)
            return false;

        RemoveResources(resourcesToSend);
        otherInventory.AddResources(resourcesToSend);
        return true;
    }

    private bool AllTheResourcesWithTheirAmountArePresentAndListIsSafe(List<ResourceDefinitionWithAmountStruct> resourcesToSend)
    {
        if (OnlyOneTypeOfEachResourceDefinition(resourcesToSend) == false)
            return false;
        
        foreach (var resourceWithAmount in resourcesToSend)
        {
            if (GetResourceAmount(resourceWithAmount.ResourceDefinition) < resourceWithAmount.Amount)
                return false;
        }
        return true;
    }

    private bool OnlyOneTypeOfEachResourceDefinition(List<ResourceDefinitionWithAmountStruct> resourcesToSend)
    {
        _checkListForInventoryTransfer.Clear();

        foreach (var resourceWithAmount in resourcesToSend)
        {
            if (_checkListForInventoryTransfer.Contains(resourceWithAmount.ResourceDefinition))
                return false;
            _checkListForInventoryTransfer.Add(resourceWithAmount.ResourceDefinition);
        }

        return true;
    }
    
    
}

public interface IRecipeInventory
{
    void Add(RecipeDefinition newRecipe);
    event Action<List<RecipeDefinition>> OnRecipeAdded;
    bool Contain(RecipeDefinition recipeDefinition);
}