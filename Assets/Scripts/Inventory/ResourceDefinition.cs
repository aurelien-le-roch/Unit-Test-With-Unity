using UnityEngine;

[CreateAssetMenu(menuName = "ResourceDefinition")]
public class ResourceDefinition : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
}