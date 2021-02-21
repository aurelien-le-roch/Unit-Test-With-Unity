using UnityEngine;

public class SpawnObjectAtPosition2 : Spell
{
    private GameObject _gameObjectPrefab;
    private float _lifeTime;

    public SpawnObjectAtPosition2(BaseSpellSetting baseSpellSetting, GameObject prefab, float lifeTime)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _gameObjectPrefab = prefab;
        _lifeTime = lifeTime;
    }

    protected override void StartCast(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.StartCast(casterPosition, targetPosition);
        var spawnPosition = CalculateSpawnPosition(casterPosition, targetPosition);
        Spawn(spawnPosition);
        SpellCastIsOver();
    }
    
    private void Spawn(Vector3 position)
    {
        var gameObject = Object.Instantiate(_gameObjectPrefab, position, Quaternion.identity);
        Object.Destroy(gameObject, _lifeTime);
    }
    
    private Vector3 CalculateSpawnPosition(Vector3 casterPosition, Vector3 targetPosition)
    {
        casterPosition=new Vector3(casterPosition.x,casterPosition.y,0);
        targetPosition=new Vector3(targetPosition.x,targetPosition.y,0);
        var distance = (targetPosition - casterPosition).magnitude;

        if (distance < MaxRange)
        {
            return targetPosition;
        }

        var direction = targetPosition - casterPosition;
        direction.Normalize();
        var maxPosition = direction * MaxRange;
        return casterPosition + maxPosition;
    }
}

