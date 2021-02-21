using UnityEngine;

public class SimpleProjectileChargeSpell : ChargedSpell
{
    private SimpleProjectile _simpleProjectile;
    private float _speed;
    private float _lifeTime;
    private Vector3 _targetScale;
    private SimpleProjectile _currentSimpleProjectile;
    
    public SimpleProjectileChargeSpell(BaseSpellSetting baseSpellSetting,SimpleProjectile simpleProjectile, float projectileSpeed, float lifeTime, Vector3 targetScale, float timeToMaxCharge)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _simpleProjectile = simpleProjectile;
        _speed = projectileSpeed;
        _lifeTime = lifeTime;
        _targetScale = targetScale;
        _timeToMaxCharge = timeToMaxCharge;
    }
    
    protected override void StartCharge(Vector3 casterPosition,Vector3 targetPosition)
    {
        base.StartCharge(casterPosition,targetPosition);
        SpawnProjectile(casterPosition,targetPosition);
    }

    protected override void MaxCharge(Vector3 casterPosition, Vector3 targetPosition)
    {
        LaunchProjectile(casterPosition,targetPosition);
        base.MaxCharge(casterPosition, targetPosition);
    }

    protected override void Charging(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.Charging(casterPosition, targetPosition);
        ExpendProjectile();
    }
    protected override void EndCast(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.EndCast(casterPosition,targetPosition);
        LaunchProjectile(casterPosition,targetPosition);
    }
    
    private void SpawnProjectile(Vector3 casterPosition,Vector3 targetPosition)
    {
        _currentSimpleProjectile = Object.Instantiate(_simpleProjectile, casterPosition, Quaternion.identity);
    }
    
    private void LaunchProjectile(Vector3 casterPosition, Vector3 targetPosition)
    {
        var direction = targetPosition - casterPosition;
        direction.Normalize();
            
        _currentSimpleProjectile.Launch(direction,_speed);
        Object.Destroy(_currentSimpleProjectile.gameObject,_lifeTime);
        SpellCastIsOver();
    }
    
    private void ExpendProjectile()
    {
        _currentSimpleProjectile.transform.localScale = 
            Vector3.Lerp(Vector3.one, _targetScale,_chargePercent);
    }
    
}

public interface ISpell
{
    void Tick(float deltaTime);
    void Casting(Vector3 casterPosition, Vector3 targetPosition);
    void NotCasting();
    void StartCast(Vector3 casterPosition, Vector3 targetPosition);
    void EndCast(Vector3 casterPosition, Vector3 targetPosition);
    void SpellCastIsOver();
}

public interface IChargeSpell
{
    void Tick(float deltaTime);
    void Casting(Vector3 casterPosition, Vector3 targetPosition);
    void NotCasting();
    void StartCast(Vector3 casterPosition, Vector3 targetPosition);
    void EndCast(Vector3 casterPosition, Vector3 targetPosition);
    void SpellCastIsOver();

    void StartCharge(Vector3 casterPosition, Vector3 targetPosition);
    void Charging(Vector3 casterPosition, Vector3 targetPosition);
    void MaxCharge(Vector3 casterPosition, Vector3 targetPosition);
}