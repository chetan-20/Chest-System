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
    public ChestController chestController { get; private set; }

    public void SetViewController(ChestController chestController)
    {
        this.chestController = chestController;
    }
    private void Start()
    {
        chestController.SetChest();
        unlockAfterTimerButton.gameObject.SetActive(false);
        parentTransform = transform.parent;
        unlockAfterTimerButton.onClick.AddListener(chestController.SetBuyButtonOnChest);
    }
    private void Update()
    {
        if ( chestController.chestStateMachine!=null)
        {
            chestController.chestStateMachine.Update();
        }
    }
}
