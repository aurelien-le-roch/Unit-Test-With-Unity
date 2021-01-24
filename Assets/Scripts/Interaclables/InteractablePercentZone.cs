using System;
using UnityEngine;

public abstract class InteractablePercentZone :  IInteractableWithZone
{
    private float _interactSpeed=2f;

    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public event Action OnInteractableHit100Percent;

    public float InteractPercent { get; set; }
    public bool PlayerInZone { get; private set; }

    public bool AlreadyHit100Percent { get; private set; }

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
        if (InteractPercent > 0 && AlreadyHit100Percent == false)
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

public abstract class InteractableCounterZone : MonoBehaviour, IHandlePlayerInZone, IInteraclable
{
    [SerializeField] private int _maxCounter;
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public event Action<int, int> OnCounterChange;
    public event Action OnMaxCounterHit;
    public int MaxCounter => _maxCounter;
    public int CurrentCounter { get; private set; }
    public bool PlayerInZone { get; private set; }


    public void PlayerEnterZone()
    {
        PlayerInZone = true;
        OnPlayerEnterZone?.Invoke();
    }

    public void PlayerExitZone()
    {
        PlayerInZone = false;
        CurrentCounter = 0;
        OnCounterChange?.Invoke(_maxCounter, CurrentCounter);

        OnPlayerExitZone?.Invoke();
    }

    public virtual void InteractDown(GameObject interactor)
    {
        if(CurrentCounter>=_maxCounter)
            return;
        CurrentCounter++;
        OnCounterChange?.Invoke(_maxCounter, CurrentCounter);
        if (CurrentCounter >= _maxCounter)
            MaxCounterHit();
    }
    public void InteractHold(GameObject interactor)
    {
    }

    public void DontInteract()
    {
    }

    protected virtual void MaxCounterHit()
    {
        OnMaxCounterHit?.Invoke();
    }
}


public interface IHandlePlayerInZone
{
    event Action OnPlayerEnterZone;
    event Action OnPlayerExitZone;
    bool PlayerInZone { get; }
    void PlayerEnterZone();
    void PlayerExitZone();
}

public interface IInteraclable
{
    void InteractDown(GameObject interactor);
    void InteractHold(GameObject interactor);
    void DontInteract();
}

public interface IInteractableWithZone : IHandlePlayerInZone,IInteraclable
{
    
}