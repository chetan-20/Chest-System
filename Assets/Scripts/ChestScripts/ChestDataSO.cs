using UnityEngine;
[CreateAssetMenu]
public class ChestDataSO : ScriptableObject
{
    public ChestType chestType;
    public Sprite chestSprite;
    public int timerInMinutes;
    public int coinsMinRange;
    public int coinsMaxRange;
    public int gemsMinRange;
    public int gemsMaxRange;      
    public ChestStates currentChestState;
    public bool undoPressed;
}
