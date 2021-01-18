using System;
using System.Collections;
using UnityEngine;

public class QTEMiningTest : MonoBehaviour
{
    [SerializeField] private float _totalTime;
    [SerializeField] private float _mediumTime;
    [SerializeField] private float _perfectTime;
    private bool _QTERunning;

    public event Action<float, float, float> OnQTEStart;
    public event Action<QTEResult> OnQTEEnd;

    private float _QTEstartTime;
    private float _QTEMaxTime;
    private FloatRange _mediumRange;
    private FloatRange _perfectRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_QTERunning == false)
                StartQTE(_totalTime, _mediumTime, _perfectTime);
            else
                StopQTE();
        }


        if (_QTERunning && Time.time >= _QTEMaxTime)
            StopQTE();
    }

    private void StartQTE(float totalTime, float mediumTime, float perfectTime)
    {
        _QTERunning = true;
        _QTEstartTime = Time.time;
        _QTEMaxTime = Time.time + totalTime;

        var midTime = totalTime / 2;

        var minMediumRange = midTime - mediumTime / 2;
        var maxMediumRange = midTime + mediumTime / 2;

        var minPerfectRange = midTime - perfectTime / 2;
        var maxPerfectRange = midTime + perfectTime / 2;

        _mediumRange = new FloatRange(minMediumRange, maxMediumRange);
        _perfectRange = new FloatRange(minPerfectRange, maxPerfectRange);

        OnQTEStart?.Invoke(totalTime, mediumTime, perfectTime);
    }

    private void StopQTE()
    {
        _QTERunning = false;
        var timeInQTE = Time.time - _QTEstartTime;

        if (_perfectRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QTEResult.Perfect);
        }
        else if (_mediumRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QTEResult.Medium);
        }
        else
        {
            OnQTEEnd?.Invoke(QTEResult.Fail);
        }
    }
}

public class QTEMining
{
    private bool _isRunning;
    private float _startTime;
    private float _maxTime;
    private float _totalTime;
    private FloatRange _mediumRange;
    private FloatRange _perfectRange;

    private Coroutine _checkMaxTimeRoutine;
    public event Action<float, float, float> OnQTESetup;
    public event Action<float> OnStartQte;
    public event Action<QTEResult> OnQTEEnd;
    public bool IsSetup { get; private set; }
    
    

    public void Use(MonoBehaviour coroutineRunner)
    {
        if (_isRunning)
        {
            StopAndPickResult(coroutineRunner);
            Reset();
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
        _maxTime = Time.time + _totalTime;

        
        _checkMaxTimeRoutine=coroutineRunner.StartCoroutine(CheckForQTEMaxTime(coroutineRunner));

        _isRunning = true;
        OnStartQte?.Invoke(_totalTime);
        
    }

    private IEnumerator CheckForQTEMaxTime(MonoBehaviour coroutineRunner)
    {
        yield return new WaitForSeconds(_maxTime);
        StopAndPickResult(coroutineRunner);
    }
    private void Reset()
    {
        
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
            OnQTEEnd?.Invoke(QTEResult.Perfect);
        }
        else if (_mediumRange.InRange(timeInQTE))
        {
            OnQTEEnd?.Invoke(QTEResult.Medium);
        }
        else
        {
            OnQTEEnd?.Invoke(QTEResult.Fail);
        }
    }

    public void JobOver()
    {
    }
}

public struct FloatRange
{
    private readonly float _min;
    private readonly float _max;

    public FloatRange(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public bool InRange(float value) => value >= _min && value <= _max;
}

public enum QTEResult
{
    Fail,
    Medium,
    Perfect,
}