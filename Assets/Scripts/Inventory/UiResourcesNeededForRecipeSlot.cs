using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiResourcesNeededForRecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForAmount;
    public void Refresh(ResourceDefinitionWithAmountStruct resourceDefinitionWithAmountStruct)
    {
        _imageForSprite.sprite = resourceDefinitionWithAmountStruct.ResourceDefinition.Sprite;
        _textForAmount.text = resourceDefinitionWithAmountStruct.Amount.ToString();
    }

    public void Clear()
    {
        _imageForSprite.sprite = null;
        _textForAmount.text = null;
    }
}