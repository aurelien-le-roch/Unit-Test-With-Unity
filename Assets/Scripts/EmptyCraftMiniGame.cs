using System;
using UnityEngine;
using UnityEngine.UI;

public class EmptyCraftMiniGame : MonoBehaviour
{
    [SerializeField] private Button _winButton;
    [SerializeField] private Button _loseButton;
    public event Action<CraftResultEnum> OnMiniGameResult;

    private void Awake()
    {
        _winButton.onClick.AddListener((() => OnMiniGameResult?.Invoke(CraftResultEnum.Win)));
        _loseButton.onClick.AddListener((() => OnMiniGameResult?.Invoke(CraftResultEnum.Lose)));
    }
}
