using System;
using System.Collections.Generic;
using System.Linq;

public interface ICraftController
{
    event Action<Dictionary<RecipeDefinition, int>> OnRecipeCraftableAmountChange;
    event Action<RecipeDefinition> OnNewCurrentRecipeFocus;
    event Action OnRecipeFocusReset;
    void SetNewCurrentRecipeInFocus(RecipeDefinition recipeDefinition);
    void Craft();
}

public class CraftController : ICraftController
{
    private readonly Player _player;
    private IResourceInventory ResourceInventory => _player.ResourceInventory;
    private IRecipeInventory RecipeInventory => _player.RecipeInventory;

    private Dictionary<RecipeDefinition, int> _recipeCraftableAmount = new Dictionary<RecipeDefinition, int>();
    private RecipeDefinition _currentRecipeInFocus;

    private IResourceInventory _resourceInventoryForHoldingCraftResources;
    
    public event Action<Dictionary<RecipeDefinition, int>> OnRecipeCraftableAmountChange;
    public event Action<RecipeDefinition> OnNewCurrentRecipeFocus;
    public event Action OnRecipeFocusReset;

    
    public CraftController(Player player)
    {
        _resourceInventoryForHoldingCraftResources=new ResourceInventory();
        _player = player;
        ResourceInventory.OnResourceChange += RefreshRecipeCraftableAmount;
        RecipeInventory.OnRecipeAdded += RefreshRecipeCraftableAmount;
    }

    private void RefreshRecipeCraftableAmount(List<RecipeDefinition> recipes)
    {
        AddRecipeToDictionaryKey(recipes);
        RefreshRecipeCraftableAmount();
    }

    private void AddRecipeToDictionaryKey(List<RecipeDefinition> recipesOwn)
    {
        foreach (var recipeDefinitionOwn in recipesOwn)
        {
            if (_recipeCraftableAmount.ContainsKey(recipeDefinitionOwn) == false)
            {
                _recipeCraftableAmount.Add(recipeDefinitionOwn, 0);
            }
        }
    }

    private void RefreshRecipeCraftableAmount(List<ResourceDefinitionWithAmount> resourcesOwn)
    {
        RefreshRecipeCraftableAmount();
    }

    private void RefreshRecipeCraftableAmount()
    {
        var amountChange = false;
        foreach (var key in _recipeCraftableAmount.Keys.ToList())
        {
            var newAmount = key.GetCraftableAmount(ResourceInventory);
            if (newAmount != _recipeCraftableAmount[key])
            {
                _recipeCraftableAmount[key] = newAmount;
                amountChange = true;
            }
        }

        if (amountChange)
        {
            OnRecipeCraftableAmountChange?.Invoke(_recipeCraftableAmount);
        }
    }
    public void SetNewCurrentRecipeInFocus(RecipeDefinition recipeDefinition)
    {
        var recipeInInventory = RecipeInventory.Contain(recipeDefinition);

        if (recipeInInventory == false)
            return;
        
        var amountThatCanBeCrafted = recipeDefinition.GetCraftableAmount(ResourceInventory);
        
        if(amountThatCanBeCrafted<=0)
            return;

        if (_currentRecipeInFocus != null)
        {
            //need to check that _resourceInventoryForHoldingCraftResources have the resources ?
            _resourceInventoryForHoldingCraftResources.SendResourceToOtherInventory(ResourceInventory,
                _currentRecipeInFocus.ResourcesNeeded);
        }
        
        _currentRecipeInFocus = recipeDefinition;
        
        //send ResourceToOtherInventory
        bool resourceGetSend =ResourceInventory.SendResourceToOtherInventory(_resourceInventoryForHoldingCraftResources,recipeDefinition.ResourcesNeeded);

        if (resourceGetSend == false)
            _currentRecipeInFocus = null;
        
        OnNewCurrentRecipeFocus?.Invoke(_currentRecipeInFocus);
    }

    public void Craft()
    {
        if(_currentRecipeInFocus==null)
            return;
        
        if(_currentRecipeInFocus.GetCraftableAmount(_resourceInventoryForHoldingCraftResources)<1)
            return;

        var recipeResult = _currentRecipeInFocus.GetRecipeResult();
        if(recipeResult==null)
            return;

        _resourceInventoryForHoldingCraftResources.RemoveAll();
        _currentRecipeInFocus = null;
        OnRecipeFocusReset?.Invoke();
        recipeResult.AddToInventories(_player);
    }
}