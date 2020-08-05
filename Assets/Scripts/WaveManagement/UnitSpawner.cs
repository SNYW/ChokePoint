﻿using UnityEngine;

public class UnitSpawner : Building
{
    public string unitName;
    public float spawnCooldown;
    public bool playerUnit;
    public Sprite cursorImage;

    private float currentCD;
    private void Start()
    {
        currentCD = spawnCooldown;
    }

    /*private void Update()
    {
        ManageCooldown();
    }
    private void ManageCooldown()
    {
        currentCD -= Time.deltaTime;
        if(currentCD <= 0)
        {
            SpawnUnit();
            currentCD = spawnCooldown;
        }
    }*/

    public void SpawnUnit()
    {
        var unit = Instantiate(MapDataManager.poolDictionary[unitName].Dequeue(), transform.position, Quaternion.identity);
        unit.SetActive(true);
        if (unit.GetComponent<PlayerUnit>() != null)
        {
            MapDataManager.Add(MapDataManager.playerUnits, unit.transform);
        }
        else
        {
            MapDataManager.Add(MapDataManager.enemyUnits, unit.transform);
        }
        
    }
}