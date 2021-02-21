using UnityEngine;

[CreateAssetMenu(menuName = "SpellDefinition/SpawnObjectAtPosition")]
public class SpawnObjectAtPositionSpellDefinition : SpellDefinition
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private float _lifeTime;

    public override Spell GetSpell()
    {
        return new SpawnObjectAtPosition(BaseSpellSetting,_gameObjectPrefab,_lifeTime);
    }
}

[CreateAssetMenu(menuName = "SpellDefinition/SpawnObjectAtPosition2")]
public class SpawnObjectAtPositionSpellDefinition2 : SpellDefinition
{
    [SerializeField] private GameObject _gameObjectPrefab;
    [SerializeField] private float _lifeTime;

    public override Spell GetSpell()
    {
        return new SpawnObjectAtPosition2(BaseSpellSetting,_gameObjectPrefab,_lifeTime);
    }
}