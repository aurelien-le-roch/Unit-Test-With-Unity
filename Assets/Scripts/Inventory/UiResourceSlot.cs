using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiResourceSlot : MonoBehaviour
{
    [SerializeField] private Image _imageForSprite;
    [SerializeField] private TextMeshProUGUI _textForNumber;

    public Sprite Sprite => _imageForSprite.sprite;
    public String TextValue => _textForNumber.text;
    public void Refresh(ResourceDefinitionWithAmount definitionWithAmount)
    {
        _imageForSprite.sprite = definitionWithAmount.Definition.Sprite;
        _textForNumber.text = definitionWithAmount.Amount.ToString();
    }

    public void Clear()
    {
        _imageForSprite.sprite=null;
        _textForNumber.text = string.Empty;
    }
}