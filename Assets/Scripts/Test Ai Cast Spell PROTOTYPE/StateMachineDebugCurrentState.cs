using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StateMachineDebugCurrentState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _historyCount=5;

    private IChangeState _changeState;

    private Queue<Type> _statesQueue=new Queue<Type>();
    private void Start()
    {
        _changeState = GetComponentInParent<IChangeState>();
        _changeState.OnCurrentStateTypeChange += Refresh;
    }

    private void Refresh(Type stateType)
    {
        _statesQueue.Enqueue(stateType);
        if (_statesQueue.Count >= _historyCount)
            _statesQueue.Dequeue();

        var text = new StringBuilder();
        foreach (var type in _statesQueue)
        {
            text.Append($"{type.Name}\n");
        }
        _text.text = text.ToString();
    }

    private void OnDestroy()
    {
        _changeState.OnCurrentStateTypeChange -= Refresh;

    }
}