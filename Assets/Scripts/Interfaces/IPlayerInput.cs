public interface IPlayerInput
{
    float Horizontal { get; }
    float Vertical { get; }

    bool InteractHold { get; }
    bool InteractDown { get; }
}