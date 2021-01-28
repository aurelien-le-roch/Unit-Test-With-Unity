using System;
using UnityEngine;

public class LootableResource : MonoBehaviour
{
    [SerializeField] private ResourceDefinition _definition;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponentInParent<Player>();
        if (player == null)
            return;

        player.ResourceInventory.Add(_definition,2);
        Destroy(gameObject);
    }
}
