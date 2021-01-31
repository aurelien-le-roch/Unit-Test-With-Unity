using System;
using UnityEngine;

public class Player : MonoBehaviour, IHaveWorkController,IPlayer,IHaveInventories
{
    [SerializeField] private float _speed;

    public IWorkController WorkController { get; private set; }
    public IPlayerInput PlayerInput { get; set; }
    public float Speed => _speed;

    private PlayerMover _mover;

    public IPlayerHandleInteractable HandleInteractable { get; set; }
    
    public IResourceInventory ResourceInventory { get; private set; }
    public IRecipeInventory RecipeInventory { get; private set; }
    public CraftController CraftController { get; private set; }

    private void Awake()
    {
        WorkController = new WorkControllerTest();
        PlayerInput=new PlayerInput();
        _mover = new PlayerMover(this);
        HandleInteractable = new PlayerHandleInteractable(this);
        ResourceInventory = new ResourceInventory();
        RecipeInventory = new RecipeInventory();
        CraftController=new CraftController(this);
    }

    private void Update()
    {
        HandleInteractable.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
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