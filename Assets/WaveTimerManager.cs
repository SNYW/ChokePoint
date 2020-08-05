using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimerManager : MonoBehaviour
{
    public float waveSpawnTimer;

    private float currentWaveSpawnTimer;
    
    void Start()
    {
        currentWaveSpawnTimer = waveSpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();
    }
    void CheckTimer()
    {
        currentWaveSpawnTimer -= Time.deltaTime;
        if(currentWaveSpawnTimer <= 0)
        {
            currentWaveSpawnTimer = waveSpawnTimer;
            SpawnWave();
        }
    }
    void SpawnWave()
    {
        foreach(Transform building in MapDataManager.enemyBuildings)
        {
            var spawner = building.GetComponent<UnitSpawner>();
            if(spawner != null)
            {
                spawner.SpawnUnit();
            }
        }
        foreach (Transform building in MapDataManager.playerBuildings)
        {
            var spawner = building.GetComponent<UnitSpawner>();
            if (spawner != null)
            {
                spawner.SpawnUnit();
            }
        }
    }
}
