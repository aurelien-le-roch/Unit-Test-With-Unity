using System;

public interface IWorkController
{
    QteMiningSetting ProcessOreMining(OreNodeInteractable oreNodeInteractable);

    CraftController2 CraftController { get; }
    //int GetCraftXp();
    //void GiveCraftXp(int amount);
    //event Action<IHaveInventories, CraftInfo, AimStateMachine> OnCraftWorkshopInteraction; 
    //void ProcessCraftWorkshopInteraction( IHaveInventories haveInventories, CraftInfo craftInfo, AimStateMachine aimStateMachine);
}