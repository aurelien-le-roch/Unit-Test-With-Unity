﻿using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UiInteractablePercentCanvas : MonoBehaviour
{
    [SerializeField] private UiInteraclablePercenPanel uiInteraclablePercenPanel;
    [SerializeField] private Animator _interactablePanelAnimator;
    private static readonly int PlayerIsOut = Animator.StringToHash("PlayerIsOut");

    public InteractablePercentZone InteractablePercent { get; private set; }

    public UiInteraclablePercenPanel UiInteraclablePercenPanel => uiInteraclablePercenPanel;
    private void Start()
    {
        InteractablePercent = GetComponentInParent<IHaveInteractablePercentZone>().InteractablePercentZone;
        InteractablePercent.OnPlayerEnterZone += HandlePlayerEnterZone;
        InteractablePercent.OnPlayerExitZone += HandlePlayerExitZone;
        InteractablePercent.OnInteractableHit100Percent += HandleHit100Percent;
    }

    private void HandleHit100Percent()
    {
        uiInteraclablePercenPanel.gameObject.SetActive(false);
    }


    private void HandlePlayerEnterZone()
    {
        uiInteraclablePercenPanel.gameObject.SetActive(true);
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