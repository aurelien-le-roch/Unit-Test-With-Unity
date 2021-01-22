using UnityEngine;

public class PlayerHandleInteractable
{
    private readonly Player _player;
    private ItsAlmostAStack<InteractablePercentZone> _currentInteractables = new ItsAlmostAStack<InteractablePercentZone>();

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
        var interactable = other.GetComponentInParent<InteractablePercentZone>();
        if (interactable == null)
            return;


        if (_currentInteractables.Count > 0)
        {
            _currentInteractables.Peek().PlayerExitZone();
        }

        _currentInteractables.Push(interactable);
        interactable.PlayerEnterZone();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<InteractablePercentZone>();

        var peek = _currentInteractables.Peek();
        if (peek != null && interactable == peek)
        {
            peek.PlayerExitZone();
            _currentInteractables.Pop();
            if (_currentInteractables.Count > 0)
                _currentInteractables.Peek().PlayerEnterZone();
        }

        if (_currentInteractables.Contains(interactable))
        {
            _currentInteractables.Remove(interactable);
        }
    }
}