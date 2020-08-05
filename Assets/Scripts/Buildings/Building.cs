﻿using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool playerOwned;
    public string buildingName;
    public int maxHealth;
    public int currentHealth;
    public int buildCost;
    public Slider healthbar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.gameObject.SetActive(false);
    }

    private void ManageHealthBar()
    {
        healthbar.value = maxHealth / currentHealth * 0.1f;
    }

    public void TakeDamage(int amount)
    {
        healthbar.gameObject.SetActive(true);
        if (currentHealth - amount <= 0)
        {
            Die();
        }
        else
        {
            currentHealth -= amount;
            ManageHealthBar();
        }
    }

    private void Die()
    {
        if (playerOwned)
        {
            MapDataManager.playerBuildings.Remove(transform);
        }
        else
        {
            MapDataManager.enemyBuildings.Remove(transform);
        }
        var tile = BuildGridManager.GetTile(transform.position);
        tile.DeleteBuilding();
    }
}
