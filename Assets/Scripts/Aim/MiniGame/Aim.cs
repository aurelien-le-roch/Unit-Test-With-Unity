using UnityEngine;

namespace AimCraftMiniGame
{
    public class Aim 
    {
        public AimLifePoint LifePoint;
        public AimScore Score;

        private AimTapController _tapController;
        private AimTargetSpawner _targetSpawner;


        public Aim(Camera camera, AimSpawnerSettings settings, AimTarget targetPrefab)
        {
            LifePoint = new AimLifePoint(3);
            Score = new AimScore();
            _tapController = new AimTapController(camera);
            _targetSpawner = new AimTargetSpawner(settings, targetPrefab, LifePoint, Score);
        }
        

        public void Tick()
        {
            _tapController.Tick();
            _targetSpawner.Tick(Time.deltaTime);
        }
        
        public void Reset()
        {
            LifePoint.ResetCurrentHealth();
            Score.Reset();
            _targetSpawner.ResetSpawnTimerAndTargetsBySecond();
        }
    }
}