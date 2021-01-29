using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    [SerializeField] private ScriptableObject _definition;

    private ICanBeAddedToPlayer _canBeAddedToPlayer;

    private void Awake()
    {
        if (_definition is ICanBeAddedToPlayer canBeAddedToPlayer)
        {
            _canBeAddedToPlayer = canBeAddedToPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_canBeAddedToPlayer == null)
        {
            Debug.LogWarning("Attention the scriptable object drop here must implement, ICanBeAddedTo player interface",this);
            return;
        }
        
        var player = other.GetComponentInParent<Player>();
        if (player == null)
            return;
        
        _canBeAddedToPlayer.AddToPlayer(player); 
        Destroy(gameObject);
    }

    public void OnValidate()
    {
        if (_definition is ICanBeAddedToPlayer canBeAddedToPlayer)
        {
            _canBeAddedToPlayer = canBeAddedToPlayer;
        }
        else
        {
            _canBeAddedToPlayer=null;
            Debug.LogError("the scriptable object drop here must implement, ICanBeAddedTo player interface",this);
        }
    }
}
