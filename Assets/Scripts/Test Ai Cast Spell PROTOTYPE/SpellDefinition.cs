using UnityEngine;

public abstract class SpellDefinition : ScriptableObject
{
    [SerializeField] private BaseSpellSetting _baseSpellSetting;
    protected BaseSpellSetting BaseSpellSetting => _baseSpellSetting;
    public abstract Spell GetSpell();
}