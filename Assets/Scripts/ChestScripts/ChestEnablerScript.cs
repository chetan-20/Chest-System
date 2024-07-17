using System.Collections.Generic;
public class ChestEnablerScript
{  
    public void DisableLockedChests(List<ChestView> chestView)
    {       
        foreach (ChestView chest in chestView)
        {
            if (chest.ChestController.GetCurrentState() == ChestStates.LOCKED)
            {
                chest.ChestController.DisableClickingCurrentChest();
            }
            else
            {
                chest.ChestController.EnableClickingCurrentChest();
            }
        }
    }
    public void EnableAllChests(List<ChestView> chestView)
    {         
        foreach (ChestView chest in chestView)
        {
            chest.ChestController.EnableClickingCurrentChest();
        }
    }
}
