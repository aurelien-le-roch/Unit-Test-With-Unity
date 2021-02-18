using System.Collections.Generic;
using UnityEngine;

public class UiRecipesCanvas : MonoBehaviour
{
    [SerializeField] private UiRecipeSlot[] _slots;
    private IRecipeInventory _recipeInventory;
    public UiRecipeSlot[] Slots => _slots;
    
    public void Bind(IRecipeInventory recipeInventory)
    {
        _recipeInventory = recipeInventory;
        _recipeInventory.OnNewRecipeAdded += HandleNewRecipeAdded;
        _recipeInventory.OnRecipeAmountChange += HandleRecipeAmountChange;
        _recipeInventory.OnRecipeRemoved += HandleRecipeRemoved;
        
        DisableAllSlot();
        
        //BindSlots(craftController);
    }

    private void HandleRecipeRemoved(RecipeDefinition recipeDefinition)
    {
        foreach (var slot in _slots)
        {
            if(slot.RecipeDefinition!=recipeDefinition)
                continue;
            slot.Clear();
        }
    }

    private void HandleRecipeAmountChange(RecipeDefinition recipeDefinition, int amount, int craftableAmount)
    {
        foreach (var slot in _slots)
        {
            if(slot.RecipeDefinition!=recipeDefinition)
                continue;
            slot.Refresh(amount, craftableAmount);
        }
    }

    private void HandleNewRecipeAdded(RecipeDefinition recipeDefinition, int amount, int craftableAmount)
    {
        //test ca
        foreach (var slot in _slots)
        {
            if (!slot.IsEmpty) 
                continue;
            slot.BindToNewRecipeDefinition(recipeDefinition,amount,craftableAmount);
            return;
        }
    }

//    private void BindSlots(ICraftController craftController)
//    {
//        foreach (var recipeSlot in Slots)
//        {
//            recipeSlot.Bind(craftController);
//        }
//    }
    
    private void DisableAllSlot()
    {
        foreach (var slot in Slots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}