using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UiRecipesCanvas))]
[RequireComponent(typeof(UiResourcesCanvas))]
[RequireComponent(typeof(UiCraftCanvas))]
public class UiBindingUiCraftCanvas : MonoBehaviour
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
        var uiResourceCanvas = GetComponent<UiResourcesCanvas>();
        var uiCraftCanvas = GetComponent<UiCraftCanvas>();
        
        uiRecipesCanvas.Bind(player.RecipeInventory);
        uiRecipesCanvas.BindCraftControllerForSlot(player.CraftController);
        uiResourceCanvas.Bind(player.ResourceInventory);
        uiCraftCanvas.Bind(player.CraftController);
    }
}