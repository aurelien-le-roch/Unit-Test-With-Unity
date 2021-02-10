using System;
using UnityEngine;

namespace AimCraftMiniGame
{
    public class AimLifePoint : IAimLifePoint
    {
        public event Action<int> OnLifePointChange;
        public event Action OnLifePointHit0;
        public int CurrentLifePoint { get; private set; }
        private bool _onLifePointHit0WasCall;
        private readonly int _startLifePoint;
        
        public AimLifePoint(int startLifePoint)
        {
            CurrentLifePoint = startLifePoint;
            _startLifePoint = startLifePoint;
        }

        public void ReduceLifePoint(int amount)
        {
            amount = Mathf.Abs(amount);
            
            CurrentLifePoint-=amount;
            if(CurrentLifePoint>=0)
                OnLifePointChange?.Invoke(CurrentLifePoint);
            if (CurrentLifePoint <= 0 && _onLifePointHit0WasCall == false)
            {
                OnLifePointHit0?.Invoke();
                _onLifePointHit0WasCall = true;
            }
        }

        public void ResetCurrentHealth()
        {
            CurrentLifePoint = _startLifePoint;
            OnLifePointChange?.Invoke(CurrentLifePoint);
        }
    }

    public interface IAimLifePoint
    { 
        event Action<int> OnLifePointChange;
        int CurrentLifePoint { get; }
    }
}