using UnityEngine;

public class UiCraftRecipes : MonoBehaviour
{
    [SerializeField] private UiRecipeSlot[] _slots;
    [SerializeField] private CraftPanel _craftPanel;
    [SerializeField] private GameObject _view;
    private CraftController2 _craftController;

    public void Bind(CraftController2 craftController)
    {
        _craftController = craftController;
        _craftController.OnCraftWorkshopInteraction += RefreshAllSlots;
        _craftPanel.OnCraftButtonClick += SendCraftRequest;
        foreach (var slot in _slots)
        {
            slot.OnRecipeSlotGetClicked += RefreshCraftPanel;
        }
        DisableAllSlot();
        ClearAllSlot();
    }


    private void RefreshCraftPanel(RecipeDefinition selectedRecipeDefinition)
    {
        _craftPanel.Refresh(selectedRecipeDefinition);
    }
    private void SendCraftRequest(RecipeDefinition recipeDefinitionToCraft)
    {
        if(recipeDefinitionToCraft==null)
            return;
        
        var craftStart = _craftController.TryToStartCraft(recipeDefinitionToCraft);
        if(craftStart)
            _view.SetActive(false);
    }
    private void RefreshAllSlots(IHaveInventories haveInventories, CraftInfo craftInfo, AimStateMachine miniGame)
    {
        var recipeInventory = haveInventories.RecipeInventory;
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < recipeInventory.Recipes.Count)
            {
                var recipeDefinition =recipeInventory.Recipes[i].Definition;
                var numberOfRecipe = recipeInventory.Recipes[i].Amount;
                var craftableAmount = recipeDefinition.GetCraftableAmount(haveInventories);
                _slots[i].BindToNewRecipeDefinition(recipeDefinition,numberOfRecipe,craftableAmount);
                _slots[i].gameObject.SetActive(true);
                continue;
            }
            
            _slots[i].Clear();
        }
        _view.SetActive(true);
    }

    private void ClearAllSlot()
    {
        foreach (var slot in _slots)
        {
            slot.Clear();
        }
    }
    
    private void DisableAllSlot()
    {
        foreach (var slot in _slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _craftController.OnCraftWorkshopInteraction -= RefreshAllSlots;
        _craftPanel.OnCraftButtonClick -= SendCraftRequest;
        foreach (var slot in _slots)
        {
            slot.OnRecipeSlotGetClicked -= RefreshCraftPanel;
        }
    }
}