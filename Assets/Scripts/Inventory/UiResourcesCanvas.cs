using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiResourcesCanvas : MonoBehaviour
{
    [SerializeField]private UiResourceSlot[] _resourceSlots;
    private IResourceInventory _resourceInventory;
    public UiResourceSlot[] Slots => _resourceSlots;
    
    public void Bind(IResourceInventory resourceInventory)
    {
        _resourceInventory =resourceInventory;
        _resourceInventory.OnResourcesChange += HandleResourcesAdded;
        ClearAllSlots();
    }
    
    private void ClearAllSlots()
    {
        foreach (var slot in _resourceSlots)
        {
            slot.Clear();
        }
    }

    private void HandleResourcesAdded(List<ResourceDefinitionWithAmount> playerResources)
    {
        for (int i = 0; i < _resourceSlots.Length; i++)
        {

            if (i < playerResources.Count)
            {
                _resourceSlots[i].Refresh(playerResources[i]);
            }
            else
            {
                _resourceSlots[i].Clear();
            }
        }
    }
}