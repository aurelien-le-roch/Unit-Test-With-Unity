using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class InteractableCanvas : MonoBehaviour
{
    [SerializeField] private InteraclablePanel _interaclablePanel;
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
        _interaclablePanel.gameObject.SetActive(false);
    }


    private void HandlePlayerEnterZone()
    {
        _interaclablePanel.gameObject.SetActive(true);
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