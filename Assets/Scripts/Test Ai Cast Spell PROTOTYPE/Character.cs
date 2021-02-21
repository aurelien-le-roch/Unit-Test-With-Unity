using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour, ICharacterWithSpells
{
    public CharacterSpells CharacterSpells { get; private set; }

    private void Awake()
    {
        CharacterSpells=new CharacterSpells(transform);
    }

    private void Update()
    {
        CharacterSpells.Tick(Time.deltaTime);
    }
}