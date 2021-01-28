using System;
using System.Collections.Generic;

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