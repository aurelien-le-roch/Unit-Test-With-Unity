using UnityEngine;

public interface IPlayerInput
{
    float Horizontal { get; }
    float Vertical { get; }

    bool InteractHold { get; }
    bool InteractDown { get; }
    bool LeftClickDown { get; }
    Vector3 MousePosition { get; }
    bool Spell1 { get;}
    bool Spell2 { get;}
    bool Spell3 { get;}
    bool Spell4 { get;}
}