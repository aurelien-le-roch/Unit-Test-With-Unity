using TMPro;
using UnityEngine;

public class AimUiBeginState : MonoBehaviour
{
    [SerializeField] private AimStateMachine _aimStateMachine;
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        var timerRounded = Mathf.Round(_aimStateMachine.BeginTimer * 100f) / 100f;

        _text.text = timerRounded.ToString();
    }
}
