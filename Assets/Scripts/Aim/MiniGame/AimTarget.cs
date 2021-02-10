using DG.Tweening;
using UnityEngine;

namespace AimCraftMiniGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AimTarget : PooledMonoBehaviour
    {
        [SerializeField] private Vector3 _startLocalScale;
        [SerializeField] private Vector3 _maxLocalScale;
        [SerializeField] private float _timeToLerpToMaxLocalScale;
        private AimLifePoint _lifePoint;
        private AimScore _score;
        public Vector3 MaxScale => _maxLocalScale;

        
        public void TargetGetHit()
        {
            transform.DOKill();
            ReturnToPool();
            
            if (_score == null) 
                return;
            
            _score.IncreaseScore(1);
            _score = null;
        }

        public void Setup(Vector3 position, AimLifePoint lifePoint, AimScore score)
        {
            transform.position = position;

            _lifePoint = lifePoint;
            _score = score;
            
            transform.localScale = _startLocalScale;
            ExpendThenReduceTargetThenDontGetHit();
        }

        private void ExpendThenReduceTargetThenDontGetHit()
        {
            transform.DOScale(_maxLocalScale, _timeToLerpToMaxLocalScale).SetEase(Ease.Linear)
                .OnComplete(() => transform.DOScale(_startLocalScale, _timeToLerpToMaxLocalScale).SetEase(Ease.Linear)
                    .OnComplete(TargetDontGetHitInTime));
            
        }

        private void TargetDontGetHitInTime()
        {
            ReturnToPool();
            transform.localScale = _startLocalScale;
            if (_lifePoint == null)
                return;
            _lifePoint.ReduceLifePoint(1);
            _lifePoint = null;
        }
    }
}