using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [SerializeField] private ScriptableObjectInInventories _definition;
    [SerializeField] private int _amount = 1;
    public ICanBeAddedToInventories CanBeAddedToInventories { get; set; }
    public int Amount => _amount;

    private void Awake()
    {
        CanBeAddedToInventories = _definition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var iHaveInventories = other.GetComponentInParent<IHaveInventories>();
        if (iHaveInventories == null)
            return;

        CanBeAddedToInventories.AddToInventory(iHaveInventories, Amount);
        Destroy(gameObject);
    }
}