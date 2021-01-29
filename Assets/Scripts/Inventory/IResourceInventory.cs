using System;
using System.Collections.Generic;

public interface IResourceInventory
{
    List<ResourceDefinitionWithAmount> ResourcesList { get; }
    event Action<List<ResourceDefinitionWithAmount> > OnResourceAdded;
    void Add(ResourceDefinition resourceDefinition, int amount);
    int GetResourceAmount(ResourceDefinition definition);
}