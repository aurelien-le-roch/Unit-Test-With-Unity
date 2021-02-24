using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "SpellDefinition/SimpleProjectile2")]
public class SimpleProjectileSpellDefinition2 : SpellDefinition
{
    [SerializeField] private SimpleProjectile _simpleProjectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifeTime;

    public override Spell GetSpell()
    {
        return new SimpleProjectileSpell2(BaseSpellSetting,_simpleProjectile,_projectileSpeed,_lifeTime);
    }
}