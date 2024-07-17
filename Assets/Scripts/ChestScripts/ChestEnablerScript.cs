using UnityEngine;
public class ChestEnablerScript : MonoBehaviour
{
    [SerializeField] private GameObject chestsMainPanel;
    public void DisableLockedChests()
    {
        ChestView[] chestView = chestsMainPanel.GetComponentsInChildren<ChestView>();
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
    public void EnableAllChests()
    {
        ChestView[] chestView = chestsMainPanel.GetComponentsInChildren<ChestView>();
        foreach (ChestView chest in chestView)
        {
            chest.ChestController.EnableClickingCurrentChest();
        }
    }
}
