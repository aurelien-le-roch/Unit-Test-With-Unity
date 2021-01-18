using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class OreNode : MonoBehaviour, IInteractablePercent,IHaveQTEMining
{
    [SerializeField] private OreNodeDefinition _definition;
    [SerializeField] private float _interactSpeed;
    [SerializeField] private Animator _animator;
    private static readonly int InteractAnimation = Animator.StringToHash("Interact");
    public float InteractPercent { get; set; }

    public event Action OnPlayerEnterZone;
    public event Action OnPlayerExitZone;
    public event Action OnInteractableHit100Percent;
    public bool PlayerInZone { get; private set; }


    private int _numberOfQTENeeded;
    private int _difficultyOfQTE;
    private int _numberOfQteDone;
    private float _qualityOfQTE;
    

    public QTEMining QTEMining { get; private set; }

    private void Awake()
    {
        QTEMining=new QTEMining();
        QTEMining.OnQTEEnd += HandleQTEResult;
    }

    public void InteractHold(GameObject interactor)
    {
        if (InteractPercent >= 1)
        {
            InteractHoldAt100Percent(interactor);
        }

        if (InteractPercent < 1)
        {
            InteractPercent += Time.deltaTime * _interactSpeed;
        }

        _animator.SetBool(InteractAnimation, true);
    }

    public void InteractDown(GameObject interactor)
    {
        if (InteractPercent < 1)
            return;
        
        QTEMining.Use(this);
        
        Debug.Log("Qte use Call pass");
        if (_numberOfQteDone >= _numberOfQTENeeded)
        {
            QTEMining.JobOver();
        }
    }

    public void DontInteract()
    {
        _animator.SetBool(InteractAnimation, false);
        if (InteractPercent > 0 && InteractPercent < 1)
        {
            InteractPercent -= Time.deltaTime * _interactSpeed;
        }
    }

    public void ResetInteractPercent()
    {
        InteractPercent = 0;
    }


    private void HandleQTEResult(QTEResult result)
    {
        Debug.Log("result are here = "+result);
        _numberOfQteDone++;
    }
    private void InteractHoldAt100Percent(GameObject interactor)
    {
        if(QTEMining.IsSetup)
            return;
        OnInteractableHit100Percent?.Invoke();
        var workController = interactor.GetComponent<IHaveWorkController>().WorkController;

        workController?.OreMining(this);
    }

    public void PlayerEnterZone()
    {
        PlayerInZone = true;
        OnPlayerEnterZone?.Invoke();
    }

    public void PlayerExitZone()
    {
        _animator.SetBool(InteractAnimation, false);
        PlayerInZone = false;
        InteractPercent = 0;
        if (QTEMining.IsSetup)
        {
            DestroyAndGiveGems(0);
        }
        OnPlayerExitZone?.Invoke();
    }


    public void DestroyAndGiveGems(float percent)
    {
        var numberOfGemsBeforePercent = Random.Range(_definition.MinOreGiven, _definition.MaxOreGiven + 1);
        var numberOfGemsSpawn = numberOfGemsBeforePercent * percent;
        //SpawnGems;
        Destroy(gameObject);
    }

    public void PrepareNodeToQTE(int numberOfQte, int difficultyOfQte)
    {
        if(QTEMining.IsSetup)
            return;
        _numberOfQTENeeded = numberOfQte;
        _difficultyOfQTE = difficultyOfQte;
        QTEMining.SetupQTE(10f,8f,2f);
    }

    private void OnDisable()
    {
        QTEMining.OnQTEEnd -= HandleQTEResult;
    }
}

public interface IHaveQTEMining
{
    QTEMining QTEMining { get; }
}

public interface IInteractablePercent
{
    float InteractPercent { get; }
    void InteractHold(GameObject interactor);
    void InteractDown(GameObject interactor);
    void DontInteract();
    void PlayerEnterZone();
    void PlayerExitZone();
    event Action OnPlayerEnterZone;
    event Action OnPlayerExitZone;
    event Action OnInteractableHit100Percent;
    bool PlayerInZone { get; }
}

public class WorkControllerTest : IWorkController
{
    private OreMiningController _oreMiningController;

    public WorkControllerTest()
    {
        _oreMiningController = new OreMiningController();
    }

    public void OreMining(OreNode oreNode)
    {
        _oreMiningController.OreMining(oreNode);
    }
}

public class OreMiningController
{


    public void OreMining(OreNode oreNode)
    {
        var numberOfQTE = GetNumberOfQTE();
        
        if (numberOfQTE == 0)
        {
            oreNode.DestroyAndGiveGems(1);
            return;
        }

        var difficultyOfQTE = GetDifficultyOfQTE();

        PrepareMiningQTE(numberOfQTE, difficultyOfQTE, oreNode);
    }

    private void PrepareMiningQTE(int numberOfQte, int difficultyOfQte, OreNode oreNode)
    {
        Debug.Log("set QTE info to mining node + Qte rdy state to node ?");
        oreNode.PrepareNodeToQTE(numberOfQte, difficultyOfQte);
    }

    private int GetDifficultyOfQTE()
    {
        return 1;
    }

    private int GetNumberOfQTE()
    {
        return 1;
    }
}


public interface IHaveWorkController
{
    IWorkController WorkController { get; }
}

public interface IWorkController
{
    void OreMining(OreNode oreNode);
}


[CreateAssetMenu(menuName = "OreNodeDefinition")]
public class OreNodeDefinition : ScriptableObject
{
    public int Level;
    public OreType OreType;
    public int MinOreGiven;
    public int MaxOreGiven;
}

public enum OreType
{
    Bronze,
    Silver,
    Gold,
}

