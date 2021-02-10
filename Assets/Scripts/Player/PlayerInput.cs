﻿using UnityEngine;

public class PlayerInput : IPlayerInput
{
    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");

    public bool InteractHold => Input.GetKey(KeyCode.E);
    public bool InteractDown => Input.GetKeyDown(KeyCode.E);
    public bool LeftClickDown => Input.GetMouseButtonDown(0);

    public Vector3 MousePosition => Input.mousePosition;
}