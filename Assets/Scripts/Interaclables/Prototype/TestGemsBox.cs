using UnityEngine;

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