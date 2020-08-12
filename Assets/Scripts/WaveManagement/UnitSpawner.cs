using UnityEngine;

public class UnitSpawner : Building
{
    public string unitName;
    public bool playerUnit;
    public Sprite cursorImage;

    public void SpawnUnit()
    {
        var unit = Instantiate(MapDataManager.poolDictionary[unitName].Dequeue(), transform.position, Quaternion.identity);
       
        unit.SetActive(true);
        if (playerUnit)
        {
            MapDataManager.Add(MapDataManager.playerUnits, unit.transform);
        }
        else
        {
            MapDataManager.Add(MapDataManager.enemyUnits, unit.transform);
        }
        
    }
}
