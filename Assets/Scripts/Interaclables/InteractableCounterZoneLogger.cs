using System;
using UnityEngine;

public class InteractableCounterZoneLogger : InteractableCounterZone
{
    private GameObject _gameObject;
    public InteractableCounterZoneLogger(GameObject gameObject,int maxCounter) : base(maxCounter)
    {
        _gameObject = gameObject;
    }
    protected override void MaxCounterHit()
    {
        base.MaxCounterHit();
        Debug.Log("Hit Max Counter !");
        GameObject.Destroy(_gameObject);
    }
}