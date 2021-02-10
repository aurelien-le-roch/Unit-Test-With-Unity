using UnityEngine;

namespace AimCraftMiniGame
{
    public class AimTargetSpawner
    {
        private readonly AimTarget _targetPrefab;
        private readonly AimLifePoint _lifePoint;
        private readonly AimScore _score;
        private AnimationCurve _targetBySecondsCurve;
        private Vector3 TopBorderPosition;
        private Vector3 RightBorderPosition;
        private Vector3 BotBorderPosition;
        private Vector3 LeftBorderPosition;

        private float _curveValue;
        private float _targetsBySecond;
        
        private float _spawnTimer;
        private float _spawnTime;
        private float _startTargetBySeconds;

        public AimTargetSpawner(AimSpawnerSettings spawnerSettings, AimTarget targetPrefab, AimLifePoint lifePoint,
            AimScore score)
        {
            _targetPrefab = targetPrefab;
            _lifePoint = lifePoint;
            _score = score;
            _targetBySecondsCurve = spawnerSettings.TargetsBySecondCurve;
            _startTargetBySeconds = spawnerSettings.StartTargetsBySecond;
            TopBorderPosition = spawnerSettings.TopBorder.position;
            RightBorderPosition = spawnerSettings.RightBorder.position;
            BotBorderPosition = spawnerSettings.BotBorder.position;
            LeftBorderPosition = spawnerSettings.LeftBorder.position;
        }

        public void Tick(float deltaTime)
        {
            _curveValue = _targetBySecondsCurve.Evaluate(_targetsBySecond);
            _targetsBySecond += _curveValue * deltaTime;
            _spawnTime = 1 / _targetsBySecond;

            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnTime)
            {
                Spawn();
                _spawnTimer = 0;
            }
        }

        private void Spawn()
        {
            var aimTarget = _targetPrefab.Get<AimTarget>(false);
            aimTarget.Setup(RandomSpawnPosition(),_lifePoint,_score);
            aimTarget.gameObject.SetActive(true);
        }
        
        private Vector3 RandomSpawnPosition()
        {
            var maxScale = _targetPrefab.MaxScale;

            var xMinPosition = LeftBorderPosition.x + (maxScale.x / 2);
            var xMaxPosition = RightBorderPosition.x - (maxScale.x / 2);

            var xRandomPosition = Random.Range(xMinPosition, xMaxPosition);

            var yMinPosition = BotBorderPosition.y + (maxScale.y / 2);
            var yMaxPosition = TopBorderPosition.y - (maxScale.y / 2);

            var yRandomPosition = Random.Range(yMinPosition, yMaxPosition);

            return new Vector3(xRandomPosition, yRandomPosition, 1);
        }

        public void ResetSpawnTimerAndTargetsBySecond()
        {
            _targetsBySecond = _startTargetBySeconds;
            _spawnTimer = 0;
        }
    }
}