using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpells
{
    private readonly Transform _casterTransform;
    public event Action OnSpellBecomeNotAvailable;
    public event Action OnSpellBecomeAvailable;
    public List<Spell> Spells { get; set; }
    private Vector3 CastPosition => _casterTransform.position;

    public CharacterSpells(Transform casterTransform)
    {
        _casterTransform = casterTransform;
        Spells=new List<Spell>();
    }
    public void AddSpell(Spell spell)
    {
        Spells.Add(spell);
        spell.OnSpellCastOver += () => OnSpellBecomeNotAvailable?.Invoke();
        OnSpellBecomeAvailable?.Invoke();
    }
    
    public bool InRangeToHit(Spell spell,ITargetPosition targetPosition)
    {
        if (Spells.Contains(spell) == false)
            return false;
        var distanceFromCastToTarget = Vector2.Distance(CastPosition, targetPosition.TargetPosition);
        return distanceFromCastToTarget >= spell.MinRange && distanceFromCastToTarget <= spell.MaxRange;
    }

    public void UseSpell(Spell spell, Vector3 targetPosition)
    {
//        if(Spells.Contains(spell) && spell.CanUse)
//            spell.Use(CastPosition,targetPosition);
        if(Spells.Contains(spell) && spell.CanUse)
            spell.Casting(CastPosition,targetPosition);
    }
    public void Tick(float deltaTime)
    {
        foreach (var spell in Spells)
        {
            var canUseBeforeTick = spell.CanUse;
            spell.Tick(deltaTime);
            if(canUseBeforeTick==false && spell.CanUse)
                OnSpellBecomeAvailable?.Invoke();
        }
    }

    public void NotUseSpell(Spell spell)
    {
        if(Spells.Contains(spell) && spell.CanUse) 
            spell.NotCasting();
    }
}
