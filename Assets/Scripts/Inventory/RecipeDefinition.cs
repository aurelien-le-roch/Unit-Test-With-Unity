using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RecipeDefinition")]
public class RecipeDefinition : ScriptableObject, ICanBeAddedToInventories
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private List<ResourceDefinitionWithAmountStruct> _resourcesNeeded;
    public Sprite Sprite => _sprite;
    public List<ResourceDefinitionWithAmountStruct> ResourcesNeeded => _resourcesNeeded;
    
    public void AddToInventories(IHaveInventories iHaveInventories)
    {
        iHaveInventories.RecipeInventory.Add(this);
    }

    public int GetCraftableAmount(IResourceInventory resourceInventory)
    {
        int smallerCraftableAmount = int.MaxValue;
        for (int i = 0; i < _resourcesNeeded.Count; i++)
        {
            int resourceAvailable = resourceInventory.GetResourceAmount(_resourcesNeeded[i].ResourceDefinition);
            int resourceNeeded = _resourcesNeeded[i].Amount;
            int craftableAmount = (int)Mathf.Floor(resourceAvailable / resourceNeeded);
            if (craftableAmount < smallerCraftableAmount)
                smallerCraftableAmount = craftableAmount;
        }

        if (smallerCraftableAmount == int.MaxValue)
            return 0;
        
        return smallerCraftableAmount;
    }
}

[Serializable]
public struct ResourceDefinitionWithAmountStruct
{
    public ResourceDefinition ResourceDefinition;
    public int Amount;
}