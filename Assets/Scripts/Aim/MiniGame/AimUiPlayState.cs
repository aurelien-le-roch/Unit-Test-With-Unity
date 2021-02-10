using TMPro;
using UnityEngine;

namespace AimCraftMiniGame
{
    public class AimUiPlayState : MonoBehaviour
    {
        [SerializeField] private AimStateMachine _stateMachine;
        [SerializeField] private TextMeshProUGUI _textForTargetHit;
        [SerializeField] private TextMeshProUGUI _textForLifePoint;
        
        private void Start()
        {
            _stateMachine.Score.OnCurrentScoreChange += ResfreshScoreUi;
            _stateMachine.LifePoint.OnLifePointChange += RefreshLifePointUi;
        }

        private void RefreshLifePointUi(int currentLifePoint)
        {
            _textForLifePoint.text = currentLifePoint.ToString();
        }

        private void OnEnable()
        {
            _textForTargetHit.text = $"{_stateMachine.Score.CurrentScore} / {_stateMachine.Score.MaxScore}";
        }

        private void ResfreshScoreUi(int currentScore)
        {
            _textForTargetHit.text = $"{currentScore} / {_stateMachine.Score.MaxScore}";
        }
    }
}