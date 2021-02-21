using UnityEngine;

public class SimpleProjectileSpell2 : Spell
{
    private SimpleProjectile _simpleProjectile;
    private SimpleProjectile _currentSimpleProjectile;
    private float _speed;
    private float _lifeTime;

    public SimpleProjectileSpell2(BaseSpellSetting baseSpellSetting,SimpleProjectile simpleProjectile, float projectileSpeed,float lifeTime)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _simpleProjectile = simpleProjectile;
        _speed = projectileSpeed;
        _lifeTime = lifeTime;
    }
    
    protected override void StartCast(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.StartCast(casterPosition, targetPosition);
        SpawnProjectile(casterPosition,targetPosition);
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
}