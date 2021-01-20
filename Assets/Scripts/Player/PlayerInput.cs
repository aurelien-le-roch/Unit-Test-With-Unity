using UnityEngine;

public class PlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool InteractHold => Input.GetKey(KeyCode.E);
    public bool InteractDown => Input.GetKeyDown(KeyCode.E);
}