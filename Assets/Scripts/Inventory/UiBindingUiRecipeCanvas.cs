using System.Collections;
using UnityEngine;

public class UiBindingUiRecipeCanvas : MonoBehaviour
{
    IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }

        var uiRecipesCanvas = GetComponent<UiRecipesCanvas>();
        uiRecipesCanvas.Bind(player.RecipeInventory);
        uiRecipesCanvas.BindCraftControllerForSlot(player.CraftController);
    }
}