using UnityEngine;

public class CounterLogger : MonoBehaviour,IHaveIHandlePlayerInZone,IHaveIInteraclable,IHaveInteractableCounterZone
{
    [SerializeField] private int _maxCounter;
    public IHandlePlayerInZone HandlePlayerInZone { get; private set; }
    public IInteraclable Interaclable { get;  private set;}
    public InteractableCounterZone InteractableCounterZone { get; private set; }

    private void Awake()
    {
        var interactableCounterZoneLogger = new InteractableCounterZoneLogger(gameObject,_maxCounter);
        HandlePlayerInZone = interactableCounterZoneLogger;
        Interaclable = interactableCounterZoneLogger;
        InteractableCounterZone = interactableCounterZoneLogger;
    }
}