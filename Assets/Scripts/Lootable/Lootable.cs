using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [SerializeField] private ScriptableObject _definition;

    public ICanBeAddedToInventories CanBeAddedToInventories { get; set; }
    
    private void Awake()
    {
        if (_definition is ICanBeAddedToInventories canBeAddedToPlayer)
        {
            CanBeAddedToInventories = canBeAddedToPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_definition is ICanBeAddedToInventories == false)
        {
            Debug.LogWarning("Attention the scriptable object drop here must implement, ICanBeAddedTo player interface",this);
            return;
        }
        
        var iHaveInventories = other.GetComponentInParent<IHaveInventories>();
        if (iHaveInventories == null)
            return;
        
        CanBeAddedToInventories.AddToInventories(iHaveInventories); 
        Destroy(gameObject);
    }

    public void OnValidate()
    {
        if (_definition is ICanBeAddedToInventories canBeAddedToPlayer)
        {
            CanBeAddedToInventories = canBeAddedToPlayer;
        }
        else
        {
            CanBeAddedToInventories=null;
            Debug.LogWarning("the scriptable object drop here must implement, ICanBeAddedTo player interface",this);
        }
    }
}
