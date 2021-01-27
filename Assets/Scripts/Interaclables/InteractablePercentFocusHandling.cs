using System;
using UnityEngine;

public class InteractablePercentFocusHandling :  IInteractableWithFocusHandling
{
    private float _interactSpeed=2f;

    public event Action OnPlayerFocusMe;
    public event Action OnPlayerStopFocusMe;
    public event Action OnInteractableHit100Percent;

    public float InteractPercent { get; set; }
    public bool IHavePlayerFocus { get; private set; }

    public bool AlreadyHit100Percent { get; private set; }

    public void PlayerStartToFocusMe()
    {
       IHavePlayerFocus = true;
        OnPlayerFocusMe?.Invoke();
    }

    public virtual void PlayerStopToFocusMe()
    {
        IHavePlayerFocus = false;
        ResetInteractPercent();
        OnPlayerStopFocusMe?.Invoke();
    }

    public virtual void DontInteract(float deltaTime)
    {
        if (InteractPercent > 0 && AlreadyHit100Percent == false)
        {
            InteractPercent -= deltaTime * _interactSpeed;
        }

        if (InteractPercent < 0)
        {
            InteractPercent = 0;
        }
    }

    public virtual void InteractHold(GameObject interactor,float deltaTime)
    {
        if (InteractPercent >= 1)
        {
            InteractHoldHit100Percent(interactor);
        }

        if (InteractPercent < 1)
        {
            InteractPercent += deltaTime * _interactSpeed;
        }
    }

    protected virtual void InteractHoldHit100Percent(GameObject interactor)
    {
        if (AlreadyHit100Percent)
            return;

        AlreadyHit100Percent = true;
        OnInteractableHit100Percent?.Invoke();
    }

    public void InteractDown(GameObject interactor)
    {
        if (AlreadyHit100Percent)
            InteractDownAfter100PercentHit(interactor);
    }

    protected virtual void InteractDownAfter100PercentHit(GameObject interactor)
    {
    }

    public void ResetInteractPercent()
    {
        InteractPercent = 0;
    }
}