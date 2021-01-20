using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UiInteractableCanvas : MonoBehaviour
{
    [SerializeField] private UiInteraclablePanel uiInteraclablePanel;
    [SerializeField] private Animator _interactablePanelAnimator;
    private static readonly int PlayerIsOut = Animator.StringToHash("PlayerIsOut");

    public IInteractablePercent InteractablePercent { get; private set; }
    private void Awake()
    {
        InteractablePercent = GetComponentInParent<IInteractablePercent>();
        InteractablePercent.OnPlayerEnterZone += HandlePlayerEnterZone;
        InteractablePercent.OnPlayerExitZone += HandlePlayerExitZone;
        InteractablePercent.OnInteractableHit100Percent += HandleHit100Percent;
    }

    private void HandleHit100Percent()
    {
        uiInteraclablePanel.gameObject.SetActive(false);
    }


    private void HandlePlayerEnterZone()
    {
        uiInteraclablePanel.gameObject.SetActive(true);
        _interactablePanelAnimator.SetBool(PlayerIsOut,false);
    }
    
    private void HandlePlayerExitZone()
    {
        _interactablePanelAnimator.SetBool(PlayerIsOut,true);
    }

    
    private void OnDisable()
    {
        InteractablePercent.OnPlayerEnterZone -= HandlePlayerEnterZone;
        InteractablePercent.OnPlayerExitZone -= HandlePlayerExitZone;
    }
}