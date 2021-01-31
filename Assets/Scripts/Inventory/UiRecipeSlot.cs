using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UiRecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForCraftableAmount;
    [SerializeField] private UiResourcesNeededForRecipeSlot[] _uiResourcesNeededSlots;

    private RecipeDefinition _recipeDefinition;
    private CraftController _craftController;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleRecipeClick);
    }

    public void Refresh(RecipeDefinition recipe)
    {
        _imageForSprite.sprite = recipe.Sprite;
        _recipeDefinition = recipe;
        for (int i = 0; i < _uiResourcesNeededSlots.Length; i++)
        {
            if (i<recipe.ResourcesNeeded.Count)
            {
                _uiResourcesNeededSlots[i].Refresh(recipe.ResourcesNeeded[i].ResourceDefinition,recipe.ResourcesNeeded[i].Amount);
            }
            else
            {
                _uiResourcesNeededSlots[i].Clear();
            }
        }
    }

    private void HandleRecipeClick()
    {
        _craftController.SetNewCurrentRecipeInFocus(_recipeDefinition);
    }
    private void RefreshCraftableAmount(int amount)
    {
        _textForCraftableAmount.text = $"x{amount}";
    }
    
    public void Bind(CraftController craftController)
    {
        _craftController = craftController;
        craftController.OnRecipeCraftableAmountChange += HandleRecipeCraftableAmountChange;
    }

    private void HandleRecipeCraftableAmountChange(Dictionary<RecipeDefinition, int> recipesCraftableAmount)
    {
        if(_recipeDefinition==null)
            return;
        
        RefreshCraftableAmount(recipesCraftableAmount[_recipeDefinition]);
    }
}