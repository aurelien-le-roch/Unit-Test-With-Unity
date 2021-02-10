using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AimButtonToStartFreeTraining : MonoBehaviour
{
    [SerializeField] private AimStateMachine _aimStateMachine;
    [SerializeField] private int _maxAmount;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonPressed);
    }

    private void HandleButtonPressed()
    {
        _aimStateMachine.TryToBeginMiniGame( _maxAmount);
    }
}