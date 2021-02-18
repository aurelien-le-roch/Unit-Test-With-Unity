using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCraftCanvasOld : MonoBehaviour
{
    [SerializeField] private UiResourcesNeededForRecipeSlot[] _slots;
    [SerializeField] private Image _imageForCraftFocusSprite;
    [SerializeField] private Button _buttonToStartCraft;

    [SerializeField] private GameObject _panel;

    public void Bind(CraftController craftController)
    {
        _buttonToStartCraft.onClick.AddListener(craftController.TryToStartCraft);
        _imageForCraftFocusSprite.sprite = null;
        craftController.OnNewCurrentRecipeFocus += Refresh;
        craftController.OnRecipeFocusReset += Clear;
        craftController.OnCraftMiniGameBegin += DisablePanel;
        craftController.OnCraftMiniGameEnd += EnablePanel;
    }

    private void EnablePanel()
    {
        _panel.SetActive(true);
    }

    private void DisablePanel()
    {
        _panel.SetActive(false);
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
