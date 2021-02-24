using System;
using UnityEngine;

public interface ICanHitTarget
{
    GameObject gameObject { get; }
    event Action<ICanHitTarget,IMTarget> OnHitTarget;
}