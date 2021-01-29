using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventoryDictionary : IResourceInventory
{
    private Dictionary<ResourceDefinition,int> _resourcesDictionary = new Dictionary<ResourceDefinition, int>();
    private List<ResourceDefinitionWithAmount> _resourcesList=new List<ResourceDefinitionWithAmount>();
    public List<ResourceDefinitionWithAmount> ResourcesList => _resourcesList;
    public event Action<List<ResourceDefinitionWithAmount> > OnResourceAdded;

    public void Add(ResourceDefinition resourceDefinition,int amount)
    {
        if (_resourcesDictionary.ContainsKey(resourceDefinition))
        {
            _resourcesDictionary[resourceDefinition]+=amount;

            foreach (var resource in _resourcesList)
            {
                if (resource.Definition == resourceDefinition)
                    resource.SetAmount(_resourcesDictionary);
            }
        }
        else
        {
            _resourcesDictionary.Add(resourceDefinition,amount);
            _resourcesList.Add(new ResourceDefinitionWithAmount(resourceDefinition,amount));
        }
        
        OnResourceAdded?.Invoke(_resourcesList);
    }

    public int GetResourceAmount(ResourceDefinition definition)
    {
        Debug.LogWarning("implement GetResourceAmount !");
        return 0;
    }
}