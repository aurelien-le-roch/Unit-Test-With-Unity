using UnityEngine;

public class SimpleProjectileSpell : Spell
{
    private SimpleProjectile _simpleProjectile;
    private float _speed;
    private float _lifeTime;

    public SimpleProjectileSpell(BaseSpellSetting baseSpellSetting,SimpleProjectile simpleProjectile, float projectileSpeed,float lifeTime)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _simpleProjectile = simpleProjectile;
        _speed = projectileSpeed;
        _lifeTime = lifeTime;
    }
    public override void Use(Vector3 casterPosition,Vector3 targetPosition)
    {
        base.Use(casterPosition,targetPosition);
        var direction = targetPosition - casterPosition;
        direction.Normalize();

        var projectile = Object.Instantiate(_simpleProjectile, casterPosition, Quaternion.identity);
        projectile.Launch(direction, _speed);
        Object.Destroy(projectile.gameObject,_lifeTime);
        SpellCastIsOver();
    }
}


[CreateAssetMenu(menuName = "SpellDefinition/ChargedSimpleProjectile")]
public class ChargedSimpleProjectileSpellDefinition : SpellDefinition
{
    [SerializeField] private SimpleProjectile _simpleProjectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifeTime;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField]private float _timeToMaxCharge;

    

    public override Spell GetSpell()
    {
        return new ChargedSimpleProjectileSpell(BaseSpellSetting,_simpleProjectile,_projectileSpeed,_lifeTime,_targetScale,_timeToMaxCharge);
    }
}

public class ChargedSimpleProjectileSpell : Spell
{
    private SimpleProjectile _simpleProjectile;
    private float _speed;
    private float _lifeTime;
    
    private Vector3 _targetScale;
    private float _timeToMaxCharge;
    
    
    private float _chargeTime;
    private SimpleProjectile _currentSimpleProjectile;
    private float _chargePercent => _chargeTime / _timeToMaxCharge;
    public ChargedSimpleProjectileSpell(BaseSpellSetting baseSpellSetting,SimpleProjectile simpleProjectile, float projectileSpeed, float lifeTime, Vector3 targetScale, float timeToMaxCharge)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _simpleProjectile = simpleProjectile;
        _speed = projectileSpeed;
        _lifeTime = lifeTime;
        _targetScale = targetScale;
        _timeToMaxCharge = timeToMaxCharge;
    }
    
    public override void Use(Vector3 casterPosition, Vector3 targetPosition)
    {
        if (_chargeTime <= 0)
        {
            _currentSimpleProjectile = Object.Instantiate(_simpleProjectile, casterPosition, Quaternion.identity);
            _chargeTime += Time.deltaTime;
            _casterPosition = casterPosition;
            _targetPosition = targetPosition;
        }
        else if(_chargeTime>=_timeToMaxCharge)//force launch when hit max charge
        {
            Launch(casterPosition,targetPosition);
        }
        else
        {
            _chargeTime += Time.deltaTime;
            _currentSimpleProjectile.transform.localScale = 
                Vector3.Lerp(Vector3.one, _targetScale,_chargePercent);
        }
    }

    private void Launch(Vector3 casterPosition, Vector3 targetPosition)
    {
        var direction = targetPosition - casterPosition;
        direction.Normalize();
            
        _currentSimpleProjectile.Launch(direction,_speed);
        Object.Destroy(_currentSimpleProjectile.gameObject,_lifeTime);
        _chargeTime = 0;
        SpellCastIsOver();
    }
    

    private float _timeFromStartUse;
    private Vector3 _casterPosition;
    private Vector3 _targetPosition;
    
}

public class ChargedSpell : Spell
{
    protected float _timeToMaxCharge;
    private float _chargeTime;
    protected float _chargePercent => _chargeTime / _timeToMaxCharge;
    private bool _stopSpellOnMaxCharge;
    protected override void StartCast(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.StartCast(casterPosition,targetPosition);
        if (_chargeTime <= 0)
        {
            _chargeTime += Time.deltaTime;
            StartCharge(casterPosition,targetPosition);
        }
    }

    public override void Casting(Vector3 casterPosition, Vector3 targetPosition)
    {
        base.Casting(casterPosition,targetPosition);
        if(_chargeTime>=_timeToMaxCharge)//force launch when hit max charge
        {
            MaxCharge(casterPosition,targetPosition);
        }
        else
        {
            _chargeTime += Time.deltaTime;
            Charging(casterPosition,targetPosition);
        }
    }

    public override void NotCasting()
    {
        base.NotCasting();
        
    }

    protected virtual void Charging(Vector3 casterPosition, Vector3 targetPosition)
    {
        
    }

    protected virtual void MaxCharge(Vector3 casterPosition, Vector3 targetPosition)
    {
        //doStuff
        
        if (_stopSpellOnMaxCharge)
        {
            SpellCastIsOver();
        }
    }

    protected virtual void StartCharge(Vector3 casterPosition, Vector3 targetPosition)
    {
        
    }

    protected override void SpellCastIsOver()
    {
        base.SpellCastIsOver();
        _chargeTime = 0;
    }
}

[CreateAssetMenu(menuName = "SpellDefinition/SimpleProjectile/ChargeSpell")]
public class SimpleProjectileChargeSpellDefinition : SpellDefinition
{
    [SerializeField] private SimpleProjectile _simpleProjectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifeTime;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField]private float _timeToMaxCharge;
    
    public override Spell GetSpell()
    {
        return new SimpleProjectileChargeSpell(BaseSpellSetting,_simpleProjectile,_projectileSpeed,_lifeTime,_targetScale,_timeToMaxCharge);
    }
}