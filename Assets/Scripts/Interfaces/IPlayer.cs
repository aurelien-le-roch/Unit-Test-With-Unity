using UnityEngine;

public interface IPlayer
{
    IPlayerInput PlayerInput { get; set; }
    GameObject gameObject { get; }
}