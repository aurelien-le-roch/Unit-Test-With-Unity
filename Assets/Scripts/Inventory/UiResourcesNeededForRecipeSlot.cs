using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiResourcesNeededForRecipeSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForAmount;
    public void Refresh(ICanBeAddedToInventories inInventoriesDefinition,int amount)
    {
        _imageForSprite.sprite = inInventoriesDefinition.Sprite;
        _textForAmount.text = amount.ToString();
    }

    public void Clear()
    {
        _imageForSprite.sprite = null;
        _textForAmount.text = string.Empty;
    }
}