using UnityEngine;

[CreateAssetMenu(menuName = "SpellEffects/DestroyGameObject")]

public class DestroyGameObjectSpellEffectDefinition : SpellActionDefinition
{
    [SerializeField] private float _lifeTime;
    public override SpellAction GetSpellAction()
    {
        return new DestroyGameObjectSpellEffect(ApplyOnCaster,_lifeTime);
    }
}