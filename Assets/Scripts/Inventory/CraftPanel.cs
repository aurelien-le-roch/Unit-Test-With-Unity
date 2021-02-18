using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    [SerializeField] private UiResourcesNeededForRecipeSlot[] _slots;
    [SerializeField] private Image _imageForCraftFocusSprite;
    [SerializeField] private Button _buttonToStartCraft;
    public event Action<RecipeDefinition> OnCraftButtonClick; 
    private RecipeDefinition _currentRecipeDefinition;
    private void Awake()
    {
        _buttonToStartCraft.onClick.AddListener(()=>OnCraftButtonClick?.Invoke(_currentRecipeDefinition));
    }
    
    public void Refresh(RecipeDefinition recipeDefinition)
    {
        _currentRecipeDefinition = recipeDefinition;
        _imageForCraftFocusSprite.sprite = recipeDefinition.Sprite;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < recipeDefinition.InInventoryObjectsNeeded.Count)
            {
                var definition = recipeDefinition.InInventoryObjectsNeeded[i].ICanBeAddedToInventories;
                var amount = recipeDefinition.InInventoryObjectsNeeded[i].Amount;
                _slots[i].Refresh(definition,amount);
            }
            else
            {
                _slots[i].Clear();
            }
        }
    }
}