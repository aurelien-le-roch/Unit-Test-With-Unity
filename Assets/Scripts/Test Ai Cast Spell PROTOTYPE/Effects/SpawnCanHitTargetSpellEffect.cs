using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpawnCanHitTargetSpellEffect : SpellEffect
{
    private ICanHitTarget _canHitTargetPrefab;
    private readonly List<SpellEffect> _effectsToApplyOnSpawn;
    private readonly List<SpellAction> _actionsToApplyOnSpawn;
    private readonly List<SpellEffect> _effectsToApplyOnHit;
    private readonly List<SpellAction> _actionsToApplyOnHit;

    
    public SpawnCanHitTargetSpellEffect(ICanHitTarget canHitTargetPrefab,List<SpellEffect> effectsToApplyOnSpawn,List<SpellAction> actionsToApplyOnSpawn,List<SpellEffect> effectsToApplyOnHit,List<SpellAction> actionsToApplyOnHit)
    {
        _canHitTargetPrefab = canHitTargetPrefab;
        _effectsToApplyOnSpawn = effectsToApplyOnSpawn;
        _actionsToApplyOnSpawn = actionsToApplyOnSpawn;
        _effectsToApplyOnHit = effectsToApplyOnHit;
        _actionsToApplyOnHit = actionsToApplyOnHit;
    }
    public override void Apply(Transform casterTransform, Vector3 targetPosition)
    {
        var gameObjectSpawned = Object.Instantiate(_canHitTargetPrefab.gameObject, targetPosition, Quaternion.identity);

        var canHitTarget = gameObjectSpawned.GetComponent<ICanHitTarget>();
        canHitTarget.OnHitTarget += ApplyEffectsOnHit;  
        
        foreach (var effect in _effectsToApplyOnSpawn)
        {
            effect.Apply(gameObjectSpawned.transform, targetPosition);
        }

        foreach (var action in _actionsToApplyOnSpawn)
        {
            action.Apply(casterTransform,gameObjectSpawned.transform);
        }
    }


    private void ApplyEffectsOnHit(ICanHitTarget canHitTarget,IMTarget target)
    {
        foreach (var effect in _effectsToApplyOnHit)
        {
            effect.Apply(canHitTarget.gameObject.transform,target.gameObject.transform.position);
        }

        foreach (var action in _actionsToApplyOnHit)
        {
            action.Apply(canHitTarget.gameObject.transform,target.gameObject.transform);
        }
    }

}