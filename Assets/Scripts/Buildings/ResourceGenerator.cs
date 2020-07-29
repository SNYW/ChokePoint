using UnityEngine;

public class ResourceGenerator : Building
{
    public PlayerEconomy playerEconomy;
    public int rateIncrease;

    protected void IncreaseIncome()
    {
        playerEconomy = GameObject.Find("PlayerBase").GetComponent<PlayerEconomy>();
        playerEconomy.generationRate += rateIncrease;
    }
    
    private void OnDestroy()
    {
        playerEconomy.generationRate -= rateIncrease;
    }
}
