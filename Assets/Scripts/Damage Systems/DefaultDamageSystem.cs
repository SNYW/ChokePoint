
public class DefaultDamageSystem : DamageSystem
{
    Unit unit;
    private void Start()
    {
        unit = GetComponent<Unit>();
    }
    public override void DealDamage()
    {
        unit.target.GetComponent<DamageSystem>().TakeDamage(unit.attackDamage);
    }

    public override void TakeDamage(int amount)
    {
        if (unit.gameObject.activeSelf)
        {
            unit.currentHealth -= amount;
            if (unit.currentHealth <= 0) { unit.Die(); }
            unit.healthBar.value = unit.currentHealth / unit.maxHealth;
        }
    }
}
