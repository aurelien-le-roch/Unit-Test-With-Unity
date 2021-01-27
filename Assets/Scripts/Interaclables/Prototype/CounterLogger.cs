using UnityEngine;

public class CounterLogger : MonoBehaviour,IHaveIHandlePlayerInteractableFocus,IHaveIInteraclable,IHaveInteractableCounterZone
{
    [SerializeField] private int _maxCounter;
    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; private set; }
    public IInteraclable Interaclable { get;  private set;}
    public InteractableCounterZone InteractableCounterZone { get; private set; }

    private void Awake()
    {
        var interactableCounterZoneLogger = new InteractableCounterZoneLogger(gameObject,_maxCounter);
        HandlePlayerInteractableFocus = interactableCounterZoneLogger;
        Interaclable = interactableCounterZoneLogger;
        InteractableCounterZone = interactableCounterZoneLogger;
    }
}