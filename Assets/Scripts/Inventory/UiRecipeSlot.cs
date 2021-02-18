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

    public RecipeDefinition RecipeDefinition { get; private set; }
    public Sprite Sprite => _imageForSprite.sprite;
    public String CraftableAmountText => _textForCraftableAmount.text;
    public bool IsEmpty => RecipeDefinition == null;
    public string AmountText { get; private set; }

    public event Action<RecipeDefinition> OnRecipeSlotGetClicked;
    //private ICraftController _craftController;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(()=>OnRecipeSlotGetClicked?.Invoke(RecipeDefinition));
    }

//    private void HandleRecipeClick()
//    {
//        _craftController.SetNewCurrentRecipeInFocus(RecipeDefinition);
//    }

//    public void Bind(ICraftController craftController)
//    {
//        _craftController = craftController;
//    }

    public void Clear()
    {
        gameObject.SetActive(false);
        RecipeDefinition = null;
        _imageForSprite.sprite = null;
        _textForCraftableAmount.text = string.Empty;
        foreach (var slot in _uiResourcesNeededSlots)
        {
            slot.Clear();
        }
    }


    public void BindToNewRecipeDefinition(RecipeDefinition recipeDefinition, int amount, int craftableAmount)
    {
        RecipeDefinition = recipeDefinition;
        RefreshCraftableAmountText(craftableAmount);
        RefreshResourceNeededSlots(recipeDefinition);
        _imageForSprite.sprite = recipeDefinition.Sprite;
        AmountText = amount.ToString();
        gameObject.SetActive(true);
    }

    public void Refresh(int amount, int craftableAmount)
    {
        RefreshCraftableAmountText(craftableAmount);
        AmountText = amount.ToString();
    }

    private void RefreshCraftableAmountText(int amount)
    {
        _textForCraftableAmount.text = $"x{amount}";
        Debug.Log("refresh craftable amount");
    }

    private void RefreshResourceNeededSlots(RecipeDefinition recipeDefinition)
    {
        for (int i = 0; i < _uiResourcesNeededSlots.Length; i++)
        {
            if (i<recipeDefinition.InInventoryObjectsNeeded.Count)
            {
                _uiResourcesNeededSlots[i].Refresh
                    (recipeDefinition.InInventoryObjectsNeeded[i].ICanBeAddedToInventories
                    ,recipeDefinition.InInventoryObjectsNeeded[i].Amount);
            }
            else
            {
                _uiResourcesNeededSlots[i].Clear();
            }
        }
    }
}

