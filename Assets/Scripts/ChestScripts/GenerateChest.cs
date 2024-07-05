using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GenerateChest : MonoBehaviour
{
    [SerializeField] private Button generateChestButton;
    [SerializeField] private Slots[] slots;
    [SerializeField] private GameObject tempChest;
    private void Start()
    {
        generateChestButton.onClick.AddListener(SpawnChest);        
    }
    private void SpawnChest()
    {        
            Slots emptySlot = GetEmptySlot();
            if(emptySlot != null)
            {
                Instantiate(tempChest, emptySlot.slotParentTransform);
                emptySlot.slotStatus = SlotStatus.Occuipied;
                
            }
            else
            {
                Debug.Log("SlotsFull");
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
