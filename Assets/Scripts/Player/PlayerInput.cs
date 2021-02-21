using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool InteractHold => Input.GetKey(KeyCode.E);
    public bool InteractDown => Input.GetKeyDown(KeyCode.E);
    public bool LeftClickDown => Input.GetMouseButtonDown(0);

    public Vector3 MousePosition => Input.mousePosition;
    public bool Spell1 => Input.GetKey(KeyCode.Alpha1);
    public bool Spell2 => Input.GetKey(KeyCode.Alpha2);
    public bool Spell3 => Input.GetKey(KeyCode.Alpha3);
    public bool Spell4 => Input.GetKey(KeyCode.Alpha4);
}