using UnityEngine;
using UnityEngine.UI;
public class ChestSlotService : MonoBehaviour
{
    [SerializeField] private Button generateChestButton;
    [SerializeField] private Slots[] slots;
    [SerializeField] private ChestDataSO[] chestData;
    [SerializeField] private ChestView chestPrefab;
    private void Start()
    {
        generateChestButton.onClick.AddListener(SpawnChest);        
    }
    private void SpawnChest()
    {        
            Slots emptySlot = GetEmptySlot();
            if(emptySlot != null)
            {
                ChestView newChestView=Instantiate(chestPrefab, emptySlot.slotParentTransform); 
                ChestDataSO randomChestData = GetRandomChestData();
                ChestController newChestController = new(randomChestData,newChestView);            
                emptySlot.slotStatus=SlotStatus.Occuipied;
                if(GameService.Instance.GetTimerStatus())
                   {
                        newChestController.DisableClickingCurrentChest();
                   }
            }
            else
                {
                    GameService.Instance.PopUpService.DisplayPopUp("SLOTS FULL");
                }           
    }
    private Slots GetEmptySlot()
    {
        foreach(Slots SLOT in slots)
        {
            if(SLOT.slotStatus == SlotStatus.Empty)
            {
                return SLOT;
            }            
        }
        return null;
    }
    private ChestDataSO GetRandomChestData()
    {
        int random = Random.Range(0, chestData.Length);
        return Instantiate(chestData[random]);
    }
    public void MarkSlotEmpty(Transform parentTransform)
    {
        foreach (Slots SLOT in slots)
        {
            if (SLOT.slotParentTransform == parentTransform)
            {
                SLOT.slotStatus = SlotStatus.Empty;
            }
        }

    }
}

[System.Serializable]
public class Slots
{
    public Transform slotParentTransform;
    public SlotStatus slotStatus;
}
public enum SlotStatus
{
    Empty,
    Occuipied
}
