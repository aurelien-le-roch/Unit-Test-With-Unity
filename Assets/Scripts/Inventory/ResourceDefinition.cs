using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class ResourceDefinition : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
}