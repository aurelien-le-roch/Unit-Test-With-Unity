using UnityEngine;

[CreateAssetMenu(menuName = "OreNodeDefinition")]
public class OreNodeDefinition : ScriptableObject
{
    public int Level;
    public OreType OreType;
    public int MinOreGiven;
    public int MaxOreGiven;
    public LootableResource GemsDropAfterMine;
}