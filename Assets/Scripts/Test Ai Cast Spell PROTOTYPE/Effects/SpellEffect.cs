using UnityEngine;

public abstract class SpellEffect : SpellAction
{
    public abstract void Apply(Transform casterTransform, Vector3 targetPosition);
    public override void Apply(Transform caster, Transform target)
    {
        Apply(caster,target.position);
    }
}

public abstract class SpellAction
{
    protected bool _applyOnCaster;
    public abstract void Apply(Transform caster, Transform target);
}