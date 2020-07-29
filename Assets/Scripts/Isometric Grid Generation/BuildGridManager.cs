using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class BuildGridManager : MonoBehaviour
{
    public Transform tileContainer;
    public GameObject gridTile;
    public Vector2 minBounds;
    public Vector2 maxBounds;
    private static Grid grid;
    public static Dictionary<Vector2,GameObject> gridTiles;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    void Start()
    {
        GenerateBuildGrid();
    }

    public void GenerateBuildGrid()
    {
        gridTiles = new Dictionary<Vector2, GameObject>();
        ClearGrid();
        grid = GetComponent<Grid>();
        for (float xIndex = minBounds.x; xIndex < maxBounds.x; xIndex += grid.cellSize.x)
        {
            for (float yIndex = minBounds.y; yIndex < maxBounds.y; yIndex += grid.cellSize.y)
            {
                Vector3Int gridPos = (grid.WorldToCell(new Vector2(xIndex, yIndex)));
                Vector3Int diagGridPos = (grid.WorldToCell(new Vector2(xIndex+(grid.cellSize.x/2), yIndex + (grid.cellSize.y / 2))));
                var mid = Instantiate(gridTile, (Vector2)grid.GetCellCenterWorld(gridPos), Quaternion.identity, tileContainer);
                var diag = Instantiate(gridTile, (Vector2)grid.GetCellCenterWorld(diagGridPos), Quaternion.identity, tileContainer);
                mid.name = grid.GetCellCenterWorld(gridPos).ToString();
                diag.name = grid.GetCellCenterWorld(diagGridPos).ToString();
                gridTiles.Add(grid.GetCellCenterWorld(gridPos), mid);
                gridTiles.Add(grid.GetCellCenterWorld(diagGridPos),diag);
            }
        }
    }

    public void ClearGrid()
    {
        if (gridTiles != null && gridTiles.Count > 0)
        {
            foreach(Vector2 key in gridTiles.Keys)
            {
                DestroyImmediate(gridTiles[key]);
            }
            gridTiles.Clear();
        }
        foreach (Transform t in tileContainer.transform)
        {
            DestroyImmediate(t.gameObject);
        }
    }
    public static GridTile GetTile(Vector2 location)
    {
        Vector3Int gridPos = (grid.WorldToCell(location));
        if (gridTiles.ContainsKey(grid.GetCellCenterWorld(gridPos)))
        {
            var obj = gridTiles[grid.GetCellCenterWorld(gridPos)];
            GridTile tile = obj.GetComponent<GridTile>();
            return tile;
        }
        return null;
    }

    public static List<GridTile> GetTileNeighbours(GridTile origin, int factor)
    {
        List<GridTile> neighbours = new List<GridTile>();
        neighbours.Add(GetTile(grid.GetCellCenterLocal(grid.LocalToCell(origin.transform.position))));
        //Debug.Log("Origin = " + grid.GetCellCenterLocal(grid.LocalToCell(origin.transform.position)));
        if (origin != null)
        {
            Collider2D[] coll = Physics2D.OverlapCircleAll(origin.transform.position, 0.6f);
            foreach(Collider2D tile in coll)
            {
                if(tile.GetComponent<GridTile>() != null)
                {
                    neighbours.Add(tile.GetComponent<GridTile>());
                }
            }
        }
        return neighbours;
    }
}
