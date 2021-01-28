using System.Collections;
using UnityEngine;

public class UiBindingUiResourceCanvas : MonoBehaviour
{
    IEnumerator Start()
    {
        var player = FindObjectOfType<Player>();
        while (player == null)
        {
            yield return null;
            player = FindObjectOfType<Player>();
        }
        
        GetComponent<UiResourceCanvas>().Bind(player.ResourceInventory);
    }
}