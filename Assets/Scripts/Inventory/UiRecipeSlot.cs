using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiRecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForCraftableAmount;
    [SerializeField] private UiResourcesNeededForRecipeSlot[] _uiResourcesNeededSlots;

    private RecipeDefinition _recipeDefinition;
    public void Refresh(RecipeDefinition recipe)
    {
        _imageForSprite.sprite = recipe.Sprite;
        _recipeDefinition = recipe;
        for (int i = 0; i < _uiResourcesNeededSlots.Length; i++)
        {
            if (i<recipe.ResourcesNeeded.Count)
            {
                _uiResourcesNeededSlots[i].Refresh(recipe.ResourcesNeeded[i]);
            }
            else
            {
                _uiResourcesNeededSlots[i].Clear();
            }
        }
    }

    private void RefreshCraftableAmount(int amount)
    {
        _textForCraftableAmount.text = $"x{amount}";
    }
    public void Clear()
    {
        _imageForSprite.sprite = null;
        _recipeDefinition = null;
        foreach (var resourcesNeededForRecipeSlot in _uiResourcesNeededSlots)
        {
            resourcesNeededForRecipeSlot.Clear();
        }
    }

    public void Bind(CraftController craftController)
    {
        craftController.OnRecipeCraftableAmountChange += HandleRecipeCraftableAmountChange;
    }

    private void HandleRecipeCraftableAmountChange(Dictionary<RecipeDefinition, int> recipesCraftableAmount)
    {
        if(_recipeDefinition==null)
            return;
        
        RefreshCraftableAmount(recipesCraftableAmount[_recipeDefinition]);
    }
}