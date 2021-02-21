using System;

public class WorkControllerTest : IWorkController
{
    private OreMiningController _oreMiningController;
    public CraftController2 CraftController { get;}
    public WorkControllerTest()
    {
        _oreMiningController = new OreMiningController();
        CraftController=new CraftController2();
    }

    public QteMiningSetting ProcessOreMining(OreNodeInteractable oreNodeInteractable)
    {
        return _oreMiningController.ProcessOreMining(oreNodeInteractable);
    }
}

public class CraftController2
{
    public event Action<IHaveInventories, CraftInfo, AimStateMachine> OnCraftWorkshopInteraction;
    public event Action OnPlayerLeaveCraftWorkShopArea; 
    private IHaveInventories _currentInventories;
    private CraftInfo _currentCraftInfo;
    private AimStateMachine _currentMiniGame;
    private RecipeDefinition _currentRecipeDefinition;
    private CraftXp _craftXp=new CraftXp();
    private int _currentMaxScore=-1;

    public void ProcessCraftWorkshopInteraction(IHaveInventories haveInventories, CraftInfo craftInfo,
        AimStateMachine aimStateMachine)
    {
        _currentInventories = haveInventories;
        _currentCraftInfo = craftInfo;
        _currentMiniGame = aimStateMachine;
        OnCraftWorkshopInteraction?.Invoke(haveInventories,craftInfo,aimStateMachine);
        //update and enable ui with event
    }

    public void PlayerLeaveCraftWorkshopArea()
    {
        _currentInventories = null;
        _currentCraftInfo = null;
        _currentMiniGame =  null;
        _currentRecipeDefinition = null;
        _currentMaxScore = -1;
        OnPlayerLeaveCraftWorkShopArea?.Invoke();
    }
    public bool TryToStartCraft(RecipeDefinition recipeDefinition)
    {
        _currentRecipeDefinition = recipeDefinition;

        if (!IsSetup() || !HaveRecipe() || !HaveResources())
        {
            _currentRecipeDefinition = null;
            return false;
        }

        if (_currentMiniGame == null || EnoughXpForInstantCraft())
        {
            RemoveResources();
            Craft();
            GiveCraftXp(RecipeResultRarity());
            return true;
        }
        
        RemoveResources();
        return TryToStartMiniGame();
    }
    
    private bool TryToStartMiniGame()
    {
        if(_currentMiniGame.CanStartMiniGame==false)
            return false;
        
        var rarity = _currentRecipeDefinition.GetRecipeResultQuality();
        _currentMaxScore = _currentCraftInfo.GetMiniGameMaxScore(rarity);
        _currentMiniGame.TryToBeginMiniGame(_currentMaxScore);
        _currentMiniGame.OnMiniGameEnd += HandleMiniGameResult;
        return true;
    }

    private void HandleMiniGameResult(int result)
    {
        _currentMiniGame.OnMiniGameEnd -= HandleMiniGameResult;
        
        var rarityOfResult = _currentCraftInfo.GetRarityFromMiniGameScore(result);
        
        GiveCraftXp(rarityOfResult);

        if (MiniGameIsWin(result))
        {
            Craft();
        }
        else
        {
            GiveFragments(rarityOfResult);
        }
    }

    private void GiveFragments(ObjectRarity rarityOfResult)
    {
        var fragmentsAmount = _currentCraftInfo.GetFragments(rarityOfResult);
    }

    private void Craft()
    {
        var craftResult = _currentRecipeDefinition.GetRecipeResult();
        craftResult.AddToInventory(_currentInventories,1);
    }

    private void RemoveResources()
    {
        var resourcesNeeded = _currentRecipeDefinition.InInventoryObjectsNeeded;
        foreach (var resourceNeeded in resourcesNeeded)
        {
            var amountToRemove = resourceNeeded.Amount;
            resourceNeeded.ICanBeAddedToInventories.RemoveFromInventory(_currentInventories,amountToRemove);
        }
    }
    private void GiveCraftXp(ObjectRarity rarity)
    {
        var xpToGive = _currentCraftInfo.GetXpToGive(rarity);
        _craftXp.IncreaseXp(xpToGive);
    }
    
    
    
    private bool IsSetup()=>_currentCraftInfo!=null && _currentCraftInfo !=null;
    private bool HaveRecipe()=>
        _currentInventories.RecipeInventory.Contain(_currentRecipeDefinition);
    private bool HaveResources() =>
        _currentRecipeDefinition.GetCraftableAmount(_currentInventories) > 0;
    private ObjectRarity RecipeResultRarity() =>
        _currentRecipeDefinition.GetRecipeResultQuality();
    private bool EnoughXpForInstantCraft()
    {
        var rarity = _currentRecipeDefinition.GetRecipeResultQuality();
        var xpNeeded = _currentCraftInfo.GetXpNeeded(rarity);
        return _craftXp.Xp >= xpNeeded;
    }
    private bool MiniGameIsWin(int result)=> result >= _currentMaxScore;
}
public class CraftXp
{
    public int Xp { get;private set; }

    public void IncreaseXp(int amount)
    {
        Xp+=amount;
    }
}