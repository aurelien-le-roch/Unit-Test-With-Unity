using UnityEngine;

[CreateAssetMenu(menuName = "ResourceDefinition")]
public class ResourceDefinition : ScriptableObject,ICanBeAddedToPlayer
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    public void AddToPlayer(Player player)
    {
        player.ResourceInventory.Add(this,2);
    }
}

public interface ICanBeAddedToPlayer
{
    void AddToPlayer(Player player);
}