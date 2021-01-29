using UnityEngine;

public class TestGemsBox : MonoBehaviour,IHaveIHandlePlayerInteractableFocus,IHaveIInteraclable
{
    [SerializeField] private Lootable _gems;
    [SerializeField] private int _minAmount;
    [SerializeField] private int _maxAmount;


    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; private set; }
    public IInteraclable Interaclable { get; private set; }

    private void Awake()
    {
        var testGemsBoxInteractable = new TestGemsBoxInteractable(gameObject, _gems, _minAmount, _maxAmount);
        HandlePlayerInteractableFocus = testGemsBoxInteractable;
        Interaclable = testGemsBoxInteractable;
    }
}