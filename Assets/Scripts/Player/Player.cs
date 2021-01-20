using System;
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