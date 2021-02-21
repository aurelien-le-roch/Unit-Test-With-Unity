using UnityEngine;

public class ChargedSpawnObjectAtPositionSpell : Spell
{
    private GameObject _prefab;
    private float _lifeTime;
    
    private Vector3 _targetScale;
    private float _timeToMaxCharge;
    
    
    private float _chargeTime;
    private GameObject _currentObject;
    private float _chargePercent => _chargeTime / _timeToMaxCharge;
    public ChargedSpawnObjectAtPositionSpell(BaseSpellSetting baseSpellSetting,GameObject prefab,  float lifeTime, Vector3 targetScale, float timeToMaxCharge)
    {
        SetBaseSpellSetting(baseSpellSetting);
        _prefab = prefab;
        _lifeTime = lifeTime;
        _targetScale = targetScale;
        _timeToMaxCharge = timeToMaxCharge;
    }
    
    public override void Use(Vector3 casterPosition, Vector3 targetPosition)
    {
        if (_chargeTime <= 0)//at first use call
        {
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
                Spawn(casterPosition+maxPosition);
            }
            
            _chargeTime += Time.deltaTime;
        }
        else if(_chargeTime>=_timeToMaxCharge)// when hit max charge
        {
            Object.Destroy(_currentObject.gameObject,_lifeTime);
            _chargeTime = 0;
            SpellCastIsOver();
        }
        else
        {
            _chargeTime += Time.deltaTime;
            _currentObject.transform.localScale = 
                Vector3.Lerp(Vector3.one, _targetScale,_chargePercent);
        }
    }
    
    private void Spawn(Vector3 position)
    {
        _currentObject = Object.Instantiate(_prefab, position, Quaternion.identity);
    }
}

[CreateAssetMenu(menuName = "SpellDefinition/ChargedSpawnObjectAtPosition")]
public class ChargedSpawnObjectAtPositionSpellDefinition : SpellDefinition
{
    [SerializeField] private GameObject _simpleProjectile;
    [SerializeField] private float _lifeTime;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField]private float _timeToMaxCharge;
    
    public override Spell GetSpell()
    {
        return new ChargedSpawnObjectAtPositionSpell(BaseSpellSetting,_simpleProjectile,_lifeTime,_targetScale,_timeToMaxCharge);
    }
}

[CreateAssetMenu(menuName = "SpellDefinition/ChargedSpawnObjectAtPosition2")]
public class ChargedSpawnObjectAtPositionSpellDefinition2 : SpellDefinition
{
    [SerializeField] private GameObject _simpleProjectile;
    [SerializeField] private float _lifeTime;

    [SerializeField] private Vector3 _targetScale;
    [SerializeField]private float _timeToMaxCharge;
    
    public override Spell GetSpell()
    {
        return new ChargedSpawnObjectAtPositionSpell2(BaseSpellSetting,_simpleProjectile,_lifeTime,_targetScale,_timeToMaxCharge);
    }
}

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