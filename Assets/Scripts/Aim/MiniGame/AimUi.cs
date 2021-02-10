using System;
using UnityEngine;

namespace AimCraftMiniGame
{
    [RequireComponent(typeof(AimStateMachine))]
    public class AimUi : MonoBehaviour
    {
        [SerializeField] private AimUiBeginState uiBeginState;
        [SerializeField] private AimUiPlayState uiPlayState;
        
        private AimStateMachine _stateMachine;
        private void Start()
        {
            _stateMachine = GetComponent<AimStateMachine>();
            _stateMachine.OnStateChange += HandleStateChange;
        }
        
        private void HandleStateChange(IState state)
        {
            uiBeginState.gameObject.SetActive(state is AimBegin);
            uiPlayState.gameObject.SetActive(state is AimPlay);
        }
    }
}