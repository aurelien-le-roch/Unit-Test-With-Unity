using UnityEngine;

[CreateAssetMenu(menuName = "Craft/CraftInfo")]
public class CraftInfo : ScriptableObject
{
    [SerializeField] private RaritiesValuesDefinition _xpNeeded;
    [SerializeField] private RaritiesValuesDefinition _xpToGive;
    [SerializeField] private RaritiesValuesDefinition _miniGameMaxScore;
    [SerializeField] private RaritiesValuesDefinition _fragments;

    public int GetXpNeeded(ObjectRarity rarity)
    {
        return _xpNeeded.GetValue(rarity);
    }
    public int GetXpToGive(ObjectRarity rarity)
    {
        return _xpToGive.GetValue(rarity);
    }
    public int GetMiniGameMaxScore(ObjectRarity rarity)
    {
        return _miniGameMaxScore.GetValue(rarity);
    }
    public int GetFragments(ObjectRarity rarity)
    {
        return _fragments.GetValue(rarity);
    }

    public ObjectRarity GetRarityFromMiniGameScore(int score)
    {
        if(score>=GetMiniGameMaxScore(ObjectRarity.Orange))
            return ObjectRarity.Orange;
        
        if(score>=GetMiniGameMaxScore(ObjectRarity.Purple))
            return ObjectRarity.Purple;
        if(score>=GetMiniGameMaxScore(ObjectRarity.Blue))
            return ObjectRarity.Blue;
        
        if(score>=GetMiniGameMaxScore(ObjectRarity.Green))
            return ObjectRarity.Green;
        
        return ObjectRarity.White;
    }
}