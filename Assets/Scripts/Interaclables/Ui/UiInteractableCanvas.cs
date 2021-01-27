using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UiInteractableCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _interactablePanelAnimator;
    private static readonly int PlayerIsOut = Animator.StringToHash("PlayerIsOut");

    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; private set; }
    private void Start()
    {
        HandlePlayerInteractableFocus = GetComponentInParent<IHaveIHandlePlayerInZone>().HandlePlayerInteractableFocus;
        HandlePlayerInteractableFocus.OnPlayerFocusMe += HandlePlayerFocusMe;
        HandlePlayerInteractableFocus.OnPlayerStopFocusMe += HandlePlayerStopFocusMe;
    }

    
    private void HandlePlayerFocusMe()
    {
        _panel.SetActive(true);
        _interactablePanelAnimator.SetBool(PlayerIsOut,false);
    }
    
    private void HandlePlayerStopFocusMe()
    {
        _panel.SetActive(false);
        _interactablePanelAnimator.SetBool(PlayerIsOut,true);
        
    }

    
    private void OnDisable()
    {
        HandlePlayerInteractableFocus.OnPlayerFocusMe -= HandlePlayerFocusMe;
        HandlePlayerInteractableFocus.OnPlayerStopFocusMe -= HandlePlayerStopFocusMe;
    }
}