using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UiInteractableCanvas : MonoBehaviour
{
    [SerializeField] private UiInteraclablePanel _uiInteraclablePanel;
    [SerializeField] private Animator _interactablePanelAnimator;
    private static readonly int PlayerIsOut = Animator.StringToHash("PlayerIsOut");

    public InteractablePercentZone InteractablePercent { get; private set; }

    public UiInteraclablePanel UiInteraclablePanel => _uiInteraclablePanel;
    private void Awake()
    {
        InteractablePercent = GetComponentInParent<InteractablePercentZone>();
        InteractablePercent.OnPlayerEnterZone += HandlePlayerEnterZone;
        InteractablePercent.OnPlayerExitZone += HandlePlayerExitZone;
        InteractablePercent.OnInteractableHit100Percent += HandleHit100Percent;
    }

    private void HandleHit100Percent()
    {
        _uiInteraclablePanel.gameObject.SetActive(false);
    }


    private void HandlePlayerEnterZone()
    {
        _uiInteraclablePanel.gameObject.SetActive(true);
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
        InteractablePercent.OnInteractableHit100Percent -= HandleHit100Percent;
    }
}