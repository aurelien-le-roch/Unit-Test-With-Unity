using System;
using UnityEngine;

public class InteractableCounterZone :  IHandlePlayerInteractableFocus, IInteraclable
{
    private int _maxCounter;
    public event Action OnPlayerFocusMe;
    public event Action OnPlayerStopFocusMe;
    public event Action<int, int> OnCounterChange;
    public event Action OnMaxCounterHit;
    public int MaxCounter => _maxCounter;
    public int CurrentCounter { get; set; }
    public bool IHavePlayerFocus { get; private set; }


    public InteractableCounterZone(int maxCounter)
    {
        _maxCounter = maxCounter;
    }
    
    public void PlayerStartToFocusMe()
    {
        IHavePlayerFocus = true;
        OnPlayerFocusMe?.Invoke();
    }

    public void PlayerStopToFocusMe()
    {
        IHavePlayerFocus = false;
        CurrentCounter = 0;
        OnCounterChange?.Invoke(_maxCounter, CurrentCounter);

        OnPlayerStopFocusMe?.Invoke();
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