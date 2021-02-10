using System;
using UnityEngine;

namespace AimCraftMiniGame
{
    public class AimScore : IAimScore
    {
        public int CurrentScore { get; private set; }
        public int MaxScore { get; private set; }
        public event Action<int> OnCurrentScoreChange;

        public void SetMaxScore(int maxScore)
        {
            MaxScore = maxScore;
        }
        public void IncreaseScore(int amount)
        {
            amount = Mathf.Abs(amount);
            CurrentScore += amount;
            OnCurrentScoreChange?.Invoke(CurrentScore);
        }

        public void Reset()
        {
            CurrentScore=0;
            OnCurrentScoreChange?.Invoke(CurrentScore);
        }
    }

    public interface IAimScore
    {
        int CurrentScore { get; }
        int MaxScore { get; }
        event Action<int> OnCurrentScoreChange;
    }
}