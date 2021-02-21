using System;
using UnityEngine;

public class AimCraftStation : MonoBehaviour, IHaveIHandlePlayerInteractableFocus, IHaveIInteraclable
{
    [SerializeField] private CraftInfo _craftInfo;
    [SerializeField] private AimStateMachine _aimStateMachine;
    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; private set; }
    public IInteraclable Interaclable { get; private set; }

    private void Awake()
    {
        var craftInteractable = new AimCraftStationInteraclable(_craftInfo,_aimStateMachine);
        HandlePlayerInteractableFocus = craftInteractable;
        Interaclable = craftInteractable;
    }
}

public class AimCraftStationInteraclable : IHandlePlayerInteractableFocus, IInteraclable
{
    public event Action OnPlayerFocusMe;
    public event Action OnPlayerStopFocusMe;
    public bool IHavePlayerFocus { get; private set; }

    private IHaveInventories _haveInventories;
    private IHaveWorkController _haveWorkController;
    private AimStateMachine _aimStateMachine;

    private CraftInfo _craftInfo;
    private int _maxGoal;
    public AimCraftStationInteraclable(CraftInfo craftInfo,AimStateMachine aimStateMachine)
    {
        _craftInfo = craftInfo;
        _aimStateMachine = aimStateMachine;
    }
    public void PlayerStartToFocusMe()
    {
        IHavePlayerFocus = true;
        OnPlayerFocusMe?.Invoke();
    }

    public void PlayerStopToFocusMe()
    {
        if (_haveWorkController != null)
        {
            _haveWorkController.WorkController.CraftController.PlayerLeaveCraftWorkshopArea();
            _haveWorkController = null;
        }
        IHavePlayerFocus = false;
        OnPlayerStopFocusMe?.Invoke();
    }

    public void InteractDown(GameObject interactor)
    {
        var haveInventories = interactor.GetComponent<IHaveInventories>();
        _haveWorkController = interactor.GetComponent<IHaveWorkController>();
        if (haveInventories != null && _haveWorkController != null)
        {
            _haveWorkController.WorkController.CraftController.ProcessCraftWorkshopInteraction(haveInventories,_craftInfo,_aimStateMachine);
            var player = interactor.GetComponent<Player>();
            if(player==null)
                return;

            _aimStateMachine.BindToAPlayer(player);
        }
    }

    public void InteractHold(GameObject interactor, float deltaTime)
    {
    }

    public void DontInteract(float deltaTime)
    {
    }
    
}


public enum ObjectRarity
{
    White,
    Green,
    Blue,
    Purple,
    Orange,
}

[Serializable]
public struct ObjectRarityAndValue
{
    public ObjectRarity ObjectRarity;
    public int Value;
}