using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellEffects/SpawnCanHitTarget")]
public class SpawnCanHitTargetSpellEffectDefinition : SpellEffectDefinition
{
    [SerializeField] private GameObject _canHitTargetPrefabAsGO;
    [SerializeField] private List<SpellEffectDefinition> _effectDefinitionsToApplyWhenSpawn;
    [SerializeField] private List<SpellActionDefinition> _actionDefinitionsToApplyWhenSpawn;
    
    [SerializeField] private List<SpellEffectDefinition> _effectDefinitionsToApplyWhenHitTarget;
    [SerializeField] private List<SpellActionDefinition> _actionDefinitionsToApplyWhenHitTarget;

    private ICanHitTarget _canHitTargetPrefab;
    public override SpellEffect GetSpellEffect()
    {
        var effectsOnSpawn = GetSpellEffects(_effectDefinitionsToApplyWhenSpawn);
        var actionsOnSpawn = GetSpellActions(_actionDefinitionsToApplyWhenSpawn);

        var effectsOnHit = GetSpellEffects(_effectDefinitionsToApplyWhenHitTarget);
        var actionsOnHit = GetSpellActions(_actionDefinitionsToApplyWhenHitTarget);
        return new SpawnCanHitTargetSpellEffect(_canHitTargetPrefab,effectsOnSpawn,actionsOnSpawn,effectsOnHit,actionsOnHit);
    }

    private void OnValidate()
    {
        var canHitTarget = _canHitTargetPrefabAsGO.GetComponent<ICanHitTarget>();
        if (canHitTarget != null)
        {
            _canHitTargetPrefab = canHitTarget;
        }
        else
        {
            _canHitTargetPrefabAsGO = null;
            _canHitTargetPrefab = null;
        }
    }
    
    private List<SpellEffect> GetSpellEffects(List<SpellEffectDefinition> spellEffectDefinitions)
    {
        var list = new List<SpellEffect>();
        foreach (var spellEffectDefinition in spellEffectDefinitions)
        {
            list.Add(spellEffectDefinition.GetSpellEffect());
        }

        return list;
    }
    private List<SpellAction> GetSpellActions(List<SpellActionDefinition> spellEffectDefinitions)
    {
        var list = new List<SpellAction>();
        foreach (var spellEffectDefinition in spellEffectDefinitions)
        {
            list.Add(spellEffectDefinition.GetSpellAction());
        }

        return list;
    }
}