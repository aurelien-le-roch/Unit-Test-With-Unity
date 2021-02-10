using System;
using UnityEngine;

namespace AimCraftMiniGame
{
    [Serializable]
    public struct AimSpawnerSettings
    {
        public AnimationCurve TargetsBySecondCurve;
        public float StartTargetsBySecond;
        public Transform TopBorder;
        public Transform RightBorder;
        public Transform BotBorder;
        public Transform LeftBorder;
    }
}