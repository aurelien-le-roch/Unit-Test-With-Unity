using UnityEngine;

public class InteractableCounterZoneLogger : InteractableCounterZone
{
    public override void InteractDown(GameObject interactor)
    {
        base.InteractDown(interactor);
        Debug.Log($"Interact with {gameObject.name}");
        Debug.Log($"Current Counter = {CurrentCounter}");
        Debug.Log($"Max Counter = {MaxCounter}");
    }

    protected override void MaxCounterHit()
    {
        base.MaxCounterHit();
        Debug.Log("Hit Max Counter !");
    }
}