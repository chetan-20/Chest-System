public class PlayerData 
{
    private int playerCoins;
    private int playerGems;   
    public PlayerData()
    {
        playerCoins = 2000;
        playerGems = 200;
    }
    public int GetPlayerCoins()
    {
        return playerCoins;
    }
    public int GetPlayerGems()
    {
        return playerGems;
    }
    public void AddCoins(int amount)
    {
        playerCoins += amount;
    }
    public void AddGems(int amount)
    {
        playerGems += amount;
    }   
    public void RemoveGems(int amount)
    {
        playerGems -= amount;
    }
}
