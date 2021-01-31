using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiResourcesNeededForRecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForAmount;
    public void Refresh(ResourceDefinition resourceDefinition,int amount)
    {
        _imageForSprite.sprite = resourceDefinition.Sprite;
        _textForAmount.text = amount.ToString();
    }

    public void Clear()
    {
        _imageForSprite.sprite = null;
        _textForAmount.text = null;
    }
}