using System;
using UnityEngine;

public class Player : MonoBehaviour, IHaveWorkController,IPlayer,IHaveInventories
{
    [SerializeField] private float _speed;
    public IWorkController WorkController { get; private set; }
    public IPlayerInput PlayerInput { get; set; }
    public float Speed => _speed;

    private PlayerMover _mover;
    public PlayerCamera PlayerCamera;
    private bool _inputEnable;

    public IPlayerHandleInteractable HandleInteractable { get; set; }
    
    public IResourceInventory ResourceInventory { get; private set; }
    public IRecipeInventory RecipeInventory { get; private set; }
    public CraftController CraftController { get; private set; }

    private void Awake()
    {
        WorkController = new WorkControllerTest();
        PlayerInput=new PlayerInput();
        EnableInput(true);
        _mover = new PlayerMover(this);
        PlayerCamera = new PlayerCamera(FindObjectOfType<Camera>());
        PlayerCamera.SetTarget(transform);
        
        HandleInteractable = new PlayerHandleInteractable(this);
        ResourceInventory = new ResourceInventory();
        RecipeInventory = new RecipeInventory(this);
        CraftController=new CraftController(this);
    }

    private void Update()
    {
        if(_inputEnable==false)
            return;
        HandleInteractable.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        PlayerCamera.Tick();
        
        if(_inputEnable==false)
            return;
        _mover.Tick();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var haveIInteraclable = other.GetComponentInParent<IHaveIInteraclable>();
        if(haveIInteraclable!=null)
            HandleInteractable.OnTriggerEnter2D(haveIInteraclable.Interaclable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var haveIInteraclable = other.GetComponentInParent<IHaveIInteraclable>();
        if(haveIInteraclable!=null)
            HandleInteractable.OnTriggerExit2D(haveIInteraclable.Interaclable);
    }

    public void EnableInput(bool value)
    {
        _inputEnable = value;
    }
}

public interface IHaveIResourceInventory
{
    IResourceInventory ResourceInventory { get; }
}

public interface IHaveRecipeInventory
{
    IRecipeInventory RecipeInventory { get; }
}

public interface IHaveInventories : IHaveIResourceInventory,IHaveRecipeInventory
{
}

public class PlayerCamera
{
    private Camera _camera;
    private Transform _cameraTransform;
    private Transform _target;

    public PlayerCamera(Camera camera)
    {
        SetNewCamera(camera);
    }
    public void Tick()
    {
        var targetPosition = _target.position;
        _camera.transform.position = new Vector3(targetPosition.x,targetPosition.y,-10);
    }

    public void SetNewCamera(Camera camera)
    {
        if(_camera!=null)
            _camera.gameObject.SetActive(false);
        
        _camera = camera;
        _cameraTransform = camera.transform;
        _camera.gameObject.SetActive(true);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}