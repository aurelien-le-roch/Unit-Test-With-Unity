using UnityEngine;

[CreateAssetMenu(menuName = "ResourceDefinition")]
public class ResourceDefinition : ScriptableObject,ICanBeAddedToInventories
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    public void AddToInventories(IHaveInventories iHaveInventories)
    {
        iHaveInventories.ResourceInventory.Add(this,2);
    }
}

public interface ICanBeAddedToInventories
{
    void AddToInventories(IHaveInventories iHaveInventories);
}