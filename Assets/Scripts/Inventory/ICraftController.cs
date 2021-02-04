public interface ICraftController
{
    //event Action<Dictionary<RecipeDefinition, int>> OnRecipeCraftableAmountChange;
    //event Action<RecipeDefinition> OnNewCurrentRecipeFocus;
    //event Action OnRecipeFocusReset;
    void SetNewCurrentRecipeInFocus(RecipeDefinition recipeDefinition);
    //void Craft();
}