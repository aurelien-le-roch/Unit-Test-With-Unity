using UnityEngine;

public class PlayerHandleInteractable
{
    private readonly IPlayer _player;
    public ItsAlmostAStack<IInteraclable> CurrentInteractables { get; } = new ItsAlmostAStack<IInteraclable>();
    
    public PlayerHandleInteractable(IPlayer player)
    {
        _player = player;
    }

    public void Tick()
    {
        if (CurrentInteractables.Count <= 0)
            return;


        if (_player.PlayerInput.InteractDown)
        {
            CurrentInteractables.Peek().InteractDown(_player.gameObject);
        }
        else if (_player.PlayerInput.InteractHold)
        {
            CurrentInteractables.Peek().InteractHold(_player.gameObject);
        }
        else
        {
            CurrentInteractables.Peek().DontInteract();
        }
    }

    public void OnTriggerEnter2D(IInteraclable iInteraclable)
    {
        if (CurrentInteractables.Count > 0)
        {
            IInteraclable peek = CurrentInteractables.Peek();
            
            if (peek is IHandlePlayerInZone peekHandlePlayerInZone)
                peekHandlePlayerInZone.PlayerExitZone();
        }
        
        CurrentInteractables.Push(iInteraclable);
        
        if(iInteraclable is IHandlePlayerInZone interactableIHandlePlayerInZone)
            interactableIHandlePlayerInZone.PlayerEnterZone();
    }

    public void OnTriggerExit2D(IInteraclable iInteraclable)
    {
        var peek = CurrentInteractables.Peek();
        
        if (LeaveCurrentInteraclableZone(peek, iInteraclable))
        {
            if (peek is IHandlePlayerInZone peekHandlePlayerInZone)
                peekHandlePlayerInZone.PlayerExitZone();
            
            CurrentInteractables.Pop();
            
            if (CurrentInteractables.Count > 0)
            {
                var peek2 =CurrentInteractables.Peek();
                if (peek2 is IHandlePlayerInZone peek2HandlePlayerInZone)
                    peek2HandlePlayerInZone.PlayerEnterZone();
            }
        }

        if (CurrentInteractables.Contains(iInteraclable))
        {
            CurrentInteractables.Remove(iInteraclable);
            
        }
    }

    private bool LeaveCurrentInteraclableZone(IInteraclable peek, IInteraclable interactable)
    {
        return peek != null && interactable == peek;
    }
}