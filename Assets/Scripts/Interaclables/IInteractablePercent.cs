using System;
using UnityEngine;

public interface IInteractablePercent
{
    float InteractPercent { get; }
    void InteractHold(GameObject interactor);
    void InteractDown(GameObject interactor);
    void DontInteract();
    void PlayerEnterZone();
    void PlayerExitZone();
    event Action OnPlayerEnterZone;
    event Action OnPlayerExitZone;
    event Action OnInteractableHit100Percent;
    bool PlayerInZone { get; }
}