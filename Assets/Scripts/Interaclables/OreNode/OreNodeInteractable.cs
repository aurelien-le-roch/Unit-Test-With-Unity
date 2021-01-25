using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class OreNodeInteractable : InteractablePercentZone,IHaveQteMining
{
    private OreNodeDefinition _definition;
    private Animator _animator;
    private static readonly int InteractAnimation = Animator.StringToHash("Interact");
    
    private int _numberOfQTENeeded;
    private int _difficultyOfQTE;
    private int _numberOfQteDone;
    private float _qualityOfQTE;
    private static readonly int Destroy1 = Animator.StringToHash("Destroy");


    public QTEMining QTEMining { get; private set; }

    private MonoBehaviour _oreNodeMonoBehaviour;

    public OreNodeInteractable(MonoBehaviour monoBehaviour,OreNodeDefinition definition,Animator animator)
    {
        _oreNodeMonoBehaviour = monoBehaviour;
        _definition = definition;
        _animator = animator;
        QTEMining=new QTEMining();
        QTEMining.OnQTEEnd += HandleQTEResult;
    }
    
    public override void PlayerExitZone()
    {
        base.PlayerExitZone();
        
        _animator.SetBool(InteractAnimation, false);
        
        if (AlreadyHit100Percent)
        {
            QTEMining.JobIsOver();
            _oreNodeMonoBehaviour.StartCoroutine(DestroyAndGiveGems(0));
        }
    }
    
    public override void DontInteract(float deltaTime)
    {
        base.DontInteract(deltaTime);
        _animator.SetBool(InteractAnimation, false);
    }

    public override void InteractHold(GameObject interactor,float deltaTime)
    {
        base.InteractHold(interactor,deltaTime);
        _animator.SetBool(InteractAnimation, true);
    }

    protected override void InteractHoldHit100Percent(GameObject interactor)
    {
        if(AlreadyHit100Percent)
            return;
        
        base.InteractHoldHit100Percent(interactor);
        
        var workController = interactor.GetComponent<IHaveWorkController>().WorkController;


        if (workController == null) 
            return;
        
        var qteSetting = workController.ProcessOreMining(this);
        PrepareNodeToQTE(qteSetting);
    }
    
    protected override void InteractDownAfter100PercentHit(GameObject interactor)
    {
        base.InteractDownAfter100PercentHit(interactor);
        
        if (_numberOfQteDone >= _numberOfQTENeeded)
            return;
        
        QTEMining.Use(_oreNodeMonoBehaviour,Time.time);
    }
    
    private void PrepareNodeToQTE(QteMiningSetting setting)
    {
        if(QTEMining.IsSetup)
            return;
        
        _numberOfQTENeeded = setting.Number;
        _difficultyOfQTE = setting.Difficulty;
        QTEMining.SetupQTE(1f,0.6f,0.2f);
    }
    
    private void HandleQTEResult(QteResult result)
    {
        Debug.Log("result are here = "+result);
        _numberOfQteDone++;
        if (result == QteResult.Perfect)
            _qualityOfQTE = 1;
        else if (result == QteResult.Medium)
            _qualityOfQTE = 0.5f;
        else
            _qualityOfQTE = 0;

        if (_numberOfQteDone >= _numberOfQTENeeded)
        {
            QTEMining.Reset(result);
            QTEMining.JobIsOver();
            _oreNodeMonoBehaviour.StartCoroutine(DestroyAndGiveGems(_qualityOfQTE));
        }
            
        else
            QTEMining.Reset(result);
    }
    
    private IEnumerator DestroyAndGiveGems(float percent)
    {
        _animator.SetTrigger(Destroy1);
        yield return new WaitForSeconds(1f);
        
        var numberOfGemsBeforePercent = Random.Range(_definition.MinOreGiven, _definition.MaxOreGiven + 1);
        var numberOfGemsSpawn = numberOfGemsBeforePercent * percent;

        ObjectsSpawner.InRandomCircle(_definition.GemsDropAfterMine,(int) numberOfGemsSpawn,0.5f,_oreNodeMonoBehaviour.transform.position);
        GameObject.Destroy(_oreNodeMonoBehaviour.gameObject);
    }
}