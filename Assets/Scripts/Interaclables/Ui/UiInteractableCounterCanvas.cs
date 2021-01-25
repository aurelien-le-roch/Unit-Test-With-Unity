using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiInteractableCounterCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private List<Image> _counterImages;
    
    public InteractableCounterZone InteractableCounterZone { get; private set; }
    
    private void Awake()
    {
        InteractableCounterZone = GetComponentInParent<IHaveInteractableCounterZone>().InteractableCounterZone;
        InteractableCounterZone.OnPlayerEnterZone += HandlePlayerEnterZone;
        InteractableCounterZone.OnPlayerExitZone += DisablePanel;
        InteractableCounterZone.OnCounterChange += HandleCounterChange;
        InteractableCounterZone.OnMaxCounterHit += DisablePanel;
    }

    private void HandleCounterChange(int max, int current)
    {
        if (max != _counterImages.Count)
        {
            Debug.LogWarning("problem with max stack and number of stack Image", this);
            return;
        }

        for (int i = 0; i < _counterImages.Count; i++)
        {
            _counterImages[i].gameObject.SetActive(i < current);
        }
    }


    private void HandlePlayerEnterZone()
    {
        _panel.gameObject.SetActive(true);
    }
    
    private void DisablePanel()
    {
        _panel.SetActive(false);
    }

    private void OnDisable()
    {
        InteractableCounterZone.OnPlayerEnterZone -= HandlePlayerEnterZone;
        InteractableCounterZone.OnPlayerExitZone -= DisablePanel;
        InteractableCounterZone.OnMaxCounterHit -= DisablePanel;
    }
}