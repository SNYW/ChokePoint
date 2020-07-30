﻿using UnityEngine;

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
       //Buildings Don't Attack Yet
    }

    public override void TakeDamage(int amount)
    {
        building.healthbar.gameObject.SetActive(true);
        building.TakeDamage(amount);
    }
}
