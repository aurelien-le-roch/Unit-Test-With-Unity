using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "RecipeDefinition")]
public class RecipeDefinition : ScriptableObjectInInventories
{
    [SerializeField] private List<ICanBeAddedToInventoriesWithAmountStruct> _inInventoryObjectsNeeded;
    [SerializeField] private ScriptableObjectInInventories _recipeResult;
    [SerializeField] private CraftMiniGamesEnum _craftMiniGamesEnum;
    public List<ICanBeAddedToInventoriesWithAmountStruct> InInventoryObjectsNeeded => _inInventoryObjectsNeeded;
    public CraftMiniGamesEnum CraftMiniGamesEnum => _craftMiniGamesEnum;
    
    public override void AddToInventory(IHaveInventories iHaveInventories,int amount)
    {
        iHaveInventories.RecipeInventory.Add(this,amount);
    }

    public override void RemoveFromInventory(IHaveInventories iHaveInventories, int amount)
    {
        iHaveInventories.RecipeInventory.Remove(this, amount);
    }

    public override int GetAmountInInventory(IHaveInventories iHaveInventories)
    {
        return iHaveInventories.RecipeInventory.GetAmountOf(this);
    }
    
    public int GetCraftableAmount(IHaveInventories ihaveHaveInventories)
    {
        int smallerCraftableAmount = int.MaxValue;
        
        for (int i = 0; i < _inInventoryObjectsNeeded.Count; i++)
        {
            int amountInInventory =
                _inInventoryObjectsNeeded[i].ICanBeAddedToInventories.GetAmountInInventory(ihaveHaveInventories);
            
            int resourceNeeded = _inInventoryObjectsNeeded[i].Amount;
            
            int craftableAmount = (int) Mathf.Floor(amountInInventory / resourceNeeded);
            
            if (craftableAmount < smallerCraftableAmount)
                smallerCraftableAmount = craftableAmount;
        }

        if (smallerCraftableAmount == int.MaxValue)
            return 0;

        return smallerCraftableAmount;
    }

    public ICanBeAddedToInventories GetRecipeResult()
    {
        return _recipeResult;
    }

    public bool NeedThatObjetForCraft(ICanBeAddedToInventories canBeAddedToInventories)
    {
        foreach (var element in _inInventoryObjectsNeeded)
        {
            if (element.ICanBeAddedToInventories == canBeAddedToInventories)
                return true;
        }

        return false;
    }
}

public enum CraftMiniGamesEnum
{
    None,
    EmptyCraftMiniGame,
}
[Serializable]
public struct ICanBeAddedToInventoriesWithAmountStruct 
{
    [SerializeField] private ScriptableObjectInInventories ScriptableObjectInInventories;
    public ICanBeAddedToInventories ICanBeAddedToInventories => ScriptableObjectInInventories;
    public int Amount;
    
}

public abstract class ScriptableObjectInInventories : ScriptableObject, ICanBeAddedToInventories
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
    public abstract void AddToInventory(IHaveInventories iHaveInventories,int amount);
    public abstract void RemoveFromInventory(IHaveInventories iHaveInventories, int amount);
    
    public abstract int GetAmountInInventory(IHaveInventories iHaveInventories); 
}

