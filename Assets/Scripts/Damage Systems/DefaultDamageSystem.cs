using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void TakeDamage(float amount)
    {
        unit.currentHealth -= amount;
    }
}
