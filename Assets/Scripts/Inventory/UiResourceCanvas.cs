using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiResourceCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField]private UiResourceSlot[] _resourceSlots;
    private IResourceInventory _resourceInventory;
    public UiResourceSlot[] Slots => _resourceSlots;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            EnableDisablePanel();
    }

    public void Bind(IResourceInventory resourceInventory)
    {
        _resourceInventory =resourceInventory;
        _resourceInventory.OnResourceAdded += HandleResourceAdded;
        ClearAllSlots();
    }
    
    private void ClearAllSlots()
    {
        foreach (var slot in _resourceSlots)
        {
            slot.Clear();
        }
    }

    private void HandleResourceAdded(List<ResourceDefinitionWithAmount> playerResources)
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

    public void EnableDisablePanel()
    {
        _panel.SetActive(!_panel.activeSelf);
    }
}