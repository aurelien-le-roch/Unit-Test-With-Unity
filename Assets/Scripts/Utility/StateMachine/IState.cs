public interface IState
{
    void Tick(float deltaTime);
    void OnEnter();
    void OnExit();
}