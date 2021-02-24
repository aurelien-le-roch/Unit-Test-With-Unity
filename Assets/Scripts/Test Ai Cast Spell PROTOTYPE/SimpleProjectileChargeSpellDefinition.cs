using UnityEngine;

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