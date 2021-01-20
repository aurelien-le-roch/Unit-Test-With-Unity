using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UiCanvasQteResult : MonoBehaviour
{
    [SerializeField] private RectTransform _mediumBar;
    [SerializeField] private RectTransform _perfectBar;
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private TextMeshProUGUI _resultText;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(float lifeTime, QteResult result, Vector3 mediumScale, Vector3 perfectScale, float cursorEndPositonX)
    {
        Destroy(gameObject,lifeTime);
        _canvasGroup.alpha = 1;
        
        _mediumBar.localScale=mediumScale;
        _perfectBar.localScale=perfectScale;
        _cursor.anchoredPosition=new Vector2(cursorEndPositonX,_cursor.anchoredPosition.y);
        _resultText.text = $"Result = {result}";
        transform.DOMoveY(transform.position.y + 1, lifeTime * 0.9f);
        _canvasGroup.DOFade(0,lifeTime * 0.9f);
    }
}