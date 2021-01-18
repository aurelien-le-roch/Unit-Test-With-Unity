using DG.Tweening;
using TMPro;
using UnityEngine;

public class UiQTEMiningCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _mediumBar;
    [SerializeField] private RectTransform _perfectBar;
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private TextMeshProUGUI _resultText;
    
    private QTEMining _QTEMining;

    private void Start()
    {
        _QTEMining = GetComponentInParent<IHaveQTEMining>().QTEMining;
        _QTEMining.OnQTESetup += HandleQteSetup;
        _QTEMining.OnStartQte += HandleQTESTart;
        
        _QTEMining.OnQTEEnd += HandleQTEEnd;
    }

    private void HandleQteSetup(float totalTime, float mediumTime, float perfectTime)
    {
        var mediumPercent = 1/(totalTime / mediumTime);
        mediumPercent= Mathf.Round(mediumPercent * 100f) / 100f;
        
        var perfectPercent = 1/(totalTime / perfectTime);
        perfectPercent= Mathf.Round(perfectPercent * 100f) / 100f;
        
        _mediumBar.localScale=new Vector3(mediumPercent,1,1);
        _perfectBar.localScale=new Vector3(perfectPercent,1,1);
        _cursor.anchoredPosition=Vector3.zero;
        
        _background.gameObject.SetActive(true);
    }

    private void HandleQTESTart(float totalTime)
    {
        _cursor.DOAnchorPosX(_background.rect.width, totalTime).SetEase(Ease.Linear);
    }

    private void HandleQTEEnd(QTEResult result)
    {
        _resultText.text = $"Result = {result}";
        _resultText.gameObject.SetActive(true);
        _cursor.DOKill();
    }
}