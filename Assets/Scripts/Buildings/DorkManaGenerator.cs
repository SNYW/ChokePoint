using UnityEngine;

public class DorkManaGenerator : ResourceGenerator
{
    private ManaEconomy manaEconomy;
    public int storageIncrease;
    private void Awake()
    {
        IncreaseIncome();
        IncreaseStorage();
    }
    private void IncreaseStorage()
    {
        manaEconomy = GameObject.Find("PlayerBase").GetComponent<ManaEconomy>();
        manaEconomy.storageMax += storageIncrease;
    }
    private void OnDestroy()
    {
        manaEconomy.storageMax -= storageIncrease;
    }
}
