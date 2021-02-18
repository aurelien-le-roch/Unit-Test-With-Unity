﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UiCraftRecipes))]

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

        var uiCraftRecipes = GetComponent<UiCraftRecipes>();
        
        uiCraftRecipes.Bind(player.WorkController.CraftController);
    }
}