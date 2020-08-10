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
    public static Dictionary<Vector3 ,GameObject> gridTiles;

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
        gridTiles = new Dictionary<Vector3, GameObject>();
        ClearGrid();
        grid = GetComponent<Grid>();
        Debug.Log(minBounds.x +" "+ maxBounds.x);

        Debug.Log(minBounds.y + " " + maxBounds.y);
        for (float xIndex = minBounds.x; xIndex < maxBounds.x; xIndex += grid.cellSize.x)
        {
            for (float zIndex = minBounds.y; zIndex < maxBounds.y; zIndex += grid.cellSize.y)
            {
               // Debug.Log(new Vector3(xIndex, 0, zIndex));
                Vector3Int gridPos = (grid.WorldToCell(new Vector3(xIndex, 0, zIndex)));
                Vector3Int diagGridPos = (grid.WorldToCell(new Vector3(xIndex+(grid.cellSize.x/2),0, zIndex + (grid.cellSize.y / 2))));
                var gridRot = Quaternion.Euler(new Vector3(90, 0, 45));
                Debug.Log(gridRot.eulerAngles);
                var mid = Instantiate(gridTile, grid.GetCellCenterWorld(gridPos), gridRot, tileContainer);
                var diag = Instantiate(gridTile, grid.GetCellCenterWorld(diagGridPos), gridRot, tileContainer);
                mid.transform.rotation = gridRot;
                //Debug.Log(grid.GetCellCenterWorld(gridPos));
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
                if (gridTiles.ContainsKey(key))
                {
                    DestroyImmediate(gridTiles[key]);
                }
            }
            gridTiles.Clear();
        }
        while (tileContainer.childCount > 0)
        {
            foreach (Transform t in tileContainer.transform)
            {
                DestroyImmediate(t.gameObject);
            }
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
