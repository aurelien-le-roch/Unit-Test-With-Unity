using System;
using System.Collections.Generic;
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
        IHavePlayerFocus = false;
        OnPlayerStopFocusMe?.Invoke();
    }

    public void InteractDown(GameObject interactor)
    {
        var haveInventories = interactor.GetComponent<IHaveInventories>();
        var haveWorkController = interactor.GetComponent<IHaveWorkController>();
        if (haveInventories != null && haveWorkController != null)
        {
            haveWorkController.WorkController.CraftController.ProcessCraftWorkshopInteraction(haveInventories,_craftInfo,_aimStateMachine);
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

[CreateAssetMenu(menuName = "Craft/RaritiesValue")]
public class RaritiesValuesDefinition : ScriptableObject
{
    [SerializeField] private List<ObjectRarityAndValue> _objectRarityAndValues;

    public int GetValue(ObjectRarity objectRarity)
    {
        foreach (var objectRarityAndValue in _objectRarityAndValues)
        {
            if (objectRarityAndValue.ObjectRarity == objectRarity)
                return objectRarityAndValue.Value;
        }

        return 0;
    }
}

[CreateAssetMenu(menuName = "Craft/CraftInfo")]
public class CraftInfo : ScriptableObject
{
    [SerializeField] private RaritiesValuesDefinition _xpNeeded;
    [SerializeField] private RaritiesValuesDefinition _xpToGive;
    [SerializeField] private RaritiesValuesDefinition _miniGameMaxScore;
    [SerializeField] private RaritiesValuesDefinition _fragments;

    public int GetXpNeeded(ObjectRarity rarity)
    {
        return _xpNeeded.GetValue(rarity);
    }
    public int GetXpToGive(ObjectRarity rarity)
    {
        return _xpToGive.GetValue(rarity);
    }
    public int GetMiniGameMaxScore(ObjectRarity rarity)
    {
        return _miniGameMaxScore.GetValue(rarity);
    }
    public int GetFragments(ObjectRarity rarity)
    {
        return _fragments.GetValue(rarity);
    }

    public ObjectRarity GetRarityFromMiniGameScore(int score)
    {
        if(score>=GetMiniGameMaxScore(ObjectRarity.Orange))
            return ObjectRarity.Orange;
        
        if(score>=GetMiniGameMaxScore(ObjectRarity.Purple))
            return ObjectRarity.Purple;
        if(score>=GetMiniGameMaxScore(ObjectRarity.Blue))
            return ObjectRarity.Blue;
        
        if(score>=GetMiniGameMaxScore(ObjectRarity.Green))
            return ObjectRarity.Green;
        
        return ObjectRarity.White;
    }
}