using UnityEngine;

public abstract class SpellEffectDefinition : ScriptableObject
{
    public abstract SpellEffect GetSpellEffect();
}

public abstract class SpellActionDefinition : ScriptableObject
{
    [SerializeField] private bool _applyOnCaster;

    public bool ApplyOnCaster => _applyOnCaster;

    public abstract SpellAction GetSpellAction();
}