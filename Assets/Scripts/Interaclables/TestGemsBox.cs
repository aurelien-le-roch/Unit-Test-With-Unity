using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestGemsBox : MonoBehaviour,IHaveIHandlePlayerInZone,IHaveIInteraclable
{
    [SerializeField] private LootableItem _gems;
    [SerializeField] private int _minAmount;
    [SerializeField] private int _maxAmount;


    public IHandlePlayerInZone HandlePlayerInZone { get; private set; }
    public IInteraclable Interaclable { get; private set; }

    private void Awake()
    {
        var testGemsBoxInteractable = new TestGemsBoxInteractable(gameObject, _gems, _minAmount, _maxAmount);
        HandlePlayerInZone = testGemsBoxInteractable;
        Interaclable = testGemsBoxInteractable;
    }
}

public class TestGemsBoxInteractable : IHandlePlayerInZone, IInteraclable
{
    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public bool PlayerInZone { get; private set; }

    private GameObject TestGemsBox;
    private LootableItem _gems;
    private int _minAmount;
    private int _maxAmount;

    public TestGemsBoxInteractable(GameObject gameObject,LootableItem prefabToSpawn,int minAmount,int maxAmount)
    {
        TestGemsBox = gameObject;
        _gems = prefabToSpawn;
        _minAmount = minAmount;
        _maxAmount = maxAmount;
    }
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
        ObjectsSpawner.InRandomCircle(_gems,numberOfSpawn,0.5f,TestGemsBox.transform.position);
        GameObject.Destroy(TestGemsBox.gameObject);
    }

    public void InteractHold(GameObject interactor,float deltaTime)
    {
    }

    public void DontInteract(float deltaTime)
    {
    }
}


