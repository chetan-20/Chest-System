using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private UIService uiService;
    [SerializeField] private GenerateChest genChest;
    [SerializeField] private PopUpService popUpService;
    private static GameService instance;
    public static GameService Instance { get{ return instance; } }
    public UIService UIService { get{ return uiService; } }
    public GenerateChest GenerateChest { get { return genChest; } }
    public PopUpService PopUpService { get { return popUpService; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
}
