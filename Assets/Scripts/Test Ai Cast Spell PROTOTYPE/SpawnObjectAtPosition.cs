using UnityEngine;

public class SpawnObjectAtPosition : Spell
{
    private GameObject _gameObjectPrefab;
    private float _lifeTime;

    public SpawnObjectAtPosition(BaseSpellSetting baseSpellSetting, GameObject prefab, float lifeTime)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _gameObjectPrefab = prefab;
        _lifeTime = lifeTime;
    }

    public override void Use(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.Use(casterPosition, targetPosition);

        casterPosition = new Vector3(casterPosition.x, casterPosition.y, 0);
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, 0);
        var distance = (targetPosition - casterPosition).magnitude;

        if (distance < MaxRange)
        {
            Spawn(targetPosition);
        }
        else
        {
            var direction = targetPosition - casterPosition;
            direction.Normalize();
            var maxPosition = direction * MaxRange;
            Spawn(casterPosition + maxPosition);
        }

        SpellCastIsOver();
    }

    private void Spawn(Vector3 position)
    {
        var gameObject = Object.Instantiate(_gameObjectPrefab, position, Quaternion.identity);
        Object.Destroy(gameObject, _lifeTime);
    }
}