using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : IResourceInventory
{
    private List<ResourceDefinitionWithAmount> _resourcesList = new List<ResourceDefinitionWithAmount>();
    public event Action<List<ResourceDefinitionWithAmount>> OnResourcesChange;
    public event Action<ResourceDefinition, int> OnResourceAmountChange;
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
                OnResourceAmountChange?.Invoke(resource.Definition,resource.Amount);
                resourceAdded = true;
            }
        }

        if (resourceAdded == false)
        {
            _resourcesList.Add(new ResourceDefinitionWithAmount(resourceDefinition, amount));
            OnResourceAmountChange?.Invoke(resourceDefinition,amount);

        }

        OnResourcesChange?.Invoke(_resourcesList);
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
            OnResourceAmountChange?.Invoke(_resourcesList[i].Definition,_resourcesList[i].Amount);

            if (_resourcesList[i].Amount > 0)
                continue;
            
            OnResourceAmountChange?.Invoke(_resourcesList[i].Definition,0);
            _resourcesList.Remove(_resourcesList[i]);
        }
        OnResourcesChange?.Invoke(_resourcesList);
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