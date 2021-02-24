using System;
using UnityEngine;

public abstract class Spell
{
    //faire un event on maxCharge
    //ou un bool
    public bool CanUse => _cooldownTimer<=0;
    public float MinRange => _baseSpellSetting.MinRange;
    public float MaxRange => _baseSpellSetting.MaxRange;

    public event Action OnSpellCastOver;
    
    private BaseSpellSetting _baseSpellSetting;

    private float _cooldownTimer;

    private Vector3 casterPosition;
    private Vector3 targetPosition;
    
    public void Tick(float deltaTime)
    {
        if(_cooldownTimer<=0)
            _cooldownTimer -= deltaTime;
    }
    private bool _casting;
    
    public virtual void Casting(Vector3 casterPosition, Vector3 targetPosition)
    {
        this.casterPosition = casterPosition;
        this.targetPosition = targetPosition;
        if (_casting == false)
        {
            SetCasting(true);
            StartCast(casterPosition,targetPosition);
        }
    }
    public virtual void NotCasting()
    {
        if (_casting)
        {
            SetCasting(false);
            EndCast(casterPosition,targetPosition);
        }
    }
    protected virtual void StartCast(Vector3 casterPosition, Vector3 targetPosition)
    {
    }
    protected virtual void EndCast(Vector3 casterPosition, Vector3 targetPosition)
    {
    }

    
    public virtual void Use(Vector3 casterPosition,Vector3 targetPosition)
    {
    }

    protected virtual void SpellCastIsOver()
    {
        _cooldownTimer =_baseSpellSetting.Cooldown;
        SetCasting(false);
        OnSpellCastOver?.Invoke();
    }

    protected void SetBaseSpellSetting(BaseSpellSetting baseSpellSetting)
    {
        _baseSpellSetting = baseSpellSetting;
    }

    private void SetCasting(bool value)
    {
        _casting = value;
    }
}