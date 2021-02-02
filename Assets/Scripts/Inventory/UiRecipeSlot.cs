﻿using System;
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

    public RecipeDefinition RecipeDefinition { get; private set; }
    public Sprite Sprite => _imageForSprite.sprite;
    public String TextForAmount => _textForCraftableAmount.text;
    private ICraftController _craftController;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleRecipeClick);
    }

    public void Refresh(RecipeDefinition recipe)
    {
        _imageForSprite.sprite = recipe.Sprite;
        RecipeDefinition = recipe;
        for (int i = 0; i < _uiResourcesNeededSlots.Length; i++)
        {
            if (i<recipe.InInventoryObjectsNeeded.Count)
            {
                _uiResourcesNeededSlots[i].Refresh(recipe.InInventoryObjectsNeeded[i].ICanBeAddedToInventories,recipe.InInventoryObjectsNeeded[i].Amount);
            }
            else
            {
                _uiResourcesNeededSlots[i].Clear();
            }
        }
    }

    private void HandleRecipeClick()
    {
        _craftController.SetNewCurrentRecipeInFocus(RecipeDefinition);
    }
    private void RefreshCraftableAmount(int amount)
    {
        _textForCraftableAmount.text = $"x{amount}";
    }
    
    public void Bind(ICraftController craftController)
    {
        _craftController = craftController;
        craftController.OnRecipeCraftableAmountChange += HandleRecipeCraftableAmountChange;
    }

    public void Clear()
    {
        RecipeDefinition = null;
        _imageForSprite.sprite = null;
        _textForCraftableAmount.text = string.Empty;
        foreach (var slot in _uiResourcesNeededSlots)
        {
            slot.Clear();
        }
    }
    private void HandleRecipeCraftableAmountChange(Dictionary<RecipeDefinition, int> recipesCraftableAmount)
    {
        if(RecipeDefinition==null)
            return;
        
        RefreshCraftableAmount(recipesCraftableAmount[RecipeDefinition]);
    }
}