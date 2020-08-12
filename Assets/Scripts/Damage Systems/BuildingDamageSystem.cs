using UnityEngine;
public class BuildingDamageSystem : DamageSystem
{
    private Building building;

    private void Start()
    {
        building = GetComponent<Building>();
        building.healthbar.gameObject.SetActive(false);
    }
    public override void DealDamage()
    {
    }

    public override void TakeDamage(int amount)
    {
        building.healthbar.gameObject.SetActive(true);
        building.currentHealth -= amount;
        if (building.currentHealth <= 0) { building.Die(); }
        building.healthbar.value = building.currentHealth / building.maxHealth;
    }
}
