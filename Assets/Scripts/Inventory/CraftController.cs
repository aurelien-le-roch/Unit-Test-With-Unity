using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CraftController : ICraftController
{
    private readonly IHaveInventories _iHaveInventories;
    private IRecipeInventory RecipeInventory => _iHaveInventories.RecipeInventory;

    private RecipeDefinition _currentRecipeInFocus;
    private Scene _miniGameScene;
    private EmptyCraftMiniGame _emptyCraftMiniGame;

    public event Action<RecipeDefinition> OnNewCurrentRecipeFocus;
    public event Action OnRecipeFocusReset;

    public CraftController(IHaveInventories iHaveInventories)
    {
        _iHaveInventories = iHaveInventories;
    }

    public void SetNewCurrentRecipeInFocus(RecipeDefinition recipeDefinition)
    {
        var recipeInInventory = RecipeInventory.Contain(recipeDefinition);

        if (recipeInInventory == false)
            return;

        var amountThatCanBeCrafted = recipeDefinition.GetCraftableAmount(_iHaveInventories);

        if (amountThatCanBeCrafted <= 0)
            return;


        _currentRecipeInFocus = recipeDefinition;

        OnNewCurrentRecipeFocus?.Invoke(_currentRecipeInFocus);
    }

    public void TryToStartCraft()
    {
        if (_currentRecipeInFocus == null)
            return;

        if (_currentRecipeInFocus.GetCraftableAmount(_iHaveInventories) < 1)
            return;

        var recipeResult = _currentRecipeInFocus.GetRecipeResult();
        if (recipeResult == null)
            return;

        StartCraft();
    }

    private void StartCraft()
    {
        if (_currentRecipeInFocus.CraftMiniGamesEnum == CraftMiniGamesEnum.None)
        {
            EndCraft(CraftResultEnum.Win);
            return;
        }


        var loadSceneAsync =
            SceneManager.LoadSceneAsync(_currentRecipeInFocus.CraftMiniGamesEnum.ToString(), LoadSceneMode.Additive);
        loadSceneAsync.completed += HandleCraftMiniGamesLoaded;
        //load scene
        //when loaded find craftMiniGame script
        // register for end mini game 
    }

    private void HandleCraftMiniGamesLoaded(AsyncOperation obj)
    {
        _miniGameScene = SceneManager.GetSceneByName(_currentRecipeInFocus.CraftMiniGamesEnum.ToString());

        foreach (var rootGameObject in _miniGameScene.GetRootGameObjects())
        {
            _emptyCraftMiniGame = rootGameObject.GetComponent<EmptyCraftMiniGame>();
            _emptyCraftMiniGame.OnMiniGameResult += EndCraft;
            break;
        }

        obj.completed -= HandleCraftMiniGamesLoaded;
    }

    private void EndCraft(CraftResultEnum craftResultEnum)
    {
        if (_miniGameScene.isLoaded)
        {
            _emptyCraftMiniGame.OnMiniGameResult -= EndCraft;
            _emptyCraftMiniGame = null;
            SceneManager.UnloadSceneAsync(_miniGameScene);
        }

        if (craftResultEnum == CraftResultEnum.Win)
            RemoveObjectNeededAndCraftObject();
        else if (craftResultEnum == CraftResultEnum.Lose)
            RemoveObjectNeededButDontCraft();
        
        _currentRecipeInFocus = null;
        OnRecipeFocusReset?.Invoke();
    }

    private void RemoveObjectNeededButDontCraft()
    {
        foreach (var itemNeeded in _currentRecipeInFocus.InInventoryObjectsNeeded)
        {
            itemNeeded.ICanBeAddedToInventories.RemoveFromInventory(_iHaveInventories, itemNeeded.Amount);
        }
    }

    private void RemoveObjectNeededAndCraftObject()
    {
        var recipeResult = _currentRecipeInFocus.GetRecipeResult();
        if (recipeResult == null)
            return;

        foreach (var itemNeeded in _currentRecipeInFocus.InInventoryObjectsNeeded)
        {
            itemNeeded.ICanBeAddedToInventories.RemoveFromInventory(_iHaveInventories, itemNeeded.Amount);
        }

        recipeResult.AddToInventory(_iHaveInventories, 1);
    }
}

public enum CraftResultEnum
{
    Win,
    Lose,
}