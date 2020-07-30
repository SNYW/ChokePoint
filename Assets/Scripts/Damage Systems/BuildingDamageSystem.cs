using UnityEngine;

public class BuildingDamageSystem : DamageSystem
{
    private Building building;

    private void Start()
    {
        building = GetComponent<Building>();
    }
    public override void DealDamage()
    {
       //Buildings Don't Attack Yet
    }

    public override void TakeDamage(int amount)
    {
        building.TakeDamage(amount);
    }
}
