using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour,IPointerClickHandler
{
    private ChestView chestView;

    public void OnPointerClick(PointerEventData eventData)
    {
        chestView = GetComponent<ChestView>();
        if(chestView != null)
        {
            UIService.Instance.OnChestClick(chestView);
        }
        else
        {
            Debug.Log("Chest not found");
        }
    }
}
