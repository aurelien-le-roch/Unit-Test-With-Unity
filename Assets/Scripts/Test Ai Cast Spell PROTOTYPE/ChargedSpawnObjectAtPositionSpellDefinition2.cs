using UnityEngine;

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