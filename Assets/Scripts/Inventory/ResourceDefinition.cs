using UnityEngine;

[CreateAssetMenu(menuName = "ResourceDefinition")]
public class ResourceDefinition : ScriptableObjectInInventories
{
    public override void AddToInventory(IHaveInventories iHaveInventories,int amount)
    {
        iHaveInventories.ResourceInventory.Add(this,amount);
    }

    public override void RemoveFromInventory(IHaveInventories iHaveInventories, int amount)
    {
        iHaveInventories.ResourceInventory.Remove(this, amount);
    }

    public override int GetAmountInInventory(IHaveInventories iHaveInventories)
    {
        return iHaveInventories.ResourceInventory.GetAmountOf(this);
    }
    
}

public interface ICanBeAddedToInventories
{
    void AddToInventory(IHaveInventories iHaveInventories,int amount);
    
    void RemoveFromInventory(IHaveInventories iHaveInventories,int amount);
    int GetAmountInInventory(IHaveInventories iHaveInventories);
    Sprite Sprite { get; }
}