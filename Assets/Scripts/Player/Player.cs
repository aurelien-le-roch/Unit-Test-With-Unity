using System;
using UnityEngine;

public class Player : MonoBehaviour, IHaveWorkController,IPlayer,IHaveInventories,ICharacterWithSpells
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
    //public CraftController CraftController { get; private set; }
    public CharacterSpells CharacterSpells { get; private set; }
    private PlayerUseSpell _playerUseSpell;
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
        //CraftController=new CraftController(this);
        CharacterSpells=new CharacterSpells(transform);
        _playerUseSpell = new PlayerUseSpell(this);
    }

    private void Update()
    {
        CharacterSpells.Tick(Time.deltaTime);
        if(_inputEnable==false)
            return;
        HandleInteractable.Tick(Time.deltaTime);
        _playerUseSpell.Tick();
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

    public Vector3 GetWorldMousePosition(Vector3 mousePosition)
    {
        return _camera.ScreenToWorldPoint(mousePosition);
    }
}

public class PlayerUseSpell
{
    private Player _player;
    private IPlayerInput PlayerInput => _player.PlayerInput;
    private CharacterSpells CharacterSpells => _player.CharacterSpells;
    public PlayerUseSpell(Player player)
    {
        _player = player;
    }

    public void Tick()
    {
        if (PlayerInput.Spell1)
            CharacterSpells.UseSpell(CharacterSpells.Spells[0],GetMouseWorldPosition());
        else
            CharacterSpells.NotUseSpell(CharacterSpells.Spells[0]);

//        if (PlayerInput.Spell2)
//            CharacterSpells.UseSpell(CharacterSpells.Spells[1],GetMouseWorldPosition());
//        else
//            CharacterSpells.NotUseSpell(CharacterSpells.Spells[1]);
//        if (PlayerInput.Spell3)
//            CharacterSpells.UseSpell(CharacterSpells.Spells[2],GetMouseWorldPosition());
//        else
//            CharacterSpells.NotUseSpell(CharacterSpells.Spells[2]);
//        if (PlayerInput.Spell4)
//            CharacterSpells.UseSpell(spellsList[3],worldMousePosition);
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePosition = _player.PlayerInput.MousePosition;
        return _player.PlayerCamera.GetWorldMousePosition(mousePosition);
    }
}