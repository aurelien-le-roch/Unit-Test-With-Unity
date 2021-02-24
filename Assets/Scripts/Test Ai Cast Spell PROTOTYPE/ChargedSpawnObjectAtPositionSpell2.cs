using UnityEngine;

public class ChargedSpawnObjectAtPositionSpell2 : ChargedSpell
{
    private GameObject _simpleProjectile;
    private float _lifeTime;
    
    private Vector3 _targetScale;
    private GameObject _currentSimpleProjectile;

    
    public ChargedSpawnObjectAtPositionSpell2(BaseSpellSetting baseSpellSetting,GameObject prefab,  float lifeTime, Vector3 targetScale, float timeToMaxCharge)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _simpleProjectile = prefab;
        _lifeTime = lifeTime;
        _targetScale = targetScale;
        _timeToMaxCharge = timeToMaxCharge;
    }
    
    protected override void StartCharge(Vector3 casterPosition,Vector3 targetPosition)
    {
        base.StartCharge(casterPosition,targetPosition);
        var spawnPosition = CalculateSpawnPosition(casterPosition, targetPosition);
        Spawn(spawnPosition);
    }
    
    protected override void Charging(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.Charging(casterPosition, targetPosition);
        ExpendProjectile();
    }

    protected override void MaxCharge(Vector3 casterPosition, Vector3 targetPosition)
    {
        SpellCastIsOver();
        base.MaxCharge(casterPosition, targetPosition);
    }

    protected override void EndCast(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.EndCast(casterPosition, targetPosition);
        SpellCastIsOver();
    }

    private void Spawn(Vector3 position)
    {
        _currentSimpleProjectile = Object.Instantiate(_simpleProjectile, position, Quaternion.identity);
    }
    
    private void ExpendProjectile()
    {
        _currentSimpleProjectile.transform.localScale = 
            Vector3.Lerp(Vector3.one, _targetScale,_chargePercent);
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

    protected override void SpellCastIsOver()
    {
        Object.Destroy(_currentSimpleProjectile);
        base.SpellCastIsOver();
    }
}