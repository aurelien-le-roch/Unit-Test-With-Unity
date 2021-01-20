using System;
using System.Collections;
using UnityEngine;

public class QTEMining
{
    private bool _isRunning;
    private float _startTime;
    private float _totalTime;
    private FloatRange _mediumRange;
    private FloatRange _perfectRange;

    private Coroutine _checkMaxTimeRoutine;
    public event Action<float, float, float> OnQTESetup;
    public event Action<QteResult> OnQTEReset;
    public event Action OnJobOver;
    public event Action<float> OnStartQte;
    public event Action<QteResult> OnQTEEnd;
    public bool IsSetup { get; private set; }
    
    

    public void Use(MonoBehaviour coroutineRunner)
    {
        if (_isRunning)
        {
            StopAndPickResult(coroutineRunner);
        }
        else
        {
            StartRunning(coroutineRunner);
        }
    }

    public void SetupQTE(float totalTime, float mediumTime, float perfectTime)
    {
        _totalTime = totalTime;

        var midTime = totalTime / 2;

        var minMediumRange = midTime - mediumTime / 2;
        var maxMediumRange = midTime + mediumTime / 2;

        var minPerfectRange = midTime - perfectTime / 2;
        var maxPerfectRange = midTime + perfectTime / 2;

        _mediumRange = new FloatRange(minMediumRange, maxMediumRange);
        _perfectRange = new FloatRange(minPerfectRange, maxPerfectRange);

        IsSetup = true;
        OnQTESetup?.Invoke(totalTime, mediumTime, perfectTime);
        //Ui register for event, then it prepare + active the qte panel
    }
    private void StartRunning(MonoBehaviour coroutineRunner)
    {
        _startTime = Time.time;
        //_maxTime = Time.time + _totalTime;

        
        _checkMaxTimeRoutine=coroutineRunner.StartCoroutine(CheckForQTEMaxTime(coroutineRunner));

        _isRunning = true;
        OnStartQte?.Invoke(_totalTime);
    }

    private IEnumerator CheckForQTEMaxTime(MonoBehaviour coroutineRunner)
    {
        yield return new WaitForSeconds(_totalTime);
        StopAndPickResult(coroutineRunner);
    }


    private void StopAndPickResult(MonoBehaviour coroutineRunner)
    {
        if (_checkMaxTimeRoutine != null)
        {
            coroutineRunner.StopCoroutine(_checkMaxTimeRoutine);
            _checkMaxTimeRoutine = null;
        }
        
        _isRunning = false;
        
        var timeInQTE = Time.time - _startTime;

        
        if (_perfectRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QteResult.Perfect);
        }
        else if (_mediumRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QteResult.Medium);
        }
        else
        {
            Debug.Log("result fail");
            OnQTEEnd?.Invoke(QteResult.Fail);
        }
    }

    public void Reset(QteResult result)
    {
        OnQTEReset?.Invoke(result);
    }

    public void JobIsOver()
    {
        OnJobOver?.Invoke();
    }
}