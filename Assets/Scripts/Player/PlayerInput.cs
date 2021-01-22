using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool InteractHold => Input.GetKey(KeyCode.E);
    public bool InteractDown => Input.GetKeyDown(KeyCode.E);
}

public interface IPlayerInput
{
    float Horizontal { get; }
    float Vertical { get; }

    bool InteractHold { get; }
    bool InteractDown { get; }
}
