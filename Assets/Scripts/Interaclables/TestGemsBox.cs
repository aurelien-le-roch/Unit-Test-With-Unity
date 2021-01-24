using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestGemsBox : MonoBehaviour,IHandlePlayerInZone,IInteraclable
{
    [SerializeField] private LootableItem _gems;
    [SerializeField] private int _minAmount;
    [SerializeField] private int _maxAmount;
    
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public bool PlayerInZone { get; private set; }
    
    public void PlayerEnterZone()
    {
        PlayerInZone = true;
        OnPlayerEnterZone?.Invoke();
    }

    public void PlayerExitZone()
    {
        PlayerInZone = false;
        OnPlayerExitZone?.Invoke();
    }
    public void InteractDown(GameObject interactor)
    {
        int numberOfSpawn = Random.Range(_minAmount, _maxAmount + 1);
        ObjectsSpawner.InRandomCircle(_gems,numberOfSpawn,0.5f,transform.position);
        Destroy(gameObject);
    }

    public void InteractHold(GameObject interactor)
    {
    }

    public void DontInteract()
    {
    }
}


