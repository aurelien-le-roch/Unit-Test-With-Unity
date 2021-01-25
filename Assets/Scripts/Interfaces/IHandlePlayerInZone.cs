using System;

public interface IHandlePlayerInZone
{
    event Action OnPlayerEnterZone;
    event Action OnPlayerExitZone;
    bool PlayerInZone { get; }
    void PlayerEnterZone();
    void PlayerExitZone();
}