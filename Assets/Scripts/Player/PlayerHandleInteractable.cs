using UnityEngine;

public class PlayerHandleInteractable
{
    private readonly Player _player;
    private ItsAlmostAStack<IInteraclable> _currentInteractables = new ItsAlmostAStack<IInteraclable>();

    public PlayerHandleInteractable(Player player)
    {
        _player = player;
    }

    public void Tick()
    {
        if (_currentInteractables.Count <= 0)
            return;


        if (_player.PlayerInput.InteractDown)
        {
            _currentInteractables.Peek().InteractDown(_player.gameObject);
        }
        else if (_player.PlayerInput.InteractHold)
        {
            _currentInteractables.Peek().InteractHold(_player.gameObject);
        }
        else
        {
            _currentInteractables.Peek().DontInteract();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<IInteraclable>();
        if (interactable == null)
            return;


        if (_currentInteractables.Count > 0)
        {
            
            IInteraclable peek = _currentInteractables.Peek();
            
            if (peek is IHandlePlayerInZone peekHandlePlayerInZone)
                peekHandlePlayerInZone.PlayerExitZone();
        }

        _currentInteractables.Push(interactable);
        
        if(interactable is IHandlePlayerInZone interactableIHandlePlayerInZone)
            interactableIHandlePlayerInZone.PlayerEnterZone();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        
        var interactable = other.GetComponentInParent<IInteraclable>();

        var peek = _currentInteractables.Peek();
        
        if (LeaveCurrentInteraclableZone(peek, interactable))
        {
            if (peek is IHandlePlayerInZone peekHandlePlayerInZone)
                peekHandlePlayerInZone.PlayerExitZone();
            
            _currentInteractables.Pop();
            
            if (_currentInteractables.Count > 0)
            {
                var peek2 =_currentInteractables.Peek();
                if (peek2 is IHandlePlayerInZone peek2HandlePlayerInZone)
                    peek2HandlePlayerInZone.PlayerEnterZone();
            }
        }

        if (_currentInteractables.Contains(interactable))
        {
            _currentInteractables.Remove(interactable);
        }
    }

    private static bool LeaveCurrentInteraclableZone(IInteraclable peek, IInteraclable interactable)
    {
        return peek != null && interactable == peek;
    }
}