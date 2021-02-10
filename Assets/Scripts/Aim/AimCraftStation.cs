using System;
using UnityEngine;

public class AimCraftStation : MonoBehaviour, IHaveIHandlePlayerInteractableFocus, IHaveIInteraclable
{
    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; }
    public IInteraclable Interaclable { get; }
}

//public class AimCraftStationInteraclable : IHandlePlayerInteractableFocus, IInteraclable
//{
//    public event Action OnPlayerFocusMe;
//    public event Action OnPlayerStopFocusMe;
//    public bool IHavePlayerFocus { get; private set; }
//
//    private IHaveInventories _haveInventories;
//    private AimStateMachine _aimStateMachine;
//
//    private ObjectQualityGiveIntDefinition _xpNeededDefinition;
//    private ObjectQualityGiveIntDefinition _xpToGiveDefinition;
//    private ObjectQualityGiveIntDefinition _miniGameMaxDefinition;
//    private ObjectQualityGiveIntDefinition _fragmentDefinition;
//    
//
//    public void PlayerStartToFocusMe()
//    {
//        IHavePlayerFocus = true;
//        OnPlayerFocusMe?.Invoke();
//    }
//
//    public void PlayerStopToFocusMe()
//    {
//        IHavePlayerFocus = false;
//        OnPlayerStopFocusMe?.Invoke();
//    }
//
//    public void InteractDown(GameObject interactor)
//    {
//        //enable craft ui
//        if (_haveInventories == null)
//        {
//            _haveInventories = interactor.GetComponent<IHaveInventories>();
//            //_miniGameXp = interactor.GetComponent<MiniGameXp>();
//        }
//    }
//
//    public void InteractHold(GameObject interactor, float deltaTime)
//    {
//    }
//
//    public void DontInteract(float deltaTime)
//    {
//    }
//
//    public void TryToStartCraft(RecipeDefinition recipeDefinition)
//    {
//        if (_haveInventories == null)
//            return;
//
//        var resourcesForCraft = recipeDefinition.GetCraftableAmount(_haveInventories) > 0;
//
//        if (resourcesForCraft == false)
//            return;
//
//        var playerXp = _miniGameXp.GetAimXp;
//
//        //ici on peut check si il s'agit d'un craftworkshop avec un mod 2fois moin d xp needed
//
//        var craftResultQuality = recipeDefinition.getRecipeResultQuality;
//        var xpNeeded = _xpNeededDefinition.GetValue(craftResultQuality);
//        if (playerXp >= xpNeeded)
//        {
//            //remove resource
//            //craft objet
//            var xpToGive = _xpToGiveDefinition.GetValue(craftResultQuality);
//            _miniGameXp.GiveXpToAim(xpToGive);
//            return;
//        }
//
//        if (_aimStateMachine.CanStartMiniGame == false)
//            return;
//
//        //remove resource
//        //start mini game
//        var miniGameMax = _miniGameMaxDefinition.GetValue(craftResultQuality);
//        _aimStateMachine.TryToBeginMiniGame(miniGameMax);
//        //register event
//    }
//
//    private void HandleMiniGameResult(int result)
//    {
//
//        var resultQuality = _miniGameMaxDefinition.GetRarity(result);
//        var xpToGive = _xpToGiveDefinition.GetValue(resultQuality);
//        _miniGameXp.GiveXpToAim(xpToGive);
//
//        if (miniGameWin)
//        {
//            //craft
//        }
//        else
//        {
//            var fragmentAmount = _fragmentDefinition.GetValue(resultQuality);
//        }
//            //if result == max
//        //craft item
//        //give xp
//        //else 
//        //give fragment
//        //give xp
//
//
//        _miniGameXp.GiveXpToAim();
//    }
//}

public enum ObjectRarity
{
    White,
    Green,
    Blue,
    Purple,
    Orange,
}
public class ObjectQualityGiveIntDefinition : ScriptableObject
{
    [SerializeField] private int _white;
    [SerializeField] private int _green;
    [SerializeField] private int _blue;
    [SerializeField] private int _purple;
    [SerializeField] private int _orange;

    public int GetValue(ObjectRarity rarity)
    {
        switch (rarity)
        {
            case ObjectRarity.White:
                return _white;
            case ObjectRarity.Green:
                return _green;
            case ObjectRarity.Blue:
                return _blue;
            case ObjectRarity.Purple:
                return _purple;
            case ObjectRarity.Orange:
                return _orange;
            default:
                return 0;
        }
    }

    public ObjectRarity GetRarity(int amount)
    {
        if (GetValue(ObjectRarity.Orange) <= amount)
            return ObjectRarity.Orange;

        if (GetValue(ObjectRarity.Purple) <= amount)
            return ObjectRarity.Purple;

        if (GetValue(ObjectRarity.Blue) <= amount)
            return ObjectRarity.Blue;

        if (GetValue(ObjectRarity.Green) <= amount)
            return ObjectRarity.Green;

        return ObjectRarity.White;
    }
}