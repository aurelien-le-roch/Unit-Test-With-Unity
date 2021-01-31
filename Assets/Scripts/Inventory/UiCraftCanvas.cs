using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCraftCanvas : MonoBehaviour
{
    [SerializeField] private UiResourcesNeededForRecipeSlot[] _slots;
    [SerializeField] private Image _imageForCraftFocusSprite;
    [SerializeField] private Button _buttonToStartCraft;

    public void Bind(CraftController craftController)
    {
        _buttonToStartCraft.onClick.AddListener(craftController.Craft);
        _imageForCraftFocusSprite.sprite = null;
        craftController.OnNewCurrentRecipeFocus += Refresh;
        craftController.OnRecipeFocusReset += Clear;
    }

    private void Clear()
    {
        _imageForCraftFocusSprite.sprite=null;
        foreach (var slot in _slots)
        {
            slot.Clear();
        }
    }
    
    private void Refresh(RecipeDefinition recipeDefinition)
    {
        _imageForCraftFocusSprite.sprite = recipeDefinition.Sprite;

        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < recipeDefinition.ResourcesNeeded.Count)
            {
                var definition = recipeDefinition.ResourcesNeeded[i].ResourceDefinition;
                var amount = recipeDefinition.ResourcesNeeded[i].Amount;
                _slots[i].Refresh(definition,amount);
            }
            else
            {
                _slots[i].Clear();
            }
        }
    }
}
