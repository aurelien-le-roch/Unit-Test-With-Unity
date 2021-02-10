using TMPro;
using UnityEngine;

public class UiDebugStateMachine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private AimStateMachine _aimStateMachine;

    private void Start()
    {
        _aimStateMachine.OnStateChange += Refresh;
    }

    private void Refresh(IState state)
    {
        _text.text = state.ToString();
    }
}