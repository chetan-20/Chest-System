using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private UIService uiService;
    [SerializeField] private ChestSlotService genChest;
    [SerializeField] private PopUpService popUpService;
    [SerializeField] private ChestEnablerScript chestEnabler;
    public PlayerData playerData;
    private bool istimerActive;
    private static GameService instance;

    public static GameService Instance { get{ return instance; } }
    public UIService UIService { get{ return uiService; } }
    public ChestSlotService GenerateChest { get { return genChest; } }
    public PopUpService PopUpService { get { return popUpService; } }
    public ChestEnablerScript ChestEnablerScript { get { return chestEnabler; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        } 
        playerData = new PlayerData();
    }
    private void Start()
    {
        istimerActive = false;      
    }
    private void Update()
    {
        OnEscapePress();
    }
    private void OnEscapePress()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void SetTimerStatus(bool status)
    {
        istimerActive = status;
    }
    public bool GetTimerStatus()
    {
        return istimerActive;
    }
}
