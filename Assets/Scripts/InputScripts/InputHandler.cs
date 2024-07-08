
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
        if(chestView != null)
        {
            UIService.Instance.OnChestClick();
            chestView.chestController.CreateStateMachine();
            UIService.Instance.SetCurrentChestView(chestView);
        }
        else
        {
            Debug.Log("Chest not found");
        }
    }
    public void SetClickStatus(bool canClick)
    {
        isClickEnabled = canClick;
    }
}
