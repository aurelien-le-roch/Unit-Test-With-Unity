using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHaveWorkController
{
    [SerializeField] private float _speed;

    public IWorkController WorkController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public float Speed => _speed;

    private PlayerMover _mover;
    
    private PlayerHandleInteractable _handleInteractable;
    private void Awake()
    {
        WorkController = new WorkControllerTest();
        PlayerInput = new PlayerInput();
        _mover = new PlayerMover(this);
        _handleInteractable = new PlayerHandleInteractable(this);
    }

    private void Update()
    {
        _handleInteractable.Tick();
    }

    private void FixedUpdate()
    {
        _mover.Tick();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _handleInteractable.OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _handleInteractable.OnTriggerExit2D(other);
    }
}

public class PlayerHandleInteractable
{
    private readonly Player _player;
    private ItsAlmostAStack<IInteractablePercent> _currentInteractables = new ItsAlmostAStack<IInteractablePercent>();
    public PlayerHandleInteractable(Player player)
    {
        _player = player;
    }
    public void Tick()
    {
        if(_currentInteractables.Count<=0)
            return;
        
        
        if ( _player.PlayerInput.InteractDown)
        {
            _currentInteractables.Peek().InteractDown(_player.gameObject);
        }else if (_player.PlayerInput.InteractHold)
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
        var interactable = other.GetComponentInParent<IInteractablePercent>();
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
        var interactable = other.GetComponentInParent<IInteractablePercent>();

        var peek = _currentInteractables.Peek();
        if (peek!=null && interactable == peek)
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
public class PlayerMover
{
    private readonly Player _player;

    public PlayerMover(Player player)
    {
        _player = player;
    }

    public void Tick()
    {
        var movement = new Vector3(_player.PlayerInput.Horizontal, _player.PlayerInput.Vertical, 0);
        movement.Normalize();

        _player.transform.position += Time.fixedDeltaTime * _player.Speed * movement;
    }
}

public class PlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool InteractHold => Input.GetKey(KeyCode.E);
    public bool InteractDown => Input.GetKeyDown(KeyCode.E);
}

public class ItsAlmostAStack<T>
{
    private List<T> items = new List<T>();
    public int Count => items.Count;
    
    public void Push(T item)
    {
        items.Add(item);
    }

    public void Pop()
    {
        T temp = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
    }

    public T Peek()
    {
        return items.Count > 0 ? items[items.Count - 1] : default(T);
    }

    public void Remove(int itemAtPosition)
    {
        items.RemoveAt(itemAtPosition);
    }

    public void Remove(T item)
    {
        items.Remove(item);
    }

    public bool Contains(T item)
    {
        return items.Contains(item);
    }
}