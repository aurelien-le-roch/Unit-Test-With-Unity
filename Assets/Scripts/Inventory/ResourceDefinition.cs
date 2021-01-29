using UnityEngine;

[CreateAssetMenu(menuName = "ResourceDefinition")]
public class ResourceDefinition : ScriptableObject,ICanBeAddedToInventories
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _amountToAddInTheResourceInventory=1;
    public Sprite Sprite => _sprite;

    public int AmountToAddInTheResourceInventory => _amountToAddInTheResourceInventory;

    public void AddToInventories(IHaveInventories iHaveInventories)
    {
        iHaveInventories.ResourceInventory.Add(this,_amountToAddInTheResourceInventory);
    }
}

public interface ICanBeAddedToInventories
{
    void AddToInventories(IHaveInventories iHaveInventories);
}