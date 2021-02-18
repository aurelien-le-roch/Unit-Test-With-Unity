using System;
using UnityEngine;

public class InputHandlerUi : MonoBehaviour
{
    [SerializeField] private GameObject _resourcePanel;
    [SerializeField] private GameObject _craftPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _resourcePanel.SetActive(!_resourcePanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _craftPanel.SetActive(false);
        }
    }
}
