using UnityEngine;

[CreateAssetMenu(menuName = "SpellDefinition/SimpleProjectile")]
public class SimpleProjectileSpellDefinition : SpellDefinition
{
    [SerializeField] private SimpleProjectile _simpleProjectile;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifeTime;

    public override Spell GetSpell()
    {
        return new SimpleProjectileSpell(BaseSpellSetting,_simpleProjectile,_projectileSpeed,_lifeTime);
    }
}

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