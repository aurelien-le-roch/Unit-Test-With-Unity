using System;
using AimCraftMiniGame;
using UnityEngine;

public class AimStateMachine : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private AimSpawnerSettings _settings;
    [SerializeField] private AimTarget _targetPrefab;
    [SerializeField] private float _beginTime=3f;
    private StateMachine _stateMachine;
    private Aim _aim;
    private AimBegin _beginState;
    private AimEnd _endState;
    public float BeginTimer => _beginState.TimerBeforePlay;
    public float BeginTime => _beginTime;
    public IAimScore Score { get;  set; }
    public IAimLifePoint LifePoint { get;  set; }
    public bool CanStartMiniGame => _stateMachine.CurrentState is AimIdle ||_stateMachine.CurrentState is AimEnd;
    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    public event Action<IState> OnStateChange;
    public event Action<int> OnMiniGameEnd;
    private void Awake()
    {
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnStateChange?.Invoke(state);

        _aim = new Aim(_camera,_settings,_targetPrefab);
        Score = _aim.Score;
        LifePoint = _aim.LifePoint;
        var idle = new AimIdle();
        _beginState = new AimBegin(_aim,_beginTime,transform);
        var play = new AimPlay(_aim);
        _endState = new AimEnd(_aim.Score);
        _endState.OnAimResult += score => OnMiniGameEnd?.Invoke(score);
        
        _stateMachine.AddTransition(idle,_beginState,()=> Score.MaxScore>0);
        _stateMachine.AddTransition(_beginState,play,()=> BeginTimer<=0);
        _stateMachine.AddTransition(play,_endState,()=>LifePoint.CurrentLifePoint<=0 ||Score.CurrentScore>=Score.MaxScore);
        _stateMachine.AddTransition(_endState,_beginState,()=>Score.MaxScore>0);
        
        _stateMachine.SetState(idle);
    }

    public void TryToBeginMiniGame(int maxAmount)
    {
        if (CanStartMiniGame==false)
            return ;
        
        _aim.Score.SetMaxScore(maxAmount);
    }

    private void Update()
    {
        _stateMachine.Tick(Time.deltaTime);
    }

    public void BindToAPlayer(Player player)
    {
        _beginState.BindToAPlayer(player);
        _endState.BindToAPlayer(player);
    }
}

public class AimEnd : IState
{
    private readonly AimScore _aimScore;
    private Player _player;
    public event Action<int> OnAimResult;
    public AimEnd(AimScore aimScore)
    {
        _aimScore = aimScore;
    }

    public void Tick(float deltaTime)
    {
    }

    public void OnEnter()
    {
        OnAimResult?.Invoke(_aimScore.CurrentScore);
        _aimScore.SetMaxScore(0);
        
        if(_player==null)
            return;
            
        _player.EnableInput(true);
        _player.PlayerCamera.SetTarget(_player.transform);
    }

    public void OnExit()
    {
        
    }

    public void BindToAPlayer(Player player)
    {
        _player = player;
    }
}
public class AimPlay : IState
{
    private readonly Aim _aim;

    public AimPlay(Aim aim)
    {
        _aim = aim;
    }
    
    public void Tick(float deltaTime)
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
    private readonly Transform _cameraTarget;
    private Player _player;

    public AimBegin(Aim aim, float timeBeforePlay, Transform cameraTarget)
    {
        _aim = aim;
        _timerDuration = timeBeforePlay;
        _cameraTarget = cameraTarget;
    }
    

    public void Tick(float deltaTime)
    {
        TimerBeforePlay -= deltaTime;
    }

    public void OnEnter()
    {
        _aim.Reset();
        TimerBeforePlay = _timerDuration;
        
        if(_player==null)
            return;
        _player.EnableInput(false);
        _player.PlayerCamera.SetTarget(_cameraTarget);
    }

    public void OnExit()
    {
    }

    public void BindToAPlayer(Player player)
    {
        _player = player;
    }
}


public class AimIdle : IState
{
    public void Tick(float deltaTime)
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}







