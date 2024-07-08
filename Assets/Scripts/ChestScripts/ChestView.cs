using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestView chestViewPrefab;
    public TextMeshProUGUI chestTypeText;
    public Image chestImage;
    public TextMeshProUGUI chestStatusText;
    public InputHandler inputHandler;
    public ChestController chestController { get; private set; }

    public void SetViewController(ChestController chestController)
    {
        this.chestController = chestController;
    }
    private void Start()
    {
        chestController.SetChest();
    }
    private void Update()
    {
        if (chestController!=null && chestController.chestStateMachine!=null && 
            chestController.chestStateMachine.CurrentState == chestController.chestStateMachine.unlockingState)
        {
            chestController.chestStateMachine.Update();
        }
    }
}
