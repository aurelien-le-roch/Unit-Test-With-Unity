using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestGemsBoxInteractable : IHandlePlayerInteractableFocus, IInteraclable
{
    public event Action OnPlayerFocusMe;
    public event Action OnPlayerStopFocusMe;
    public bool IHavePlayerFocus { get; private set; }

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
    public void PlayerStartToFocusMe()
    {
        IHavePlayerFocus = true;
        OnPlayerFocusMe?.Invoke();
    }

    public void PlayerStopToFocusMe()
    {
        IHavePlayerFocus = false;
        OnPlayerStopFocusMe?.Invoke();
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