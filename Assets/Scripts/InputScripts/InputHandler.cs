
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour,IPointerClickHandler
{
    private ChestView chestView;
    private bool isClickEnabled = true;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClickEnabled)
        {
            Debug.Log("Click Disabled");
            return;
        }
        chestView = GetComponent<ChestView>();
        GameService.Instance.UIService.SetCurrentChestView(chestView);       
        chestView.chestController.CheckCurrentState(chestView.chestController.currentChestState);        
    }
    public void SetClickStatus(bool canClick)
    {
        isClickEnabled = canClick;
    }
   
}
