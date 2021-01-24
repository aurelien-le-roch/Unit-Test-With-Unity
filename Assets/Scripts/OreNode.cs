using UnityEngine;

public class OreNode : MonoBehaviour,IHaveIInteraclable,IHaveInteractablePercentZone,IHaveIHaveQteMining
{
    [SerializeField] private OreNodeDefinition _definition;
    [SerializeField] private Animator _animator;
    
    
    public IInteraclable Interaclable { get; private set; }
    public InteractablePercentZone  InteractablePercentZone { get; private set; }
    public IHaveQteMining HaveQteMining { get; private set; }
    public OreNodeInteractable OreNodeInteractable { get; private set; }
    private void Awake()
    {
         OreNodeInteractable=new OreNodeInteractable(this,_definition,_animator);
        
        Interaclable = OreNodeInteractable;
        HaveQteMining = OreNodeInteractable;
        InteractablePercentZone = OreNodeInteractable;
    }
}

public interface IHaveIInteraclable
{
    IInteraclable Interaclable { get; }
}

public interface IHaveInteractablePercentZone
{
    InteractablePercentZone InteractablePercentZone { get; }
}
public interface IHaveIHaveQteMining
{
    IHaveQteMining HaveQteMining { get; }
}


