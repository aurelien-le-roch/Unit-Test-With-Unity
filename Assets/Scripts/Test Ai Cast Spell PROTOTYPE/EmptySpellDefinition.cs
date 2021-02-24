using UnityEngine;

[CreateAssetMenu(menuName = "SpellDefinition/EmptySpell")]
public class EmptySpellDefinition : SpellDefinition
{
    public override Spell GetSpell()
    {
        return new EmptySpell();
    }
}