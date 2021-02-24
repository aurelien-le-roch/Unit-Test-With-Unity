using UnityEngine;

[CreateAssetMenu(menuName = "SpellEffects/LaunchGameObject")]
public class LaunchGameObjectSpellEffectDefinition : SpellEffectDefinition
{
    [SerializeField] private float _speed;
    
    public override SpellEffect GetSpellEffect()
    {
        return new LaunchGameObjectSpellEffect(_speed);
    }
}