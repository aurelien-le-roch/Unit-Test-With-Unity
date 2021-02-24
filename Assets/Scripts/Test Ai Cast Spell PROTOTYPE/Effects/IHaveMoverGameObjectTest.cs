using System;
using System.Collections;
using AimCraftMiniGame;
using UnityEngine;

public class IHaveMoverGameObjectTest : MonoBehaviour,IHaveMover,ICanHitTarget,IMTarget
{
    public IMover Mover { get; private set; }
    public event Action<ICanHitTarget,IMTarget> OnHitTarget;

    public void ChangeMover(IMover newMover)
    {
        Mover = newMover;
    }

    private void FixedUpdate()
    {
        Mover?.Tick();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var isTarget = other.GetComponent<IMTarget>();
        if(isTarget==null)
            return;
        
        OnHitTarget?.Invoke(this,isTarget);
    }
}