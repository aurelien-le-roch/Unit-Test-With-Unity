using System;

public interface IHandlePlayerInteractableFocus
{
    event Action OnPlayerFocusMe;
    event Action OnPlayerStopFocusMe;
    bool IHavePlayerFocus { get; }
    void PlayerStartToFocusMe();
    void PlayerStopToFocusMe();
}