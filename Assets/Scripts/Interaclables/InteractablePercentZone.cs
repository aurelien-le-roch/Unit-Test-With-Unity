using System;
using UnityEngine;

public abstract class InteractablePercentZone : MonoBehaviour,IInteractablePercent
{
    [SerializeField] private float _interactSpeed;
    
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public event Action OnInteractableHit100Percent;
    
    public float InteractPercent { get; set; }
    public bool PlayerInZone { get; private set; }
    
    protected bool AlreadyHit100Percent;
    
    public void PlayerEnterZone()
    {
        PlayerInZone = true;
        OnPlayerEnterZone?.Invoke();
    }

    public virtual void PlayerExitZone()
    {
        PlayerInZone = false;
        ResetInteractPercent();
        OnPlayerExitZone?.Invoke();
    }
    public virtual void DontInteract()
    {
        if (InteractPercent > 0 && AlreadyHit100Percent==false)
        {
            InteractPercent -= Time.deltaTime * _interactSpeed;
        }
    }
    
    public virtual void InteractHold(GameObject interactor)
    {
        if (InteractPercent >= 1)
        {
            InteractHoldHit100Percent(interactor);
        }

        if (InteractPercent < 1)
        {
            InteractPercent += Time.deltaTime * _interactSpeed;
        }
    }
    
    protected virtual void InteractHoldHit100Percent(GameObject interactor)
    {
        if(AlreadyHit100Percent)
            return;
        
        AlreadyHit100Percent = true;
        OnInteractableHit100Percent?.Invoke();
    }

    public void InteractDown(GameObject interactor)
    {
        if(AlreadyHit100Percent)
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