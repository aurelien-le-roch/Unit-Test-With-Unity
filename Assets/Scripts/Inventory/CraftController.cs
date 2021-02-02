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
    private readonly IHaveInventories _iHaveInventories;
    private IResourceInventory ResourceInventory => _iHaveInventories.ResourceInventory;
    private IRecipeInventory RecipeInventory => _iHaveInventories.RecipeInventory;

    private Dictionary<RecipeDefinition, int> _recipeCraftableAmount = new Dictionary<RecipeDefinition, int>();
    private RecipeDefinition _currentRecipeInFocus;

    private IResourceInventory _resourceInventoryForHoldingCraftResources;
    
    public event Action<Dictionary<RecipeDefinition, int>> OnRecipeCraftableAmountChange;
    public event Action<RecipeDefinition> OnNewCurrentRecipeFocus;
    public event Action OnRecipeFocusReset;

    
    public CraftController(IHaveInventories iHaveInventories)
    {
        _resourceInventoryForHoldingCraftResources=new ResourceInventory();
        _iHaveInventories = iHaveInventories;
        ResourceInventory.OnResourceChange += RefreshRecipeCraftableAmount;
        RecipeInventory.OnRecipeChange += RefreshRecipeCraftableAmount;
    }

    private void RefreshRecipeCraftableAmount(List<RecipeDefinitionWithAmount> recipes)
    {
        AddRecipeToDictionaryKey(recipes);
        RefreshRecipeCraftableAmount();
    }

    private void AddRecipeToDictionaryKey(List<RecipeDefinitionWithAmount> recipesOwn)
    {
        foreach (var recipeDefinitionOwn in recipesOwn)
        {
            if (_recipeCraftableAmount.ContainsKey(recipeDefinitionOwn.Definition) == false)
            {
                _recipeCraftableAmount.Add(recipeDefinitionOwn.Definition, 0);
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
            var newAmount = key.GetCraftableAmount(_iHaveInventories);
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
        
        var amountThatCanBeCrafted = recipeDefinition.GetCraftableAmount(_iHaveInventories);
        
        if(amountThatCanBeCrafted<=0)
            return;

        
        _currentRecipeInFocus = recipeDefinition;
        
        OnNewCurrentRecipeFocus?.Invoke(_currentRecipeInFocus);
    }

    public void Craft()
    {
        if(_currentRecipeInFocus==null)
            return;
        
        if(_currentRecipeInFocus.GetCraftableAmount(_iHaveInventories)<1)
            return;

        var recipeResult = _currentRecipeInFocus.GetRecipeResult();
        if(recipeResult==null)
            return;

        foreach (var itemNeeded in _currentRecipeInFocus.InInventoryObjectsNeeded)
        {
            itemNeeded.ICanBeAddedToInventories.RemoveFromInventory(_iHaveInventories,itemNeeded.Amount);
        }
        _currentRecipeInFocus = null;
        OnRecipeFocusReset?.Invoke();
        recipeResult.AddToInventory(_iHaveInventories,1);
    }
}