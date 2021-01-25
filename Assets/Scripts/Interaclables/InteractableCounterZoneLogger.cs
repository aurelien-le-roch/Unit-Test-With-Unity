using System;
using UnityEngine;

public class InteractableCounterZoneLogger : InteractableCounterZone
{
    private GameObject _gameObject;
    public InteractableCounterZoneLogger(GameObject gameObject,int maxCounter) : base(maxCounter)
    {
        _gameObject = gameObject;
    }
    public override void InteractDown(GameObject interactor)
    {
        base.InteractDown(interactor);
        Debug.Log($"Interact with {_gameObject.name}");
        Debug.Log($"Current Counter = {CurrentCounter}");
        Debug.Log($"Max Counter = {MaxCounter}");
    }

    protected override void MaxCounterHit()
    {
        base.MaxCounterHit();
        Debug.Log("Hit Max Counter !");
    }
}