using UnityEngine;

public class DestroyGameObjectSpellEffect : SpellAction
{
    private readonly float _lifeTime;

    public DestroyGameObjectSpellEffect(bool applyOnCaster, float lifeTime)
    {
        _applyOnCaster = applyOnCaster;
        _lifeTime = lifeTime;
    }
    public override void Apply(Transform casterTransform, Transform targetTransform)
    {
        var objectToDestroy = _applyOnCaster ? casterTransform.gameObject : targetTransform.gameObject;
        Object.Destroy(objectToDestroy, _lifeTime);
    }
}

