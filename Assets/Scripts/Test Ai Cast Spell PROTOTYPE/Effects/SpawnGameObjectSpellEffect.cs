using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class SpawnGameObjectSpellEffect : SpellEffect
{
    private readonly List<SpellEffect> _effectsToApplyOnSpawn;
    private readonly GameObject _gameObjectPrefab;
    
    public SpawnGameObjectSpellEffect(GameObject gameObjectPrefab,List<SpellEffect> effectsToApplyOnSpawn)
    {
        _gameObjectPrefab = gameObjectPrefab;
        _effectsToApplyOnSpawn = effectsToApplyOnSpawn;
    }
    public override void Apply(Transform casterTransform, Vector3 targetPosition)
    {
        var gameObjectSpawned = Object.Instantiate(_gameObjectPrefab, targetPosition, Quaternion.identity);
        foreach (var effect in _effectsToApplyOnSpawn)
        {
            effect.Apply(gameObjectSpawned.transform, targetPosition);
        }
    }
}