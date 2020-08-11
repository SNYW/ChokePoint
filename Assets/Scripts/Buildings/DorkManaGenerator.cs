using UnityEngine;

public class DorkManaGenerator : ResourceGenerator
{
    private ManaEconomy manaEconomy;
    public int storageIncrease;
    private void Awake()
    {
        manaEconomy = GameObject.Find("PlayerBase").GetComponent<ManaEconomy>();
        IncreaseIncome();
        IncreaseStorage();
    }
    private void IncreaseStorage()
    {
        manaEconomy.storageMax += storageIncrease;
    }
    private void OnDestroy()
    {
        manaEconomy.storageMax -= storageIncrease;
    }
}
