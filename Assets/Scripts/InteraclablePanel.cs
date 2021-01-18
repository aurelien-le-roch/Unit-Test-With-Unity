using System;
using UnityEngine;
using UnityEngine.UI;

public class InteraclablePanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private InteractableCanvas _interactableCanvas;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private Image _progressBar;
    
    
    private void OnEnable()
    {
        _canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if (_interactableCanvas.InteractablePercent.PlayerInZone)
        {
            if (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
            }

            _progressBar.fillAmount=_interactableCanvas.InteractablePercent.InteractPercent;
        }else if (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    
}

