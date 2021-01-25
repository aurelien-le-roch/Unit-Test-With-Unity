public interface IPlayerHandleInteractable
{
    void Tick(float deltaTime);
    void OnTriggerEnter2D(IInteraclable iInteraclable);
    void OnTriggerExit2D(IInteraclable iInteraclable);
}