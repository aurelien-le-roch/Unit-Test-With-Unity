using System.Collections.Generic;
using UnityEngine;

public class UiRecipesCanvas : MonoBehaviour
{
    [SerializeField] private UiRecipeSlot[] _slots;
    private IRecipeInventory _recipeInventory;
    public UiRecipeSlot[] Slots => _slots;
    
    public void Bind(IRecipeInventory recipeInventory,ICraftController craftController)
    {
        _recipeInventory = recipeInventory;
        _recipeInventory.OnRecipeChange += HandleRecipeAdded;
        
        DisableAllSlot();
        
        BindSlots(craftController);
    }
    
    private void BindSlots(ICraftController craftController)
    {
        foreach (var recipeSlot in Slots)
        {
            recipeSlot.Bind(craftController);
        }
    }
    
    private void DisableAllSlot()
    {
        foreach (var slot in Slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    private void HandleRecipeAdded(List<RecipeDefinitionWithAmount> recipes)
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i < recipes.Count)
            {
                Slots[i].Refresh(recipes[i].Definition);
                Slots[i].gameObject.SetActive(true);
            }
            else
            {
                Slots[i].Clear();
                Slots[i].gameObject.SetActive(false);
            }
        }
    } 
}