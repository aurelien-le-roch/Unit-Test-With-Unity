using UnityEngine;

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