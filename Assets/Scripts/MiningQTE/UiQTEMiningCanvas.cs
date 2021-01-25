using System;
using DG.Tweening;
using UnityEngine;

public class UiQTEMiningCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _mediumBar;
    [SerializeField] private RectTransform _perfectBar;
    [SerializeField] private RectTransform _cursor;

    [SerializeField] private UiCanvasQteResult _uiCanvasQteResult;

    public GameObject QtePanel => _background.gameObject;
    public Vector3 _mediumBarLocalScale=>_mediumBar.localScale;
    public Vector3 _perfectBarLocalScale=>_perfectBar.localScale;
    private QTEMining _QTEMining;
    private CanvasGroup _canvasGroup;

    
    private void Start()
    {
        _QTEMining = GetComponentInParent<IHaveIHaveQteMining>().HaveQteMining.QTEMining;
        _canvasGroup = GetComponent<CanvasGroup>();
        _QTEMining.OnQTESetup += HandleQteSetup;
        _QTEMining.OnStartQte += HandleQTESTart;
        _QTEMining.OnJobOver += HandleJobOver;
        _QTEMining.OnQTEEnd += HandleQTEEnd;
        _QTEMining.OnQTEReset += HandleReset;
    }

    private void HandleQteSetup(float totalTime, float mediumTime, float perfectTime)
    {
        var mediumPercent = 1/(totalTime / mediumTime);
        mediumPercent= Mathf.Round(mediumPercent * 100f) / 100f;
        
        var perfectPercent = 1/(totalTime / perfectTime);
        perfectPercent= Mathf.Round(perfectPercent * 100f) / 100f;
        
        _mediumBar.localScale=new Vector3(mediumPercent,1,1);
        _perfectBar.localScale=new Vector3(perfectPercent,1,1);
        
        FirstReset();
    }

    private void HandleQTESTart(float totalTime)
    {
        _canvasGroup.DOKill();
        _canvasGroup.alpha = 1;
        _cursor.DOAnchorPosX(_background.rect.width, totalTime).SetEase(Ease.Linear);
    }

    //run after Reset
    private void HandleQTEEnd(QteResult result)
    {
       
    }

    //run befor end
    private void HandleReset(QteResult result)
    {
        var canvasResult = Instantiate(_uiCanvasQteResult, transform.position, Quaternion.identity);
        canvasResult.Setup(2f,result, _mediumBar.localScale,_perfectBar.localScale,_cursor.anchoredPosition.x);
        Debug.Log("Handle reset");
        _background.gameObject.SetActive(true);
        _cursor.DOKill();
        _cursor.anchoredPosition=Vector3.zero;
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.4f);
    }

    private void FirstReset()
    {
        _background.gameObject.SetActive(true);
        _cursor.DOKill();
        _cursor.anchoredPosition=Vector3.zero;
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, 0.4f);
    }
    
    private void HandleJobOver()
    {
        Debug.Log("QTE End");
        _cursor.DOKill();
        _background.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _canvasGroup.DOKill();
        _QTEMining.OnQTESetup -= HandleQteSetup;
        _QTEMining.OnStartQte -= HandleQTESTart;
        _QTEMining.OnJobOver -= HandleJobOver;
        _QTEMining.OnQTEEnd -= HandleQTEEnd;
        _QTEMining.OnQTEReset -= HandleReset;
    }
}