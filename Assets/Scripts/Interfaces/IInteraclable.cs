using UnityEngine;

public interface IInteraclable
{
    void InteractDown(GameObject interactor);
    void InteractHold(GameObject interactor,float deltaTime);
    void DontInteract(float deltaTime);
}