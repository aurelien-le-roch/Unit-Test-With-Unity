using System;
using System.Collections.Generic;

public interface IResourceInventory
{
    List<ResourceDefinitionWithAmount> ResourcesList { get; }
    event Action<List<ResourceDefinitionWithAmount> > OnResourceChange;
    void Add(ResourceDefinition resourceDefinition, int amount);
    int GetResourceAmount(ResourceDefinition definition);
    bool SendResourceToOtherInventory(IResourceInventory otherInventory, List<ResourceDefinitionWithAmountStruct> resourcesToSend);
    void AddResources(List<ResourceDefinitionWithAmountStruct> resources);
    void RemoveAll();
}