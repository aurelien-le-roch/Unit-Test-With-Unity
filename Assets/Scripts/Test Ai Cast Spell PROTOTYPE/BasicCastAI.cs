using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicCastAI : MonoBehaviour, ITargetPosition, IChangeState
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _waitTimeBetweenEachCast;
    private ICharacterWithSpells _characterWithSpells;
    private BasicCastStateMachine _basicCastStateMachine;
    public Vector3 TargetPosition => _targetTransform.position;

    public event Action<Type> OnCurrentStateTypeChange;
    public Type CurrentStateType => _basicCastStateMachine.CurrentStateType;

    private void Start()
    {
        _characterWithSpells = GetComponentInChildren<ICharacterWithSpells>();
        _basicCastStateMachine = new BasicCastStateMachine(_characterWithSpells, this, _waitTimeBetweenEachCast);
        _basicCastStateMachine.OnCurrentStateTypeChange += stateType => OnCurrentStateTypeChange?.Invoke(stateType);
    }

    private void Update()
    {
        _basicCastStateMachine.Tick(Time.deltaTime);
    }
}

internal interface IChangeState
{
    event Action<Type> OnCurrentStateTypeChange;
    Type CurrentStateType { get; }
}

public class BasicCastStateMachine : IChangeState
{
    public event Action<Type> OnCurrentStateTypeChange;
    public Type CurrentStateType => _stateMachine.CurrentState.GetType();

    private StateMachine _stateMachine;

    public BasicCastStateMachine(ICharacterWithSpells characterWithSpells, ITargetPosition targetPosition,
        float waitTimeBetweenEachCast)
    {
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnCurrentStateTypeChange?.Invoke(state.GetType());
        // var characterSpellInRange = new CharacterSpellInRange(characterWithSpells,targetPosition);
        var availableCondition = new CharacterSpellNotInCd(characterWithSpells);
        var idle = new Idle();
        var pickAvailableSpellInRange = new PickAvailableSpell(availableCondition);
        var usePickedSpell =
            new UsePickedSpell(characterWithSpells.CharacterSpells, pickAvailableSpellInRange, targetPosition);
        var waitTimeAfterCast = new WaitTimeAfterCast(waitTimeBetweenEachCast);


        _stateMachine.AddTransition(idle, pickAvailableSpellInRange,
            () => availableCondition.AvailableSpells.Count > 0);

        _stateMachine.AddTransition(pickAvailableSpellInRange, usePickedSpell,
            () => pickAvailableSpellInRange.SpellPicked != null);

        _stateMachine.AddTransition(usePickedSpell, waitTimeAfterCast,
            () => usePickedSpell.SpellCastIsOver);

        _stateMachine.AddTransition(waitTimeAfterCast, idle,
            () => waitTimeAfterCast.NoMoreTimeToWait);

        _stateMachine.SetState(idle);
    }

    public void Tick(float deltaTime)
    {
        _stateMachine.Tick(deltaTime);
    }
}

public class CharacterSpellNotInCd : IHaveAvailableSpells
{
    private readonly ICharacterWithSpells _characterWithSpells;
    public List<Spell> AvailableSpells { get; }

    public CharacterSpellNotInCd(ICharacterWithSpells characterWithSpells)
    {
        _characterWithSpells = characterWithSpells;
        AvailableSpells = new List<Spell>();

        _characterWithSpells.CharacterSpells.OnSpellBecomeNotAvailable += CalculateAvailables;
        _characterWithSpells.CharacterSpells.OnSpellBecomeAvailable += CalculateAvailables;
        CalculateAvailables();
    }

    private void CalculateAvailables()
    {
        AvailableSpells.Clear();
        
        foreach (var characterSpell in _characterWithSpells.CharacterSpells.Spells)
        {
            if (characterSpell.CanUse)
                AvailableSpells.Add(characterSpell);
        }
    }
}

public class WaitTimeAfterCast : IState
{
    public bool NoMoreTimeToWait => _timer <= 0;
    private readonly float _waitTimeBetweenEachCast;
    private float _timer;

    public WaitTimeAfterCast(float waitTimeBetweenEachCast)
    {
        _waitTimeBetweenEachCast = waitTimeBetweenEachCast;
    }

    public void Tick(float deltaTime)
    {
        _timer -= deltaTime;
    }

    public void OnEnter()
    {
        _timer = _waitTimeBetweenEachCast;
    }

    public void OnExit()
    {
    }
}

public class UsePickedSpell : IState
{
    public bool SpellCastIsOver { get; private set; }
    private readonly CharacterSpells _characterSpells;
    private readonly PickAvailableSpell _pickAvailableSpell;
    private readonly ITargetPosition _targetPosition;

    public UsePickedSpell(CharacterSpells characterSpells, PickAvailableSpell pickAvailableSpell,
        ITargetPosition targetPosition)
    {
        _characterSpells = characterSpells;
        _pickAvailableSpell = pickAvailableSpell;
        _targetPosition = targetPosition;
        SpellCastIsOver = true;
    }

    public void Tick(float deltaTime)
    {
        _characterSpells.UseSpell(_pickAvailableSpell.SpellPicked, _targetPosition.TargetPosition);
    }

    public void OnEnter()
    {
        SpellCastIsOver = false;
        var pickedSpell = _pickAvailableSpell.SpellPicked;
        pickedSpell.OnSpellCastOver += SetSpellCastOverToTrueAndClearPickedSpell;
        
    }

    public void OnExit()
    {
    }

    private void SetSpellCastOverToTrueAndClearPickedSpell()
    {
        _pickAvailableSpell.SpellPicked.OnSpellCastOver -= SetSpellCastOverToTrueAndClearPickedSpell;
        _pickAvailableSpell.ClearPickSpell();
        SpellCastIsOver = true;
    }
}

public class CharacterSpellInRange : IHaveAvailableSpells
{
    private readonly ICharacterWithSpells _characterWithSpells;
    private readonly ITargetPosition _targetPosition;
    public List<Spell> AvailableSpells { get; }

    public CharacterSpellInRange(ICharacterWithSpells characterWithSpells, ITargetPosition targetPosition)
    {
        _characterWithSpells = characterWithSpells;
        _targetPosition = targetPosition;
        AvailableSpells = new List<Spell>();

        _characterWithSpells.CharacterSpells.OnSpellBecomeNotAvailable += CalculateAvailables;
        _characterWithSpells.CharacterSpells.OnSpellBecomeAvailable += CalculateAvailables;
        CalculateAvailables();
    }

    private void CalculateAvailables()
    {
        AvailableSpells.Clear();

        var canUseSpells = new List<Spell>();

        foreach (var characterSpell in _characterWithSpells.CharacterSpells.Spells)
        {
            if (characterSpell.CanUse)
                canUseSpells.Add(characterSpell);
        }


        foreach (var canUseSpell in canUseSpells)
        {
            if (_characterWithSpells.CharacterSpells.InRangeToHit(canUseSpell, _targetPosition))
                AvailableSpells.Add(canUseSpell);
        }
    }
}

public interface IHaveAvailableSpells
{
    List<Spell> AvailableSpells { get; }
}


public class PickAvailableSpell : IState
{
    private readonly IHaveAvailableSpells _characterSpellInRange;

    public Spell SpellPicked { get; private set; }

    public PickAvailableSpell(IHaveAvailableSpells characterSpellInRange)
    {
        _characterSpellInRange = characterSpellInRange;
    }


    public void ClearPickSpell() => SpellPicked = null;

    public void Tick(float deltaTime)
    {
    }

    public void OnEnter()
    {
        var randomRange = _characterSpellInRange.AvailableSpells.Count;
        var randomIndex = Random.Range(0, randomRange);
        SpellPicked = _characterSpellInRange.AvailableSpells[randomIndex];
    }

    public void OnExit()
    {
    }
}

public class Idle : IState
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

public interface ITargetPosition
{
    Vector3 TargetPosition { get; }
}