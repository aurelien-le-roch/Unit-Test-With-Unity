using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft/RaritiesValue")]
public class RaritiesValuesDefinition : ScriptableObject
{
    [SerializeField] private List<ObjectRarityAndValue> _objectRarityAndValues;

    public int GetValue(ObjectRarity objectRarity)
    {
        foreach (var objectRarityAndValue in _objectRarityAndValues)
        {
            if (objectRarityAndValue.ObjectRarity == objectRarity)
                return objectRarityAndValue.Value;
        }

        return 0;
    }
}