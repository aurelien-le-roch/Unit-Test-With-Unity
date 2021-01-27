using System;
using UnityEngine;

public class Workbench : MonoBehaviour,IHaveIHandlePlayerInZone,IHaveIInteraclable
{
    public IHandlePlayerInteractableFocus HandlePlayerInteractableFocus { get; private set; }
    public IInteraclable Interaclable { get; private set; }

    private void Awake()
    {
        var workbenchInteractable = new WorkbenchInteractable();
        HandlePlayerInteractableFocus = workbenchInteractable;
        Interaclable = workbenchInteractable;
    }
}

public class WorkbenchInteractable : IHandlePlayerInteractableFocus, IInteraclable
{
    public event Action OnPlayerFocusMe;
    public event Action OnPlayerStopFocusMe;
    public bool IHavePlayerFocus { get; private set; }
    
    
    public void PlayerStartToFocusMe()
    {
        IHavePlayerFocus = true;
        OnPlayerFocusMe?.Invoke();
    }

    public void PlayerStopToFocusMe()
    {
        IHavePlayerFocus = false;
        OnPlayerStopFocusMe?.Invoke();
        Debug.Log("close Interact workBench");
        
    }

    public void InteractDown(GameObject interactor)
    {
        Debug.Log("Open Interact workBench");
    }

    public void InteractHold(GameObject interactor, float deltaTime)
    {
    }

    public void DontInteract(float deltaTime)
    {
    }
}

public class CraftController
{
         // dic de recettes with crafting setting (nombre de craft possible,min goal,nombre de tentative a chaque craft,...)
         // the dic se refresh sous diff condition ...
         
         // begin craft(recette)
         // on craft end ...
         //on craft exit ...
         
         // a la fin reterir des items/en ajoute + ajoute de l'xp au métier de craft
 }