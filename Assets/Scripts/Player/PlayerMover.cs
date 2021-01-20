using UnityEngine;

public class PlayerMover
{
    private readonly Player _player;

    public PlayerMover(Player player)
    {
        _player = player;
    }

    public void Tick()
    {
        var movement = new Vector3(_player.PlayerInput.Horizontal, _player.PlayerInput.Vertical, 0);
        movement.Normalize();

        _player.transform.position += Time.fixedDeltaTime * _player.Speed * movement;
    }
}