using System;
using UnityEngine;

public class InteractableCounterZone :  IHandlePlayerInZone, IInteraclable
{
    private int _maxCounter;
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public event Action<int, int> OnCounterChange;
    public event Action OnMaxCounterHit;
    public int MaxCounter => _maxCounter;
    public int CurrentCounter { get; set; }
    public bool PlayerInZone { get; private set; }


    public InteractableCounterZone(int maxCounter)
    {
        _maxCounter = maxCounter;
    }
    
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
    public void InteractHold(GameObject interactor,float deltaTime)
    {
    }

    public void DontInteract(float deltaTime)
    {
    }

    protected virtual void MaxCounterHit()
    {
        OnMaxCounterHit?.Invoke();
    }
}