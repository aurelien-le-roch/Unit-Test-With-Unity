using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UiInteractableCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _interactablePanelAnimator;
    private static readonly int PlayerIsOut = Animator.StringToHash("PlayerIsOut");

    public IHandlePlayerInZone IHandlePlayerInZone { get; private set; }
    private void Awake()
    {
        IHandlePlayerInZone = GetComponentInParent<IHandlePlayerInZone>();
        IHandlePlayerInZone.OnPlayerEnterZone += HandlePlayerEnterZone;
        IHandlePlayerInZone.OnPlayerExitZone += HandlePlayerExitZone;
    }

    
    private void HandlePlayerEnterZone()
    {
        _panel.SetActive(true);
        _interactablePanelAnimator.SetBool(PlayerIsOut,false);
    }
    
    private void HandlePlayerExitZone()
    {
        _panel.SetActive(false);
        _interactablePanelAnimator.SetBool(PlayerIsOut,true);
        
    }

    
    private void OnDisable()
    {
        IHandlePlayerInZone.OnPlayerEnterZone -= HandlePlayerEnterZone;
        IHandlePlayerInZone.OnPlayerExitZone -= HandlePlayerExitZone;
    }
}