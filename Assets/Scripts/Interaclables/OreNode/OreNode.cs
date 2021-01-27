using UnityEngine;

public class OreNode : MonoBehaviour,IHaveIHandlePlayerInZone,IHaveIInteraclable,IHaveInteractablePercentZone,IHaveIHaveQteMining
{
    [SerializeField] private OreNodeDefinition _definition;
    [SerializeField] private Animator _animator;
    
    
    public IInteraclable Interaclable { get; set; }
    public InteractablePercentFocusHandling  InteractablePercentFocusHandling { get; private set; }
    public IHaveQteMining HaveQteMining { get; private set; }
    public OreNodeInteractable OreNodeInteractable { get; private set; }
    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; set; }
    private void Awake()
    {
         OreNodeInteractable=new OreNodeInteractable(this,_definition,_animator);
        
        Interaclable = OreNodeInteractable;
        HaveQteMining = OreNodeInteractable;
        InteractablePercentFocusHandling = OreNodeInteractable;
        HandlePlayerInteractableFocus = OreNodeInteractable;
    }

    
}