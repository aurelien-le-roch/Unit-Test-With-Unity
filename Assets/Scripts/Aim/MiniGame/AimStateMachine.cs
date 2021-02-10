using System;
using AimCraftMiniGame;
using UnityEngine;

public class AimStateMachine : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AimSpawnerSettings _settings;
    [SerializeField] private AimTarget _targetPrefab;
    private StateMachine _stateMachine;
    private Aim _aim;
    private AimBegin _beginState;
    public float BeginTimer => _beginState.TimerBeforePlay;

    public IAimScore Score { get; private set; }
    public IAimLifePoint LifePoint { get; private set; }
    public event Action<IState> OnStateChange;
    
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnStateChange?.Invoke(state);

        _aim = new Aim(_camera,_settings,_targetPrefab);
        Score = _aim.Score;
        LifePoint = _aim.LifePoint;
        var idle = new AimIdle();
        _beginState = new AimBegin(_aim,3f);
        var play = new AimPlay(_aim);
        var end = new AimEnd(_aim.Score);
        
        _stateMachine.AddTransition(idle,_beginState,()=> Score.MaxScore!=0);
        _stateMachine.AddTransition(_beginState,play,()=> BeginTimer<=0);
        _stateMachine.AddTransition(play,end,()=>_aim.LifePoint.CurrentLifePoint<=0 ||Score.CurrentScore>=Score.MaxScore);
        _stateMachine.AddTransition(end,idle,()=>Score.MaxScore==0);
        
        _stateMachine.SetState(idle);
    }

    public void TryToBeginMiniGame(int maxAmount)
    {
        if (_stateMachine.CurrentState is AimIdle == false)
            return ;
        
        _aim.Score.SetMaxScore(maxAmount);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}

public class AimEnd : IState
{
    private readonly AimScore _aimScore;

    public AimEnd(AimScore aimScore)
    {
        _aimScore = aimScore;
    }

    public void Tick()
    {
    }

    public void OnEnter()
    {
        _aimScore.SetMaxScore(0);
    }

    public void OnExit()
    {
    }
}
public class AimPlay : IState
{
    private readonly Aim _aim;

    public AimPlay(Aim aim)
    {
        _aim = aim;
    }

    public void Tick()
    {
        _aim.Tick();
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}
public class AimBegin : IState
{
    private readonly Aim _aim;
    public float TimerBeforePlay;
    private readonly float _timerDuration;
    public AimBegin(Aim aim,float timeBeforePlay)
    {
        _aim = aim;
        _timerDuration = timeBeforePlay;
    }

    public void Tick()
    {
        TimerBeforePlay -= Time.deltaTime;
    }

    public void OnEnter()
    {
        _aim.Reset();
        TimerBeforePlay = _timerDuration;
    }

    public void OnExit()
    {
    }
}


public class AimIdle : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}







