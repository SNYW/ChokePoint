using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public Grid grid;
    public GameObject building;
    public float buildCooldown;
    public Vector2 buildAreaMax;
    public Vector2 buildAreaMin;

    private float currentBuildCooldown;
    void Start()
    {
        currentBuildCooldown = buildCooldown;
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
        Vector2 randomBuildLocation = new Vector2(Random.Range(buildAreaMin.x, buildAreaMax.x), Random.Range(buildAreaMin.y, buildAreaMax.y));
        Vector3Int gridPos = (grid.WorldToCell(randomBuildLocation));
        var buildLocation = (Vector2)grid.GetCellCenterLocal(gridPos);
        Instantiate(building, BuildGridManager.GetTile(buildLocation).transform.position, Quaternion.identity);
    }
}
