using System;
using System.Collections;
using UnityEngine;

public class QTEMining
{
    public bool IsRunning { get; private set; }
    private float _startTime;
    public float TotalTime { get; private set; }
    public FloatRange MediumRange { get; private set; }
    public FloatRange PerfectRange { get; private set; }

    private Coroutine _checkMaxTimeRoutine;
    public event Action<float, float, float> OnQTESetup;
    public event Action<QteResult> OnQTEReset;
    public event Action OnJobOver;
    public event Action<float> OnStartQte;
    public event Action<QteResult> OnQTEEnd;
    public bool IsSetup { get; private set; }
    
    public void Use(MonoBehaviour coroutineRunner,float time)
    {
        if (IsRunning)
        {
            StopAndPickResult(coroutineRunner,time);
        }
        else
        {
            StartRunning(coroutineRunner,time);
        }
    }

    public void SetupQTE(float totalTime, float mediumTime, float perfectTime)
    {
        TotalTime = totalTime;

        var midTime = totalTime / 2;
        var minMediumRange = midTime - mediumTime / 2;
        var maxMediumRange = midTime + mediumTime / 2;
        var minPerfectRange = midTime - perfectTime / 2;
        var maxPerfectRange = midTime + perfectTime / 2;

        MediumRange = new FloatRange(minMediumRange, maxMediumRange);
        PerfectRange = new FloatRange(minPerfectRange, maxPerfectRange);

        IsSetup = true;
        OnQTESetup?.Invoke(totalTime, mediumTime, perfectTime);
        //Ui register for event, then it prepare + active the qte panel
    }
    private void StartRunning(MonoBehaviour coroutineRunner,float time)
    {
        _startTime = time;
        //_maxTime = Time.time + _totalTime;

        
        _checkMaxTimeRoutine=coroutineRunner.StartCoroutine(CheckForQTEMaxTime(coroutineRunner));

        IsRunning = true;
        OnStartQte?.Invoke(TotalTime);
    }

    private IEnumerator CheckForQTEMaxTime(MonoBehaviour coroutineRunner)
    {
        yield return new WaitForSeconds(TotalTime);
        StopAndPickResult(coroutineRunner,Time.time);
    }


    private void StopAndPickResult(MonoBehaviour coroutineRunner,float time)
    {
        if (_checkMaxTimeRoutine != null)
        {
            coroutineRunner.StopCoroutine(_checkMaxTimeRoutine);
            _checkMaxTimeRoutine = null;
        }
        
        IsRunning = false;
        
        var timeInQTE = time - _startTime;

        
        if (PerfectRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QteResult.Perfect);
        }
        else if (MediumRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QteResult.Medium);
        }
        else
        {
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

