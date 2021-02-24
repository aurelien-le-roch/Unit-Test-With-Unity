using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpellEffects/SpawnObject")]
public class SpawnGameObjectSpellEffectDefinition : SpellEffectDefinition
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private List<SpellEffectDefinition> _effectDefinitionsToApplyWhenSpawn;

    
    public override SpellEffect GetSpellEffect()
    {
        return new SpawnGameObjectSpellEffect(_gameObjectPrefab,GetSpellEffects(_effectDefinitionsToApplyWhenSpawn));
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
}