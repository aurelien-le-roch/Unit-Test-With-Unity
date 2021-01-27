using System;
using UnityEngine;
using UnityEngine.UI;

public class UiInteraclablePercenPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UiInteractablePercentCanvas uiInteractablePercentCanvas;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private Image _progressBar;

    public float FillAmount => _progressBar.fillAmount;
    
    
    private void OnEnable()
    {
        _canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if (uiInteractablePercentCanvas.InteractablePercent.IHavePlayerFocus)
        {
            if (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
            }

            _progressBar.fillAmount=uiInteractablePercentCanvas.InteractablePercent.InteractPercent;
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

