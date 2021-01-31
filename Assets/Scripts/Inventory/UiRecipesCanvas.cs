using System.Collections.Generic;
using UnityEngine;

public class UiRecipesCanvas : MonoBehaviour
{
    [SerializeField]private UiRecipeSlot[] _recipeSlots;
    private IRecipeInventory _recipeInventory;
    public UiRecipeSlot[] Slots => _recipeSlots;
    
    public void Bind(IRecipeInventory recipeInventory)
    {
        _recipeInventory =recipeInventory;
        _recipeInventory.OnRecipeAdded += HandleRecipeAdded;
        DisableAllSlot();
        
    }

    public void BindCraftControllerForSlot(CraftController craftController)
    {
        foreach (var recipeSlot in Slots)
        {
            recipeSlot.Bind(craftController);
        }
    }
    
    private void DisableAllSlot()
    {
        foreach (var slot in _recipeSlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    private void HandleRecipeAdded(List<RecipeDefinition> recipes)
    {
        for (int i = 0; i < _recipeSlots.Length; i++)
        {
            if (i < recipes.Count)
            {
                _recipeSlots[i].Refresh(recipes[i]);
                _recipeSlots[i].gameObject.SetActive(true);
            }
            else
            {
                _recipeSlots[i].gameObject.SetActive(false);
            }
        }
    } 
}