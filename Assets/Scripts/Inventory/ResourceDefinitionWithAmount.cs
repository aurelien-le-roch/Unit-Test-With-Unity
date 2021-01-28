using System.Collections.Generic;

public class ResourceDefinitionWithAmount 
{
    public ResourceDefinitionWithAmount(ResourceDefinition definition,int amount)
    {
        Definition = definition;
        Amount = amount;
    }
    
    public ResourceDefinition Definition { get; }
    public int Amount { get;  private set; }
    public void SetAmount(Dictionary<ResourceDefinition,int> resources)
    {
        Amount = resources[Definition];
    }
    public void IncreaseAmount(int value)
    {
        Amount += value;
    }
}