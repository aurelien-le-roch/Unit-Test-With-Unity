using System;
using System.Collections.Generic;

public interface IResourceInventory
{
    List<ResourceDefinitionWithAmount> ResourcesList { get; }
    event Action<List<ResourceDefinitionWithAmount> > OnResourceChange;
    void Add(ResourceDefinition resourceDefinition, int amount);
    int GetAmountOf(ResourceDefinition definition);
    void Remove(ResourceDefinition definition, int amount);
}