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
    public Button unlockAfterTimerButton;
    public TextMeshProUGUI unlockAfterTimerText;
    public Transform parentTransform;
    public Button undoChestButton;
    public ChestController chestController { get; private set; }

    public void SetViewController(ChestController chestController)
    {
        this.chestController = chestController;
    }
    private void Start()
    {
        chestController.SetChest();
        chestController.EnableBuyButtonOnChest(false);
        chestController.EnableUndoButton(false);
        unlockAfterTimerButton.onClick.AddListener(chestController.SetBuyButtonOnChest);
        undoChestButton.onClick.AddListener(chestController.UndoChestState);
        parentTransform = transform.parent;            
    }
    private void Update()
    {
        if ( chestController.chestStateMachine!=null)
        {
            chestController.chestStateMachine.Update();
        }
    }
}
