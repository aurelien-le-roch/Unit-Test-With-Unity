using System.Collections.Generic;
using UnityEngine;

public class CharacterSpellBuilder : MonoBehaviour
{
    [SerializeField] private List<SpellDefinition> _spellDefinitions;

    private void Start()
    {
        var characterWithSpells = GetComponent<ICharacterWithSpells>().CharacterSpells;
        foreach (var spellDefinition in _spellDefinitions)
        {
            characterWithSpells.AddSpell(spellDefinition.GetSpell());
        }
    }
}