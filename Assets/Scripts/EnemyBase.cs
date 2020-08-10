using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Grid grid;
    public WaveSet waveSet;
    public Vector2 buildAreaMax;
    public Vector2 buildAreaMin;
    private float buildCooldown;
    private int waveIndex;
    private GameObject[] buildings;
    
    private float currentBuildCooldown;
    void Start()
    {
        waveIndex = 0;
        currentBuildCooldown = GetWaveCooldown();
    }

    
    void Update()
    {
        ManageCooldown();
    }
    private void ManageCooldown()
    {
        currentBuildCooldown -= Time.deltaTime;
        if (currentBuildCooldown <= 0)
        {
            Build();
            currentBuildCooldown = buildCooldown;
        }
    }

    private void Build()
    {
        buildings = GetWaveBuildings();
        foreach (GameObject building in buildings)
        {
            Vector2 randomBuildLocation = new Vector3(Random.Range(buildAreaMin.x, buildAreaMax.x), 0, Random.Range(buildAreaMin.y, buildAreaMax.y));
            Vector3Int gridPos = (grid.WorldToCell(randomBuildLocation));
            var buildLocation = grid.GetCellCenterLocal(gridPos);
            var instBuilding = Instantiate(building, BuildGridManager.GetTile(buildLocation).transform.position, Quaternion.identity);
            MapDataManager.Add(MapDataManager.enemyBuildings, instBuilding.transform);
        }
        buildCooldown = GetWaveCooldown();
        waveIndex++;
    }

    private GameObject[] GetWaveBuildings()
    {   
        if(waveIndex >= waveSet.waves.Length)
        {
            waveIndex = waveSet.waves.Length-1;
        }
        return waveSet.waves[waveIndex].buildings;
    }
    private float GetWaveCooldown()
    {
        return waveSet.waves[waveIndex].timeToNext;
    }
}
